using Function.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Function
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(
                    webHostBuilder => webHostBuilder.UseKestrel()
                        .ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Information))
                        .UseUrls("http://*:5000")
                        .ConfigureServices(CompositionRoot)
                        .Configure(Application));
        
        private static void CompositionRoot(IServiceCollection services)
        {
            services.AddSingleton<FunctionHandler>();
        }
        
        private static void Application(IApplicationBuilder app)
        {
            var handler = app.ApplicationServices.GetRequiredService<FunctionHandler>();

            app.Use(_ => context => context.HandleRootAsync(handler.HandleAsync));
        }
    }
}
