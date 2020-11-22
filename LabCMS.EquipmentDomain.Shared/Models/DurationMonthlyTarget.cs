using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Shared.Models
{
    public class DurationMonthlyTarget
    {
        public DateTimeOffset StartTime { get; set; }
        public double Target { get; set; }
    }
}
