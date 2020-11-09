using LabCMS.ProjectDomain.Server.Repositories;
using LabCMS.ProjectDomain.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace LabCMS.ProjectDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ProjectsRepository _repository;
        public ProjectsController(
            ProjectsRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public IAsyncEnumerable<Project> GetAsync() => _repository.Projects.AsNoTracking().AsAsyncEnumerable();

        [HttpPost]
        public async ValueTask PostAsync(Project project)
        {
            await _repository.Projects.AddAsync(project);
            await _repository.SaveChangesAsync();
        }

        [HttpPut]
        public async ValueTask PutAsync(Project project)
        {
            _repository.Projects.Update(project);
            await _repository.SaveChangesAsync();
        }

        [HttpDelete("{projectName}")]
        public async ValueTask<ActionResult> DeleteByNameAsync(string projectName)
        {
            using HttpClient client = new();
            string gatewayUrls = _configuration["GatewayUrls"];
            Uri getUri = new($"{gatewayUrls}/api/UsageRecords");
            IEnumerable<string> usageRecordProjectNamesPayload = 
                (await client.GetFromJsonAsync<IEnumerable<UsageRecordWithProjectNamePayload>>(getUri))!.Select(item=>item.ProjectName!);
            

            if (usageRecordProjectNamesPayload.Any(item => item == projectName))
            { return BadRequest();}
            else
            {
                Project? project = _repository.Projects.FirstOrDefault(item=>item.FullName==projectName);
                if (project is not null)
                {
                    _repository.Projects.Remove(project);
                    await _repository.SaveChangesAsync();
                }
                return Ok();
            }
        }
    }

    public class UsageRecordWithProjectNamePayload
    {
        public string? ProjectName {get;set;}
    }
}
