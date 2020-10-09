using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.Gateway.Shared.Services
{
    public class RemoteIPAddressProvider
    {
        public string? GetAddress(HttpContext httpContext)
        {
            if(httpContext.Request.Headers.ContainsKey("X-Real-IP")){
                return httpContext.Request.Headers["X-Real-IP"];
            }else{
                return httpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
            }
        }
    }
}