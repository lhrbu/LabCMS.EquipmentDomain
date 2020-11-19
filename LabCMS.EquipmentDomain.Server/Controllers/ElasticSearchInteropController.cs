using LabCMS.EquipmentDomain.Server.Repositories;
using LabCMS.EquipmentDomain.Server.Services;
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
    public class ElasticSearchInteropController : ControllerBase
    {
        private readonly UsageRecordsRepository _usageRecordsRepository;
        private readonly ElasticSearchInteropService _elasticInterop;
        public ElasticSearchInteropController(
            ElasticSearchInteropService elasticSearch,
            UsageRecordsRepository usageRecordsRepository)
        { 
            _elasticInterop = elasticSearch;
            _usageRecordsRepository = usageRecordsRepository;
        }
        public async ValueTask SyncWithDatabaseAsync()
        { 
            IEnumerable<UsageRecord> usageRecords = _usageRecordsRepository.UsageRecords.AsNoTracking();
            IEnumerable<UsageRecord> usageRecordsInES = await _elasticInterop.SearchAllAsync();

            // Only in ES but not in sqlite, which means need to be deleted
            IEnumerable<UsageRecord> recordsNeedToDelete = usageRecordsInES.Except(usageRecords).ToList();
            
            // Only in database but not in sqlite, which means need to be added
            IEnumerable<UsageRecord> recordsNeedToAdd = usageRecords.Except(usageRecordsInES).ToList();

            await _elasticInterop.RemoveManyAsync(recordsNeedToDelete);
            await _elasticInterop.IndexManyAsync(recordsNeedToAdd);

            //await _elasticSearch.RemoveAllAsync();
            //await _elasticSearch.IndexManyAsync(usageRecords);
        }
    }
}
