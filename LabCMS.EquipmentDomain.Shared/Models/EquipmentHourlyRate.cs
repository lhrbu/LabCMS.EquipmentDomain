using System;
using System.Collections.Generic;

namespace LabCMS.EquipmentDomain.Shared.Models
{
    public class EquipmentHourlyRate
    {
        public string? EquipmentNo { get; set; }
        public string? EquipmentName { get; set; }        
        public string? MachineCategory { get; set; }
        public string? HourlyRate { get; set; }
    }
}