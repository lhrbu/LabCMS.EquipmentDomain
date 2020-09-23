using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.FixtureDomain.Shared.Models
{
    public class LocationNo
    {
        public int StockNo { get; set; }
        public int Floor { get; set; }
        public override string ToString() => $"{StockNo}-{Floor}";
    }
}
