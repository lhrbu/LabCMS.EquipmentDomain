using System;

namespace LabCMS.Tools.MigrationHelper.Models
{
    public class ArchiveUsageRecord
    {
        public Guid? Id{get;set;}
        public string? User {get;set;}
        public string? EquipmentNo{get;set;}
        public string? TestNo{get;set;}
        public string? ProjectName{get;set;}
        public string? StartTime {get;set;}
        public string? EndTime {get;set;}
        public string? TestType {get;set;}
    }
}