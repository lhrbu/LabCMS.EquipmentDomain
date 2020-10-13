using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using LabCMS.Tools.MigrationHelper.Models;
using LabCMS.Tools.MigrationHelper.Services;
using LabCMS.EquipmentDomain.Shared.Models;

namespace LabCMS.Tools.MigrationHelper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string archivePath = args.Length>0?args[0]:"ArchiveUsageRecords.json";
            using Stream stream = File.OpenRead(archivePath);
            ArchiveUsageRecord[] archiveItems = (await JsonSerializer.DeserializeAsync<ArchiveUsageRecord[]>(stream))!;

            string outputPath = args.Length>1?args[1]:"OutputUsageRecords.json";
            if(File.Exists(outputPath)){File.Delete(outputPath);}
            using Stream outputStream = File.OpenWrite(outputPath);
            MigrationService migrationService=new();
            IEnumerable<UsageRecord> items = migrationService.Convert(archiveItems);

            await JsonSerializer.SerializeAsync<IEnumerable<UsageRecord>>(outputStream,items,new JsonSerializerOptions{
                WriteIndented = true
            });

            Console.WriteLine("Convert Done");
        }
    }
}
