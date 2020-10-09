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
    [Route("api/[controller]")]
    public class FixturesController:ControllerBase
    {
        private readonly DynamicQueryService _queryService;
        private readonly FixturesRepository _repository;
        public FixturesController(
            DynamicQueryService queryService,
            FixturesRepository repository)
        {
            _queryService = queryService; 
            _repository = repository;
        }

        [HttpGet]
        public IAsyncEnumerable<Fixture> GetAsync()=>_repository.Fixtures.AsNoTracking().AsAsyncEnumerable();
        
        [HttpPost]
        public async ValueTask PostAsync(Fixture fixture)
        {
            await _repository.Fixtures.AddAsync(fixture);
            await _repository.SaveChangesAsync();
        }

        [HttpPost("DynamicQuery")]
        public async ValueTask<IEnumerable<Fixture>> PostAsync(string code)
        {
            string expression = $"(IEnumerable<Fixture> items)=>{code}";
            return await _queryService.QueryAsync(
                _repository.Fixtures.AsNoTracking().AsEnumerable(),
                expression
            );
        }

        [HttpPut]
        public async ValueTask PutAsync(Fixture fixture)
        {
            _repository.Fixtures.Update(fixture);
            await _repository.SaveChangesAsync();
        }
    }
}