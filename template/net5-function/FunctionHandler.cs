using System.Threading.Tasks;

using Function.Extensions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Function
{
    internal class FunctionHandler
    {
        private readonly ILogger<FunctionHandler> _logger;
        
        public FunctionHandler(ILogger<FunctionHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(HttpContext context)
        {
            _logger.LogInformation("Function run");
            
            return context.Response.WriteAsync(".NET5 Function".WithLogo(), context.RequestAborted);
        }
    }
}