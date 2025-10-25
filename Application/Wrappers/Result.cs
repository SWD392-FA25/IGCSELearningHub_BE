
namespace Application.Wrappers
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public string Status { get; set; } = "success";
        public int StatusCode { get; set; } = 200;
        public string? Message { get; set; }
        public T? Data { get; set; }
        public Dictionary<string, object>? Details { get; set; }
        public Dictionary<string, string>? Errors { get; set; }

        public Result()
        {
        }

        // Success response
        public static Result<T> Success(T data, string message = "Operation successful.", int statusCode = 200)
        {
            return new Result<T>
            {
                Succeeded = true,
                Status = "success",
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        // Fail response with optional errors
        public static Result<T> Fail(string message, int statusCode = 400, Dictionary<string, string>? errors = null)
        {
            return new Result<T>
            {
                Succeeded = false,
                Status = "fail",
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }

        public Result<T> AddDetail(string key, object value)
        {
            Details ??= new Dictionary<string, object>();
            Details[key] = value;
            return this;
        }

        public Result<T> AddError(string key, string message)
        {
            Errors ??= new Dictionary<string, string>();
            Errors[key] = message;
            return this;
        }

        public Result<T> SetStatusCode(int code)
        {
            StatusCode = code;
            return this;
        }
    }
}
