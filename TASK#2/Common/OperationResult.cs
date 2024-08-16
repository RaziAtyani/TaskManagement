namespace TASK_2.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public string Message { get; }
        public T Data { get; }

        public OperationResult(int statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            IsSuccess = statusCode >= 200 && statusCode < 300;
        }

        public OperationResult(int statusCode, string message) : this(statusCode, message, default)
        {
        }
    }

    public class OperationResult
    {
        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public string Message { get; }

        public OperationResult(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = statusCode >= 200 && statusCode < 300;
        }
    }
}
