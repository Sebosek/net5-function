# OpenFaaS .NET5 Function Template

This repository contains the template for OpenFaaS using the upgraded `of-watchdog` which allows for higher throughput.

```
> faas-cli template pull https://github.com/Sebosek/net5-function.git
> faas-cli new --list
Languages available as templates:
- net5-function
```

This template uses a middleware handler in an ASP.NET Core Web API. This allows additional context available in the request and more control over the response (by providing the HTTP context to the handler).

## Using the template
First, pull the template with the faas CLI and create a new function:

```
> faas-cli template pull https://github.com/Sebosek/net5-function.git
> faas-cli new --lang net5-function <function name>
```

In the directory that was created, using the name of you function, you'll find `FunctionHandler.cs`. It will look like this:

``` csharp
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
```

This is a simple implementation of a welcome function.