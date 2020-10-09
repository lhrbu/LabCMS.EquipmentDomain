using LabCMS.EquipmentDomain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Shared.Services
{
    public class EquipmentHourlyRateProviderForUsageRecord
    {
        private readonly EquipmentHourlyRateCacheService _cacheService;
        public EquipmentHourlyRateProviderForUsageRecord(EquipmentHourlyRateCacheService cacheService)
        { _cacheService = cacheService; }
        public EquipmentHourlyRate? GetEquipmentHourlyRate(UsageRecord usageRecord)=>
            _cacheService.EquipmentHourlyRates.FirstOrDefault(item => item.EquipmentNo == usageRecord.EquipmentNo);
    }
}
