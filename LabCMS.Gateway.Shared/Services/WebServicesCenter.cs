using LabCMS.Gateway.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace LabCMS.Gateway.Shared.Services
{
    public class WebServicesCenter
    {
        private static Uri DefaultGatewayHostUri = new("http://localhost:5050"); 
        private Uri? ConfigGateHostUri=> _configuration.GetValue<Uri?>("GatewayUrl");

        private readonly IConfiguration _configuration;
        public WebServicesCenter(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        public Uri? GatewayHostUri => ConfigGateHostUri ?? DefaultGatewayHostUri;

        public async ValueTask<IEnumerable<WebService>> GetAsync()
        {
            using HttpClient httpClient = new();
            Uri getUri = new(GatewayHostUri!, $"/api/{nameof(WebService)}s");
            return (await httpClient.GetFromJsonAsync<IEnumerable<WebService>>(GatewayHostUri))!;
        }

        public async ValueTask<WebService?> GetByNameAsync(string name)
        {
            using HttpClient httpClient = new();
            Uri getUri = new(GatewayHostUri!, $"/api/{nameof(WebService)}s/{name}");
            return await httpClient.GetFromJsonAsync<WebService>(getUri);
        }

        public async ValueTask PostAsync(WebService webService)
        {
            using HttpClient httpClient = new();
            Uri postUri = new(GatewayHostUri!, $"/api/{nameof(WebService)}s");
            await httpClient.PostAsJsonAsync(postUri, webService);
        }
        public async ValueTask DeleteByIdAsync(Guid id)
        {
            using HttpClient httpClient = new();
            Uri deleteUri = new(GatewayHostUri!, $"/api/{nameof(WebService)}s/{id}");
            await httpClient.DeleteAsync(deleteUri);
        }
    }
}
