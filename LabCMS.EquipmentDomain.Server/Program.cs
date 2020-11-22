using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using LabCMS.EquipmentDomain.Server.Services;
using LabCMS.EquipmentDomain.Server.Controllers;
using Syncfusion.XlsIO;
using System.IO;
using LabCMS.ProjectDomain.Shared.Services;
using LabCMS.EquipmentDomain.Shared.Services;

namespace LabCMS.EquipmentDomain.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            RegisterSyncfusion();
            IHost host = CreateHostBuilder(args).Build();
            await InitializeCache(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task InitializeCache(IHost host)=>
            await host.Services.GetRequiredService<ReloadCacheService>().ReloadCacheAsync();
        
        static void TestDynamicQueryService(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var service= scope.ServiceProvider.GetRequiredService<DynamicQueryService>();
            Guid assemblyId =Guid.NewGuid();
            //var result = service.DynamicQueryByV8("return usageRecords.Count");
            //Type type = result.GetType();
        }

        

        private static void RegisterSyncfusion()=>
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "MzMzMjA4QDMxMzgyZTMzMmUzMGFvandKU2FCMEhVTWRnUWlIOUQvWFB6UGxXM0VsTjRBRnpzclRPSGIxL289");
    }
}
