using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.Extensions.Configuration;
using Nest;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Services
{
    public class ElasticSearchInteropService
    {
        private readonly ElasticClient _elasticClient;
        public string IndexName = nameof(UsageRecord).ToLower();
        public ElasticSearchInteropService(
            IConfiguration configuration)
        {
            _elasticClient = new(new Uri(configuration.GetConnectionString("ElasticSearchUrl")));
            TryCreateIndex();
        }
        private bool TryCreateIndex()
        {
            if (!_elasticClient.Indices.Exists(IndexName).Exists)
            {
                CreateIndexResponse response = _elasticClient.Indices.Create(IndexName, builder =>
                     builder.Map<UsageRecord>(mapper => mapper.AutoMap()));
                if (!response.IsValid)
                { Log.Logger.Information("Can't create Index,see {DebugInfo}", response.DebugInformation);}
                else { return true; }
            }
            return false;
        }

        public async ValueTask IndexAsync(UsageRecord usageRecord) =>
            await _elasticClient.IndexAsync(usageRecord, item => item.Index(IndexName));
        public async ValueTask IndexManyAsync(IEnumerable<UsageRecord> usageRecords) =>
            await _elasticClient.IndexManyAsync(usageRecords, IndexName);

        public async ValueTask<IEnumerable<UsageRecord>> SearchAllAsync() =>
            (await _elasticClient.SearchAsync<UsageRecord>(s => s.MatchAll())).Documents;

        public async ValueTask RemoveByIdAsync(Guid id) =>
            await _elasticClient.DeleteAsync<UsageRecord>(id);

        public async ValueTask RemoveManyAsync(IEnumerable<UsageRecord> usageRecords) =>
            await _elasticClient.DeleteManyAsync(usageRecords, IndexName);
        public async ValueTask RemoveAllAsync() =>
            await _elasticClient.Indices.DeleteAsync(new DeleteIndexRequest(IndexName));

    }
}
