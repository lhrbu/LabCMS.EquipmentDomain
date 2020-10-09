using LabCMS.Gateway.Shared.Services;
using LabCMS.ProjectDomain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LabCMS.ProjectDomain.Shared.Services
{
    public class ProjectsWebCacheService
    {
        private readonly IConfiguration _configuration;
        private string GatewayUrls => _configuration["GatewayUrls"];        
        public ProjectsWebCacheService(IConfiguration configuration)
        { _configuration = configuration; }
        public IEnumerable<Project> CachedProjects { get; private set; } = Array.Empty<Project>();
        public async Task RefreshCacheAsync()
        {
            Uri getUri = new($"{GatewayUrls}/api/{nameof(Project)}s");
            using HttpClient httpClient = new();
            CachedProjects = (await httpClient.GetFromJsonAsync<IEnumerable<Project>>(getUri))!;
        }
    }
}
