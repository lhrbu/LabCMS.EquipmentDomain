using LabCMS.EquipmentDomain.Shared.Models;
using LabCMS.ProjectDomain.Shared.Models;
using LabCMS.ProjectDomain.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Shared.Services
{
    public class UsageRecordProjectProvider
    {
        private readonly ProjectsWebCacheService _cacheService;
        public UsageRecordProjectProvider(
            ProjectsWebCacheService cacheService)
        { _cacheService = cacheService; }

        public Project? GetProject(UsageRecord usageRecord)=>
            _cacheService.Projects.FirstOrDefault(item=>item.Name==usageRecord.ProjectName)
    }
}
