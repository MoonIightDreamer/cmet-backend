using System.Text.Json.Serialization;

namespace cmet_backend.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApiError? Error { get; set; }

        public class ApiError
        {
            public string Code { get; set; }
            public string Message { get; set; }
        }

        public static ApiResponse success(object data)
        {
            return new ApiResponse
            {
                Success = true,
                Data = data
            };
        }

        public static ApiResponse success()
        {
            return new ApiResponse
            {
                Success = true
            };
        }

        public static ApiResponse fail(string code, string message)
        {
            return new ApiResponse
            {
                Success = false,
                Error = new ApiError
                {
                    Code = code,
                    Message = message
                }
            };
        }
    }
}
