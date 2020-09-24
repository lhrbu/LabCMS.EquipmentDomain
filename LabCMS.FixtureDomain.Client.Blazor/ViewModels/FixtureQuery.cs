using LabCMS.FixtureDomain.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.FixtureDomain.Client.Blazor.ViewModels
{
    public class FixtureQuery
    {
        public string? No { get; set; }
        public string? ProjectNo { get; set; }
        public string? ProjectName { get; set; }
        public FixtureType? Type { get; set; }
        public Direction? Direction { get; set; }
        public int? SortId { get; set; }
        public int? StockNo { get; set; }
        public int? Floor { get; set; }
    }
}
