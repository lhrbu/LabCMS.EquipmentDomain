using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabCMS.FixtureDomain.Shared.Models;
using LabCMS.FixtureDomain.Server.Services;
using LabCMS.FixtureDomain.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LabCMS.FixtureDomain.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FixturesController:ControllerBase
    {
        private readonly FixturesRepository _repository;
        public FixturesController(FixturesRepository repository)
        { _repository = repository;}

        [HttpGet]
        public IAsyncEnumerable<Fixture> GetAsync()=>_repository.Fixtures.AsNoTracking().AsAsyncEnumerable();
        
        [HttpPost]
        public async ValueTask PostAsync(Fixture fixture)
        {
            await _repository.Fixtures.AddAsync(fixture);
            await _repository.SaveChangesAsync();
        }

        [HttpPut]
        public async ValueTask PutAsync(Fixture fixture)
        {
            _repository.Fixtures.Update(fixture);
            await _repository.SaveChangesAsync();
        }
    }
}