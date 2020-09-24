using LabCMS.FixtureDomain.Server.Services;
using LabCMS.FixtureDomain.Shared.Models;
using LabCMS.FixtureDomain.Shared.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LabCMS.FixtureDomain.Server
{
    public class Program
    {
        public static List<Fixture> mockData = Enumerable.Range(1, 100).Select(
        item => new Fixture
        {
            ProjectNo = item.ToString(),
            Type = FixtureType.Vibration,
            Direction = Direction.Left,
            SortId = 1,
            LocationNo = new() { StockNo = 1, Floor = 2 },
            Remark = "New Remark"
        }
    ).ToList();

        public static async Task Main(string[] args)
        {
            DynamicQueryService service = new();
            var items = await service.QueryAsync(mockData,
                @"(IEnumerable<Fixture> items)=>
                            items.Where(item=>item.ProjectNo.Contains(""1""))
                                .Take(20)"
                                
            );
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
