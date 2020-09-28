using LabCMS.EquipmentDomain.Shared.Models;
using LabCMS.Gateway.Shared.Models;
using LabCMS.Gateway.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Shared.Services
{
    public class EquipmentHourlyRateCacheService
    {
        private readonly WebServicesCenter _webServicesCenter;
        public EquipmentHourlyRateCacheService(WebServicesCenter webServicesCenter)
        { _webServicesCenter = webServicesCenter; }
        public IEnumerable<EquipmentHourlyRate> EquipmentHourlyRates { get; private set; }
            = Array.Empty<EquipmentHourlyRate>();
        public async Task RefreshAsync()
        {
            WebService? webService = await _webServicesCenter.GetByNameAsync(nameof(EquipmentDomain));
            if (webService == null) { return; }
            Uri getUri = new(webService.HostUri!, $"/api/{nameof(EquipmentHourlyRate)}s");
            using HttpClient httpClient = new();
            EquipmentHourlyRates = (await httpClient.GetFromJsonAsync<IEnumerable<EquipmentHourlyRate>>(getUri))!;
        }

    }
}
