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
    public class ProjectProvider
    {
        private readonly ProjectsWebCacheService _cacheService;
        public ProjectProvider(
            ProjectsWebCacheService cacheService)
        { _cacheService = cacheService; }

        public Project? GetProject(UsageRecord usageRecord)=>
            _cacheService.CachedProjects.FirstOrDefault(item=>item.Name==usageRecord.ProjectName);
    }
}
