using LabCMS.EquipmentDomain.Server.Repositories;
using LabCMS.EquipmentDomain.Server.Services;
using LabCMS.EquipmentDomain.Shared.Models;
using LabCMS.EquipmentDomain.Shared.Services;
using LabCMS.ProjectDomain.Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Models
{
    public class UsageRecordInExcel
    {
        private readonly UsageRecord _usageRecord;
        private readonly ProjectsWebCacheService _projectsWebCacheService;
        private readonly EquipmentHourlyRatesLocalCacheService _equipmentHourlyRatesLocalCacheService;
        public UsageRecordInExcel(
            UsageRecord usageRecord,
            ProjectsWebCacheService projectsWebCacheService,
            EquipmentHourlyRatesLocalCacheService equipmentHourlyRatesLocalCacheService
            )
        { 
            _usageRecord = usageRecord;
            _projectsWebCacheService = projectsWebCacheService;
            _equipmentHourlyRatesLocalCacheService = equipmentHourlyRatesLocalCacheService;
        }

        public string? User => _usageRecord.User;

        [DisplayName("Test No")]
        public string? TestNo => _usageRecord.TestNo;

        [DisplayName("Equipment No")]
        public string? EquipmentNo => _usageRecord.EquipmentNo;

        [DisplayName("Equipment Name")]
        public string? EquipmentName => _equipmentHourlyRatesLocalCacheService.CachedEquipmentHourlyRates
            .FirstOrDefault(item => item.EquipmentNo == EquipmentNo)?.EquipmentName;

        [DisplayName("Test Type")]
        public string? TestType => _usageRecord.TestType;

        [DisplayName("Project No")]
        public string? ProjectNo => _projectsWebCacheService.CachedProjects
            .FirstOrDefault(item => item.FullName == ProjectName)?.No;

        [DisplayName("Project Name")]
        public string? ProjectName => _usageRecord.ProjectName;


        [DisplayName("Start Time")]
        //public string? StartTimeString => StartTime?.ToLocalTime().ToString("G").Replace(" ", "  ");
        public DateTime? StartTimeString => StartTime?.LocalDateTime;
        private DateTimeOffset? StartTime => _usageRecord.StartTime;


        [DisplayName("End Time")]
        public DateTime? EndTimeString => EndTime?.LocalDateTime;

        private DateTimeOffset? EndTime => _usageRecord.EndTime;
        public double? Duration => (EndTime.HasValue && StartTime.HasValue) ? 
            Math.Round((EndTime.Value - StartTime.Value).TotalHours, 2) : 
            null;

        [DisplayName("Machine Category")]
        public string? MachineCategory => _equipmentHourlyRatesLocalCacheService.CachedEquipmentHourlyRates
            .FirstOrDefault(item => item.EquipmentNo == EquipmentNo)?.MachineCategory;

        [DisplayName("Hourly Rate")]
        public string? HourlyRate => _equipmentHourlyRatesLocalCacheService.CachedEquipmentHourlyRates
            .FirstOrDefault(item => item.EquipmentNo == EquipmentNo)?.HourlyRate;
    }
}
