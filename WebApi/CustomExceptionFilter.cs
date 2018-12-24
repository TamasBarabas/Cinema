using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Model.Exceptions;
using System;
using System.Net;

namespace WebApi
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = string.Empty;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(EmptySpecificationException))
            {
                message = context.Exception.ToString();
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(IndexOutOfRangeException))
            {
                message = context.Exception.ToString();
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(ArgumentOutOfRangeException))
            {
                message = context.Exception.ToString();
                status = HttpStatusCode.BadRequest;
            }
            else
            {
                message = context.Exception.Message;
                status = HttpStatusCode.NotFound;
            }
            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            var err = message + " " + context.Exception.StackTrace;
            response.WriteAsync(err);
        }
    }
}
