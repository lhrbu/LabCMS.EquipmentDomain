using LabCMS.FixtureDomain.Shared.Models;
using LabCMS.Gateway.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.FixtureDomain.Shared.Services
{
    public static class FixtureProvider
    {
        public static IEnumerable<Fixture> Fixtures { get; private set; }
            = new List<Fixture>();

        public static async Task LoadAsync()
        {
            Uri uri = UriProvider.GetUri(nameof(Fixture));
            using HttpClient httpClient = new();
            Fixtures = (await httpClient.GetFromJsonAsync<IEnumerable<Fixture>>(uri))!;
        }

    }
}
