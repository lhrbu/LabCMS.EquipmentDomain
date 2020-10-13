using LabCMS.EquipmentDomain.Server.Services;
using LabCMS.ProjectDomain.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReloadCacheController : ControllerBase
    {
        private readonly ReloadCacheService _reloadCacheService;
        public ReloadCacheController(ReloadCacheService reloadCacheService)
        { _reloadCacheService = reloadCacheService; }

        [HttpGet]
        public async Task ReloadCacheAsync() => await _reloadCacheService.ReloadCacheAsync();
        
    }
}
