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
using System.Text.Json;
using LabCMS.ProjectDomain.Shared.Services;

namespace LabCMS.EquipmentDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageRecordsController : ControllerBase
    {
        private readonly UsageRecordsRepository  _usageRecordsRepository;
        private readonly UsageRecordsRecycleBin _usageRecordsRecycleBin;
        private readonly DynamicQueryService _dynamicQueryService;
        private readonly ExcelExportService _excelExportService;
        private readonly ProjectsWebCacheService _projectsWebCacheService;
        private readonly EquipmentHourlyRatesLocalCacheService _equipmentHourlyRatesLocalCacheService;
        private readonly ElasticSearchInteropService _elasticSearch;
        
        public UsageRecordsController(
            UsageRecordsRepository  usageRecordsRepository,
            UsageRecordsRecycleBin usageRecordsRecycleBin,
            DynamicQueryService dynamicQueryService,
            ExcelExportService excelExportService,
            ProjectsWebCacheService projectsWebCacheService,
            EquipmentHourlyRatesLocalCacheService equipmentHourlyRatesLocalCacheService,
            ElasticSearchInteropService elasticSearch
            )
        { 
            _usageRecordsRepository = usageRecordsRepository;
            _usageRecordsRecycleBin = usageRecordsRecycleBin;
            _dynamicQueryService = dynamicQueryService;
            _excelExportService = excelExportService;
            _projectsWebCacheService = projectsWebCacheService;
            _equipmentHourlyRatesLocalCacheService = equipmentHourlyRatesLocalCacheService;

            _elasticSearch = elasticSearch;
        }

        [HttpGet]
        public IAsyncEnumerable<UsageRecord> GetAsync() =>
            _usageRecordsRepository.UsageRecords.AsNoTracking().AsAsyncEnumerable();

        [HttpPost]
        public async ValueTask<ActionResult> PostAsync(UsageRecord usageRecord)
        {
            if (Validate(usageRecord))
            {
                _=_elasticSearch.IndexAsync(usageRecord).ConfigureAwait(false);
                await _usageRecordsRepository.UsageRecords.AddAsync(usageRecord);
                await _usageRecordsRepository.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest("Invalid usage record was posted"); }
        }

        [HttpPut]
        public async ValueTask<ActionResult> PutAsync(UsageRecord usageRecord)
        {
            if (Validate(usageRecord))
            {
                _ = _elasticSearch.IndexAsync(usageRecord).ConfigureAwait(false);
                _usageRecordsRepository.UsageRecords.Update(usageRecord);
                await _usageRecordsRepository.SaveChangesAsync();
                return Ok();
            }
            else { return BadRequest("Invalid usage record was put"); }
        }
        [HttpDelete("{id}")]
        public async ValueTask DeleteByIdAsync(Guid id)
        {
            UsageRecord? usageRecord = await _usageRecordsRepository.UsageRecords.FindAsync(id);
            if(usageRecord!=null)
            {
                _=_elasticSearch.RemoveByIdAsync(id).ConfigureAwait(false);
                _usageRecordsRepository.UsageRecords.Remove(usageRecord);
                await _usageRecordsRecycleBin.SoftDeletedUsageRecords.AddAsync(usageRecord);
                
                await _usageRecordsRecycleBin.SaveChangesAsync();
                await _usageRecordsRepository.SaveChangesAsync();
            }
        }
        
        [HttpPost("Restore/{id}")]
        public async ValueTask RestoreById(Guid id)
        {
            UsageRecord? usageRecord = await _usageRecordsRecycleBin.SoftDeletedUsageRecords.FindAsync(id);
            if(usageRecord is not null)
            {
                _usageRecordsRecycleBin.SoftDeletedUsageRecords.Remove(usageRecord);
                await _usageRecordsRepository.UsageRecords.AddAsync(usageRecord);

                await _usageRecordsRecycleBin.SaveChangesAsync();
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

        [HttpGet("LoadSeedData")]
        public async ValueTask LoadSeedData()
        {
            using Stream stream=System.IO.File.OpenRead("OutputUsageRecords.json");
            UsageRecord[] usageRecords = (await JsonSerializer.DeserializeAsync<UsageRecord[]>(stream))!;
            await _usageRecordsRepository.UsageRecords.AddRangeAsync(usageRecords);
            await _usageRecordsRepository.SaveChangesAsync();
        }

        private bool Validate(UsageRecord usageRecord)=>
            _projectsWebCacheService.CachedProjects.Any(item => item.FullName == usageRecord.ProjectName) &&
            _equipmentHourlyRatesLocalCacheService.CachedEquipmentHourlyRates.Any(item => item.EquipmentNo == usageRecord.EquipmentNo);
        
    }
}
