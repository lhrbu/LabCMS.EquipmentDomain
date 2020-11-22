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
    public class UsageRecordIdEqualityComparer:IEqualityComparer<UsageRecord>
    {
        public bool Equals(UsageRecord? x, UsageRecord? y)
        {
            if((x is null) && (y is null)) { return false; }
            else { return x?.Id == y?.Id; }
        }
        public int GetHashCode(UsageRecord obj)=>obj.Id.GetHashCode();
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ElasticSearchInteropController : ControllerBase
    {
        private readonly UsageRecordIdEqualityComparer _comparer = new();
        private readonly UsageRecordsRepository _usageRecordsRepository;
        private readonly ElasticSearchInteropService _elasticInterop;
        public ElasticSearchInteropController(
            ElasticSearchInteropService elasticSearch,
            UsageRecordsRepository usageRecordsRepository)
        { 
            _elasticInterop = elasticSearch;
            _usageRecordsRepository = usageRecordsRepository;
        }

        [HttpPost]
        public async ValueTask SyncWithDatabaseAsync()
        { 
            IEnumerable<UsageRecord> usageRecords = _usageRecordsRepository.UsageRecords.AsNoTracking();
            var count = usageRecords.Count();
            var res = usageRecords.Where(item=>item.Id is null).ToArray();

            IEnumerable<UsageRecord> usageRecordsInES = await _elasticInterop.SearchAllAsync();
            var reses = usageRecordsInES.Where(item=>item.Id is null).ToArray();
            // await _elasticInterop.RemoveManyAsync(reses);
            // Only in ES but not in sqlite, which means need to be deleted
            IEnumerable<UsageRecord> recordsNeedToDelete = usageRecordsInES.Except(usageRecords,_comparer).ToList();
            
            // Only in database but not in sqlite, which means need to be added
            IEnumerable<UsageRecord> recordsNeedToAdd = usageRecords.Except(usageRecordsInES,_comparer).ToList();

            if(recordsNeedToDelete.Any()){
                await _elasticInterop.RemoveManyAsync(recordsNeedToDelete);
            }
            if(recordsNeedToAdd.Any()){
                await _elasticInterop.IndexManyAsync(recordsNeedToAdd);
            }

            //await _elasticSearch.RemoveAllAsync();
            //await _elasticSearch.IndexManyAsync(usageRecords);
        }
    }
}
