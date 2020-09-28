using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.EquipmentDomain.Server.Repositories
{
    public class EquipmentHourlyRatesRepository:DbContext
    {
        public EquipmentHourlyRatesRepository(DbContextOptions<EquipmentHourlyRatesRepository> options) :
            base(options)
        { }
        public DbSet<EquipmentHourlyRate> EquipmentHourlyRates { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentHourlyRate>()
                .HasKey(item => item.EquipmentNo);
        }
    }
}
