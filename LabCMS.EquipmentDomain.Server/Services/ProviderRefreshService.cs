using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabCMS.ProjectDomain.Shared.Services;
using LabCMS.EquipmentDomain.Shared.Services;

namespace LabCMS.EquipmentDomain.Server.Services
{
    public class ProviderRefreshService
    {
        public async Task RefreshAsync()
        {
            await ProjectsWebAPI.RefreshAsync();
            await EquipmentHourlyRateProvider.RefreshAsync();
        }
    }
}