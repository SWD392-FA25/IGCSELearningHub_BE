using Application.Utils;
using Serilog.Context;
using System.Diagnostics;
using System.Text;

namespace WebAPI.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = context.TraceIdentifier;

            using (LogContext.PushProperty("TraceId", traceId))
            {
                var stopwatch = Stopwatch.StartNew();

                var requestBody = await ReadRequestBodyAsync(context);
                var maskedRequest = SensitiveDataMasker.MaskSensitiveData(requestBody);

                _logger.LogInformation("Incoming Request: {method} {url} | Body: {body}",
                    context.Request.Method, context.Request.Path, maskedRequest);

                var originalBodyStream = context.Response.Body;
                using var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                await _next(context);

                stopwatch.Stop();

                var responseBody = await ReadResponseBodyAsync(context);
                var maskedResponse = SensitiveDataMasker.MaskSensitiveData(responseBody);

                _logger.LogInformation("Response: {statusCode} | Duration: {duration}ms | Body: {body}",
                    context.Response.StatusCode, stopwatch.ElapsedMilliseconds, maskedResponse);

                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            return Truncate(body, 1000);
        }

        private async Task<string> ReadResponseBodyAsync(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var body = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            return Truncate(body, 1000);
        }

        private string Truncate(string input, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...(truncated)";
        }
    }
}
