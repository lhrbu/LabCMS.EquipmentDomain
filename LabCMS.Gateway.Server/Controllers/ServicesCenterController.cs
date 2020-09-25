using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LabCMS.Gateway.Shared.Models;
using LabCMS.Gateway.Server.Repositories;

namespace LabCMS.Gateway.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesCenterController:ControllerBase
    {
        private readonly ServicesCenterRepository _repository;
        public ServicesCenterController(ServicesCenterRepository repository)
        {
            _repository=repository;
        }

        [HttpGet]
        public IAsyncEnumerable<WebService> GetAsync()=>_repository.WebServices.AsNoTracking().AsAsyncEnumerable();

        [HttpGet("{name}")]
        public IAsyncEnumerable<WebService> GetByNameAsync(string name)=>_repository.WebServices.AsNoTracking()
            .Where(item=>item.Name==name).AsAsyncEnumerable();

        [HttpPost]
        public async ValueTask PostAsync(WebService service)
        {
            await _repository.WebServices.AddAsync(service);
            await _repository.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async ValueTask DeleteByIdAsync(Guid id)
        {
            WebService? service = await _repository.WebServices.FindAsync(id);
            if(service!=null)
            {
                _repository.WebServices.Remove(service);
                await _repository.SaveChangesAsync();
            }
        }
    }
}