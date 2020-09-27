using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;

namespace LabCMS.Gateway.Shared.Models
{
    public class WebService
    {
        public Guid? Id {get;set;}
        public string? Name {get;set;}
        public Uri? HostUri {get;set;}
    }
}