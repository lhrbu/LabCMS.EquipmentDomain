using System;
using System.Net.Http;

namespace LabCMS.Gateway.Shared.Models
{
    public class WebService
    {
        public Guid? Id {get;set;}
        public string? Name {get;set;}
        public Uri? Uri {get;set;}
        public HttpMethod? HttpMethod {get;set;}
    }
}