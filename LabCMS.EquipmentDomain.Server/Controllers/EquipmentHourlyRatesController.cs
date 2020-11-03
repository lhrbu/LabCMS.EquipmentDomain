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
        private readonly EquipmentHourlyRatesRepository  _repository;
        public EquipmentHourlyRatesController(EquipmentHourlyRatesRepository repository)
        { _repository = repository; }
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
    }
}
