using LabCMS.EquipmentDomain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Shared.Services
{
    public class EquipmentHourlyRateProvider
    {
        private readonly EquipmentHourlyRatesWebCacheService _cacheService;
        public EquipmentHourlyRateProvider(EquipmentHourlyRatesWebCacheService cacheService)
        { _cacheService = cacheService; }
        public EquipmentHourlyRate? GetEquipmentHourlyRate(UsageRecord usageRecord)=>
            _cacheService.CachedEquipmentHourlyRates.FirstOrDefault(item => item.EquipmentNo == usageRecord.EquipmentNo);
    }
}
