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
    public static class ProjectProvider
    {
        public static IEnumerable<Project> Projects { get; private set; }
            = new List<Project>();

        public static async Task RefreshAsync()
        {
            Uri uri = UriProvider.GetUri(nameof(Project));
            using HttpClient httpClient = new();
            Projects = (await httpClient.GetFromJsonAsync<IEnumerable<Project>>(uri))!;
        }
        
    }
}
