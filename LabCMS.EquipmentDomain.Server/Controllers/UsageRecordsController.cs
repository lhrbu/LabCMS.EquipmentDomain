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
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LabCMS.EquipmentDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageRecordsController : ControllerBase
    {
        private readonly UsageRecordsRepository  _usageRecordsRepository;
        private readonly DynamicQueryService _dynamicQueryService;
        private readonly ExcelExportService _excelExportService;
        public UsageRecordsController(
            UsageRecordsRepository  usageRecordsRepository,
            DynamicQueryService dynamicQueryService,
            ExcelExportService excelExportService
            )
        { 
            _usageRecordsRepository = usageRecordsRepository;
            _dynamicQueryService = dynamicQueryService;
            _excelExportService = excelExportService;
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
        public dynamic DynamicQuery([FromBody]string codePiece)=>
            _dynamicQueryService.DynamicQuery(codePiece);

        [HttpGet("ExcelInterop")]
        public dynamic ExportToExcelAsync()
        {
            Stream stream = _excelExportService.Export(
                _usageRecordsRepository.UsageRecords.AsNoTracking());
            return this.File(stream, "text/plain", "EquipmentUsageRecord.xlsx");
        }
    }
}
