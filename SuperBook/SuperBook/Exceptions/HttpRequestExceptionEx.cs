using System;
using System.Net;
using System.Net.Http;

namespace SuperBook.Exceptions
{
    public class HttpRequestExceptionEx : HttpRequestException
    {
        public HttpStatusCode HttpStatusCode { get; }

        public HttpRequestExceptionEx(HttpStatusCode statusCode) 
            : this(statusCode, null, null)
        {
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string message) 
            : this(statusCode, message, null)
        {
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string message, Exception inner) 
            : base(message, inner)
        {
            this.HttpStatusCode = statusCode;
        }
    }
}
