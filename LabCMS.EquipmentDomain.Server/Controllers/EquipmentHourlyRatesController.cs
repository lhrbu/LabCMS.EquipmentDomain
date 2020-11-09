using LabCMS.EquipmentDomain.Server.Repositories;
using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UsageRecordsRecycleBin _usageRecordsRecyclBin;
        private readonly EquipmentHourlyRatesRepository  _repository;
        public EquipmentHourlyRatesController(
            EquipmentHourlyRatesRepository repository,
            UsageRecordsRepository usageRecordsRepository,
            UsageRecordsRecycleBin usageRecordsRecycleBin
            )
        { 
            _repository = repository;
            _usageRecordsRepository = usageRecordsRepository;
            _usageRecordsRecyclBin = usageRecordsRecycleBin;
        }
        [HttpGet]
        public IAsyncEnumerable<EquipmentHourlyRate> GetAsync() =>
            _repository.EquipmentHourlyRates.AsNoTracking().AsAsyncEnumerable();
        
        [HttpPost]
        public async ValueTask PostAsync(EquipmentHourlyRate equipmentHourlyRate)
        {
            await _repository.EquipmentHourlyRates.AddAsync(equipmentHourlyRate);
            await _repository.SaveChangesAsync();
        }

        [HttpPut]
        public async ValueTask PutAsync(EquipmentHourlyRate equipmentHourlyRate)
        {
            _repository.EquipmentHourlyRates.Update(equipmentHourlyRate);
            await _repository.SaveChangesAsync();
        }

        [HttpDelete("{equipmentNo}")]
        public async ValueTask<ActionResult> DeleteAsync(string equipmentNo)
        {
            if(_usageRecordsRepository.UsageRecords.Any(item=>item.EquipmentNo == equipmentNo) || 
                    _usageRecordsRecyclBin.SoftDeletedUsageRecords.Any(item=>item.EquipmentNo==equipmentNo))
            { return BadRequest();}
            else{
               EquipmentHourlyRate? equipmentHourlyRate =await _repository.EquipmentHourlyRates.FindAsync(equipmentNo);
               if(equipmentHourlyRate is not null)
               {
                   _repository.EquipmentHourlyRates.Remove(equipmentHourlyRate);
                   await _repository.SaveChangesAsync();
                   return Ok();
               }else{ return BadRequest();}
            }
                    
        }
    }
}
