using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabCMS.Gateway.Shared.Services;

namespace LabCMS.Gateway.Shared.Extensions
{
    public static class RemoteIPAddressProviderExtensions
    {
        private readonly static string _outputTemplate =
               "[{Timestamp:yyyy/MM/dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        
        public static void InitializeLogger() =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Ocelot", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: _outputTemplate)
                .WriteTo.File("Gateway.log",outputTemplate:_outputTemplate)
                //.WriteTo.SQLite("Logs/Log.db")
                .CreateLogger();
        
        public static IApplicationBuilder UseRemoteIPAddressProviderForSerilog(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    RemoteIPAddressProvider remoteIPAddressProvider = app
                        .ApplicationServices.GetRequiredService<RemoteIPAddressProvider>();
                    diagnosticContext.Set("IPAddress", remoteIPAddressProvider.GetAddress(httpContext));
                };
                options.MessageTemplate = "{IPAddress} HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
            });
            return app;
        }

        public static void AddRemoteIPAddressProvider(this IServiceCollection services) =>
            services.TryAddSingleton<RemoteIPAddressProvider>();
    }
}