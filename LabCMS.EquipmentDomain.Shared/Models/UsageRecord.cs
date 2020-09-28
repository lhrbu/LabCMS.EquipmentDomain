using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using LabCMS.EquipmentDomain.Shared.Services;
using LabCMS.ProjectDomain.Shared.Models;
using LabCMS.ProjectDomain.Shared.Services;

namespace LabCMS.EquipmentDomain.Shared.Models
{
    public class UsageRecord
    {
        public Guid? Id {get;set;}
        public string? TestNo {get;set;}
        public string? EquipmentNo {get;set;}
        public string? TestType {get;set;}
        public string? ProjectName {get;set;}
        public DateTimeOffset? StartTime {get;set;}
        public DateTimeOffset? EndTime {get;set;}
        public double? Duration => (EndTime-StartTime)?.TotalHours;
    }
}