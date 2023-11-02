namespace SmEticaret.Shared.ServiceResult
{
    public class ServiceResult : IServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public int StatusCode { get; private set; }

        public static IServiceResult Success(int statusCode = 200)
        {
            return new ServiceResult
            {
                IsSuccess = true,
                StatusCode = statusCode
            };
        }
        public static IServiceResult<T> Success<T>(T data, int statusCode = 200)
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                StatusCode = statusCode,
                Data = data
            };
        }

        public static IServiceResult Fail(string message, int statusCode = 400)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Message = message
            };
        }

        public static IServiceResult<T> Fail<T>(string message, int statusCode = 400)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Message = message
            };
        }
    }

    public class ServiceResult<T> : ServiceResult, IServiceResult<T>
    {
        public T? Data { get; internal set; }
    }
}
