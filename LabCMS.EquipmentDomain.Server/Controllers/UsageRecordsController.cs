using LabCMS.EquipmentDomain.Server.Repositories;
using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using LabCMS.EquipmentDomain.Server.Services;

namespace LabCMS.EquipmentDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageRecordsController : ControllerBase
    {
        private readonly UsageRecordsRepository  _usageRecordsRepository;
        private readonly DynamicQueryService _dynamicQueryService;
        public UsageRecordsController(
            UsageRecordsRepository  usageRecordsRepository,
            DynamicQueryService dynamicQueryService
            )
        { 
            _usageRecordsRepository = usageRecordsRepository;
            _dynamicQueryService = dynamicQueryService;
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
        [HttpDelete("{id}")]
        public async ValueTask DeleteByIdAsync(Guid id)
        {
            UsageRecord? usageRecord = await _usageRecordsRepository.UsageRecords.FindAsync(id);
            if(usageRecord!=null)
            {
                _usageRecordsRepository.UsageRecords.Remove(usageRecord);
                await _usageRecordsRepository.SaveChangesAsync();
            }
        }

        [HttpPost("DynamicQuery")]
        public IEnumerable<dynamic> DynamicQuery(string codePiece)=>
            _dynamicQueryService.DynamicQuery(codePiece);
    }
}
