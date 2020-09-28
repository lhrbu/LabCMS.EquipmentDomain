using LabCMS.EquipmentDomain.Server.Repositories;
using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentHourlyRatesController : ControllerBase
    {
        private readonly UsageRecordsRepository _usageRecordsRepository;
        public EquipmentHourlyRatesController(
            UsageRecordsRepository usageRecordsRepository
            )
        { 
            _usageRecordsRepository = usageRecordsRepository;
        }

        [HttpGet]
        public IAsyncEnumerable<UsageRecord> GetAsync() =>
            _usageRecordsRepository.UsageRecords.AsNoTracking().AsAsyncEnumerable();

        [HttpPost]
        public async ValueTask PostAsync(UsageRecord usageRecord)
        {
            await _usageRecordsRepository.UsageRecords.AddAsync(usageRecord);
            await _usageRecordsRepository.SaveChangesAsync();
        }

        [HttpPut]
        public async ValueTask PutAsync(UsageRecord usageRecord)
        {
            _usageRecordsRepository.UsageRecords.Update(usageRecord);
            await _usageRecordsRepository.SaveChangesAsync();
        }
        [HttpDelete]
        public async ValueTask DeleteByIdAsync(Guid id)
        {
            UsageRecord? usageRecord = await _usageRecordsRepository.UsageRecords.FindAsync(id);
            if(usageRecord!=null)
            {
                _usageRecordsRepository.UsageRecords.Remove(usageRecord);
                await _usageRecordsRepository.SaveChangesAsync();
            }
        }
    }
}
