using LabCMS.EquipmentDomain.Server.Repositories;
using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Services
{
    public class EquipmentHourlyRatesLocalCacheService
    {
        private readonly IServiceProvider _serviceProvider;
        public EquipmentHourlyRatesLocalCacheService(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<EquipmentHourlyRate> CachedEquipmentHourlyRates { get; private set; } 
            = Array.Empty<EquipmentHourlyRate>();

        public void RefreshCache()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            CachedEquipmentHourlyRates = scope.ServiceProvider.GetRequiredService<EquipmentHourlyRatesRepository>()
                .EquipmentHourlyRates.AsNoTracking().ToArray();
        }
        
    }
}
