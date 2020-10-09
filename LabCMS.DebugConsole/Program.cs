using LabCMS.EquipmentDomain.Shared.Models;
using System.Text.Json;
using System;

namespace LabCMS.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestJsonConverter();
            
            Console.WriteLine("Hello World!");
        }

        static void TestJsonConverter()
        {
            UsageRecord usageRecord = new UsageRecord
            {
                Id = Guid.NewGuid(),
                StartTime = DateTimeOffset.Now,
                EndTime = DateTimeOffset.Now,
                ProjectName = "Haha",
                EquipmentNo = "01-01",
                TestNo = "No",
                TestType = "Vibration"
            };

            string str = JsonSerializer.Serialize(usageRecord);
            UsageRecord usageRecord2 = JsonSerializer.Deserialize<UsageRecord>(str);
        }
    }
}
