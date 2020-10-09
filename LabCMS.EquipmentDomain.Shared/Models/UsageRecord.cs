using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using LabCMS.Shared.Models;

namespace LabCMS.EquipmentDomain.Shared.Models
{
    public class UsageRecord
    {
        public Guid? Id {get;set;}
        public string? User { get; set; }
        public string? TestNo {get;set;}
        public string? EquipmentNo {get;set;}
        public string? TestType {get;set;}
        public string? ProjectName {get;set;}

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? StartTime {get;set;}

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? EndTime {get;set;}

        [JsonIgnore]
        public double? Duration => (EndTime-StartTime)?.TotalHours;
    }
}