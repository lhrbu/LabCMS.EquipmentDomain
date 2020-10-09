using LabCMS.FixtureDomain.Shared.Models;
using LabCMS.ProjectDomain.Shared.Models;
using LabCMS.ProjectDomain.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.FixtureDomain.Shared.Services
{
    public class FixtureNoProvider
    {
        private readonly ProjectsWebCacheService _cacheService;
        public FixtureNoProvider(ProjectsWebCacheService cacheService)
        { _cacheService = cacheService; }
        public string? GetNo(Fixture fixture)
        {
            Project? project = _cacheService.CachedProjects.FirstOrDefault(item => item.No == fixture.ProjectNo);
            return $"{project?.Name}-{fixture.Type.ToString().First()}-{fixture.SortId}{fixture.Direction.ToString().First()}";
        }
    }
}
