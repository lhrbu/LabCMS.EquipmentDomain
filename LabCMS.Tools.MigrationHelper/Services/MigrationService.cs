using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabCMS.EquipmentDomain.Shared.Models;
using LabCMS.Tools.MigrationHelper.Models;
using System.Text.Json;

namespace LabCMS.Tools.MigrationHelper.Services
{
    public class MigrationService
    {
        public IEnumerable<UsageRecord> Convert(IEnumerable<ArchiveUsageRecord> archiveUsageRecords)=>
            archiveUsageRecords.Select(item=>new UsageRecord
            {
                Id=null,
                User = item.User,
                TestNo = item.TestNo,
                EquipmentNo = item.EquipmentNo,
                TestType = item.TestType,
                ProjectName = item.ProjectName,
                StartTime = item.StartTime!=null?DateTimeOffset.Parse(item.StartTime):null,
                EndTime = item.EndTime!=null?DateTimeOffset.Parse(item.EndTime):null,
            });
        
    }
}