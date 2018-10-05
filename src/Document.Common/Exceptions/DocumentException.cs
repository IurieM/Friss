using System;
using System.Net;

namespace Document.Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
