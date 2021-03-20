using Microsoft.AspNetCore.Http;
using Shukhlyada.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shukhlyada.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is HttpException httpException)
                {
                    context.Response.StatusCode = (int)httpException.StatusCode;
                    await context.Response.WriteAsync(httpException.ToJsonString());
                    return;
                }

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Unknown error has occured");
                return;
            }
        }
    }
}
