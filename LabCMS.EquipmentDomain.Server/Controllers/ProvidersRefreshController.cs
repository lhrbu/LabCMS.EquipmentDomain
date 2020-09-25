using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LabCMS.EquipmentDomain.Server.Services;

namespace LabCMS.EquipmentDomain.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvidersRefreshController:ControllerBase
    {
        private readonly ProviderRefreshService _service;
        public ProvidersRefreshController(
            ProviderRefreshService service
        ){ _service = service;}
        [HttpPost]
        public async ValueTask PostAsync()=> await _service.RefreshAsync();
    }
}