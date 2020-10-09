using LabCMS.EquipmentDomain.Shared.Models;
using LabCMS.Gateway.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LabCMS.EquipmentDomain.Shared.Services
{
    public class EquipmentHourlyRateCacheService
    {
        private readonly IConfiguration _configuration;
        private string GatewayUrls => _configuration["GatewayUrls"];
        public EquipmentHourlyRateCacheService(IConfiguration configuration)
        { _configuration = configuration; }
        public IEnumerable<EquipmentHourlyRate> EquipmentHourlyRates { get; private set; }
            = Array.Empty<EquipmentHourlyRate>();
        public async Task RefreshAsync()
        {
            Uri getUri = new($"{GatewayUrls}/api/{nameof(EquipmentHourlyRate)}s");
            using HttpClient httpClient = new();
            EquipmentHourlyRates = (await httpClient.GetFromJsonAsync<IEnumerable<EquipmentHourlyRate>>(getUri))!;
        }

    }
}
