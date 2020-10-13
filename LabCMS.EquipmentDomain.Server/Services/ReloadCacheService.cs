using LabCMS.ProjectDomain.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Services
{
    public class ReloadCacheService
    {
        private readonly ProjectsWebCacheService _projectsWebCacheService;
        private readonly EquipmentHourlyRatesLocalCacheService _equipmentHourlyRatesLocalCacheService;
        public ReloadCacheService(ProjectsWebCacheService projectsWebCacheService,
            EquipmentHourlyRatesLocalCacheService equipmentHourlyRatesLocalCacheService)
        {
            _projectsWebCacheService = projectsWebCacheService;
            _equipmentHourlyRatesLocalCacheService = equipmentHourlyRatesLocalCacheService;
        }

        public async Task ReloadCacheAsync()
        {
            Task task = _projectsWebCacheService.RefreshCacheAsync();
            _equipmentHourlyRatesLocalCacheService.RefreshCache();
            await task;
        }
    }
}
