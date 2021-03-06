using System;
using System.Net;
using System.Threading.Tasks;
using Core.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.WebApi.Middlewares {

    public class ErrorMiddleware {

        private readonly RequestDelegate next;

        public ErrorMiddleware (RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke (HttpContext context) {
            try {
                await next (context);
            } catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
            }
        }

        private static Task HandleExceptionAsync (HttpContext context, Exception exception) {
            var code = HttpStatusCode.InternalServerError;

            if (exception is ArgumentException) code = HttpStatusCode.BadRequest;
            else if (exception is AuthException) code = HttpStatusCode.Unauthorized;
            else if (exception is DuplicateException) code = HttpStatusCode.Ambiguous;

            var result = JsonConvert.SerializeObject (new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync (result);
        }
    }
}