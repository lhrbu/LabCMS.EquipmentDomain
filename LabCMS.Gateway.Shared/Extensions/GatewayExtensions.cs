using LabCMS.Gateway.Shared.Models;
using LabCMS.Gateway.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.Gateway.Shared.Extensions
{
    public static class GatewayExtensions
    {
        public static IServiceCollection AddGateway(this IServiceCollection services)
        {
            services.TryAddTransient<WebServicesCenter>();
            return services;
        }
        public static IApplicationBuilder UseGateway(this IApplicationBuilder app,string serviceName)
        {
            Uri hostUri = new(app.ServerFeatures.Get<IServerAddressesFeature>()
                    .Addresses.First());
            
            WebService webService = new()
            {
                Id = Guid.NewGuid(),
                Name = serviceName,
                HostUri = hostUri
            };
            IHostApplicationLifetime lifeTime = app.ApplicationServices
                .GetRequiredService<IHostApplicationLifetime>();
            WebServicesCenter webServicesCenter = app.ApplicationServices
                .GetRequiredService<WebServicesCenter>();

            lifeTime.ApplicationStarted.Register(
                async () => await webServicesCenter.PostAsync(webService));
            lifeTime.ApplicationStopped.Register(
                async () =>await webServicesCenter.DeleteByIdAsync(webService.Id.Value));

            return app;
        }
    }
}
