using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Function.Extensions
{
    public static class HttpContextExtensions
    {
        public static Task HandleRootAsync(this HttpContext context, Func<HttpContext, Task> handler) =>
            context.Request.Path == "/" ? handler(context) : NotFound(context);
        
        private static Task NotFound(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            
            return Task.CompletedTask;
        }
    }
}