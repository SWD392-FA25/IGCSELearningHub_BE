using Application.Exceptions;
using Application.Wrappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Serilog.Context;

namespace WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Items["X-Correlation-ID"]?.ToString() ?? context.TraceIdentifier;
            var path = context.Request.Path;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            using (LogContext.PushProperty("RequestPath", path))
            {
                try
                {
                    await _next(context);
                }
                catch (AppException ex) // App-defined (custom) exception
                {
                    _logger.LogError(ex, "AppException caught for path {Path} in middleware", path);

                    var response = Result<object>.Fail(ex.Message, (int)HttpStatusCode.BadRequest);

                    if (ex.Errors != null)
                    {
                        foreach (var error in ex.Errors)
                        {
                            response.AddError(error.Key, error.Value);
                        }
                    }

                    await WriteResponseAsync(context, response, HttpStatusCode.BadRequest);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Database update exception caught");

                    var response = Result<object>.Fail("A database error occurred.", 500)
                                                  .AddError("db", ex.InnerException?.Message ?? ex.Message);

                    await WriteResponseAsync(context, response, HttpStatusCode.InternalServerError);
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "SQL exception caught");

                    var response = Result<object>.Fail("Database error", 500)
                                                  .AddError("sql", ex.Message);

                    await WriteResponseAsync(context, response, HttpStatusCode.InternalServerError);
                }
                catch (UnauthorizedAccessException ex)
                {
                    _logger.LogError(ex, "UnauthorizedAccessException caught in middleware");

                    var response = Result<object>.Fail("Unauthorized", (int)HttpStatusCode.Unauthorized)
                                                      .AddError("detail", ex.Message);

                    await WriteResponseAsync(context, response, HttpStatusCode.Unauthorized);
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogError(ex, "KeyNotFoundException caught in middleware");

                    var response = Result<object>.Fail("Resource not found", (int)HttpStatusCode.NotFound)
                                                      .AddError("detail", ex.Message);

                    await WriteResponseAsync(context, response, HttpStatusCode.NotFound);
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogWarning(ex, "Operation was canceled");

                    var response = Result<object>.Fail("Request was cancelled or timed out", 408)
                                                  .AddError("cancellation", ex.Message);

                    await WriteResponseAsync(context, response, HttpStatusCode.RequestTimeout);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception caught in middleware");

                    var response = Result<object>.Fail("Internal Server Error", (int)HttpStatusCode.InternalServerError)
                                                      .AddError("exception", ex.Message);

                    await WriteResponseAsync(context, response, HttpStatusCode.InternalServerError);
                }
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, Result<object> response, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}