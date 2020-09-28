using LabCMS.FixtureDomain.Shared.Models.Enums;
using LabCMS.ProjectDomain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabCMS.ProjectDomain.Shared.Services;
using System.Text.Json.Serialization;

namespace LabCMS.FixtureDomain.Shared.Models
{
    public class Fixture
    {
        public Guid? Id { get; set; }
        public string? ProjectNo { get; set; }
        public FixtureType Type { get; set; }
        public Direction Direction { get; set; }
        public int SortId { get; set; }
        public LocationNo? LocationNo { get; set; }
        public string? Remark { get; set; }
    }
}
