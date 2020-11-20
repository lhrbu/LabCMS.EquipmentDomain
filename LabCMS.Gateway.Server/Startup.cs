using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Configuration.File;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Serilog;
using Microsoft.AspNetCore.Hosting.Server.Features;
using LabCMS.Gateway.Shared.Extensions;

namespace LabCMS.Gateway.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot().AddConsul();
            services.AddRemoteIPAddressProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRemoteIPAddressProviderForSerilog();
            ICollection<string> ipAddresses = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses;
            Console.WriteLine($"ASP NET Core now Listenning on: {ipAddresses.First()}");
            Log.Logger.Information("ASP NET Core now Listenning on: {Address}"
                ,ipAddresses);
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseOcelot();
            app.UseConsulAsServiceProvider(nameof(Gateway));
            
        }
    }
}
