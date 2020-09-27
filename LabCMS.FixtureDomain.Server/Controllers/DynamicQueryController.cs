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
    public class DynamicQueryController:ControllerBase
    {
        private readonly DynamicQueryService _queryService;
        private readonly FixturesRepository _repository;
        public DynamicQueryController(
            DynamicQueryService queryService,
            FixturesRepository repository)
        {
            _queryService = queryService;
            _repository = repository;
        }

        [HttpPost]
        public async ValueTask<IEnumerable<Fixture>> PostAsync(string code)
        {
            string expression = $"(IEnumerable<Fixture> items)=>{code}";
            return await _queryService.QueryAsync(
                _repository.Fixtures.AsNoTracking().AsEnumerable(),
                expression
            );
        }
    }
}