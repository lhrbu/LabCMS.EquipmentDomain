using LabCMS.Gateway.Shared.Services;
using LabCMS.EquipmentDomain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Shared.Services
{
    public static class EquipmentHourlyRateProvider
    {
        public static IEnumerable<EquipmentHourlyRate> EquipmentHourlyRates { get; private set; }
            = new List<EquipmentHourlyRate>();

        public static async Task RefreshAsync()
        {
            Uri uri = UriProvider.GetUri(nameof(EquipmentHourlyRate));
            using HttpClient httpClient = new();
            EquipmentHourlyRates = (await httpClient.GetFromJsonAsync<IEnumerable<EquipmentHourlyRate>>(uri))!;
        } 
    }
}