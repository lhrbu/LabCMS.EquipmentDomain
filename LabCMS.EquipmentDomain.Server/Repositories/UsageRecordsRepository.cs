using LabCMS.EquipmentDomain.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabCMS.Shared.Models;

namespace LabCMS.EquipmentDomain.Server.Repositories
{
    public class UsageRecordsRepository:DbContext
    {
        public UsageRecordsRepository(DbContextOptions<UsageRecordsRepository> options):
            base(options)
        { }
        public DbSet<UsageRecord> UsageRecords { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsageRecord>()
                .HasKey(item => item.Id);
            modelBuilder.Entity<UsageRecord>()
                .Property(item => item.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UsageRecord>()
                .Property(item => item.StartTime)
                .HasConversion(EntityFrameworkCoreValueConverters.DataTimeOffsetUtcSecondsConverter);
            modelBuilder.Entity<UsageRecord>()
                .Property(item => item.EndTime)
                .HasConversion(EntityFrameworkCoreValueConverters.DataTimeOffsetUtcSecondsConverter);
        }
    }
}
