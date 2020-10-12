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

namespace LabCMS.EquipmentDomain.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestDynamicQueryService(args);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static void TestDynamicQueryService(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var service= scope.ServiceProvider.GetRequiredService<DynamicQueryService>();
            Guid assemblyId =Guid.NewGuid();
            var result = service.DynamicQuery("return new[]{1,2,3,4,5};");
            Type type = result.GetType();
        }
    }
}
