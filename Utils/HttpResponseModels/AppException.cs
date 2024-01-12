using System.Net;

namespace Utils.HttpResponseModels
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object? Errors { get; set; }

        public AppException() { }

        public AppException(
            HttpStatusCode statusCode,
            string message,
            object? errors = null
        ) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
