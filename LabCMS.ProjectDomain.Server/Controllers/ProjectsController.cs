using LabCMS.ProjectDomain.Server.Repositories;
using LabCMS.ProjectDomain.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.ProjectDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsRepository _repository;
        public ProjectsController(
            ProjectsRepository repository)
        { _repository = repository; }

        [HttpGet]
        public IAsyncEnumerable<Project> GetAsync() => _repository.Projects.AsNoTracking().AsAsyncEnumerable();
    
        [HttpPost]
        public async ValueTask PostAsync(Project project)
        {
            await _repository.Projects.AddAsync(project);
            await _repository.SaveChangesAsync();
        }

        public async ValueTask PutAsync(Project project)
        {
            _repository.Projects.Update(project);
            await _repository.SaveChangesAsync();
        }
    }
}
