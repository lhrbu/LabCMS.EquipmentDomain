using LabCMS.Gateway.Shared.Models;
using LabCMS.Gateway.Shared.Services;
using LabCMS.ProjectDomain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.ProjectDomain.Shared.Services
{
    public class ProjectsWebCacheService
    {
        private readonly WebServicesCenter _webServicesCenter;
        public ProjectsWebCacheService(WebServicesCenter webServicesCenter)
        { _webServicesCenter = webServicesCenter; }
        public IEnumerable<Project> Projects { get; private set; } = Array.Empty<Project>();
        public async Task RefreshAsync()
        {
            WebService? projectService = await _webServicesCenter.GetByNameAsync(nameof(ProjectDomain));
            if (projectService == null) { return; }
            Uri getUri = new(projectService.HostUri!, $"/api/{nameof(Project)}s");
            using HttpClient httpClient = new();
            Projects = (await httpClient.GetFromJsonAsync<IEnumerable<Project>>(getUri))!;
        }
    }
}
