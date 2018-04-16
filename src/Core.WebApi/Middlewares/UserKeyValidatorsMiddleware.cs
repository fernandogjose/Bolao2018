using System;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Core.WebApi.Middlewares {

    public class UserKeyValidatorsMiddleware {
        private readonly RequestDelegate _next;

        private readonly UserValidation _userValidation;

        public UserKeyValidatorsMiddleware (RequestDelegate next, UserValidation userValidation) {
            _next = next;
            _userValidation = userValidation;
        }

        public async Task Invoke (HttpContext context) {

            StringValues tokenRequest;
            if (!context.Request.Headers.TryGetValue ("token", out tokenRequest)) {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync ("token não encontrado");
                return;
            }

            StringValues userIdRequest;
            if (!context.Request.Headers.TryGetValue ("userId", out userIdRequest)) {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync ("UserId não encontrado");
                return;
            }

            bool isValid = _userValidation.RequestIsValid (tokenRequest[0], Convert.ToInt32 (userIdRequest[0]));
            if (!isValid) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync ("token inválido");
                return;
            }

            await _next.Invoke (context);
        }
    }

    public static class UserKeyValidatorsExtension {
        public static IApplicationBuilder ApplyUserKeyValidation (this IApplicationBuilder app) {
            app.UseMiddleware<UserKeyValidatorsMiddleware> ();
            return app;
        }
    }
}