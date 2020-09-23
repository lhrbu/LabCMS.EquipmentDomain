using LabCMS.FixtureDomain.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.FixtureDomain.Server.Repositories
{
    public class FixturesRepository:DbContext
    {
        public FixturesRepository(DbContextOptions<FixturesRepository> options)
            : base(options) { }
        public DbSet<Fixture> Fixtures { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fixture>().HasKey(item => new{ item.ProjectNo,item.Type,item.SortId,item.Direction});
            modelBuilder.Entity<Fixture>().OwnsOne(item => item.LocationNo);
            modelBuilder.Entity<Fixture>().Property(item => item.Type).HasConversion<string>();
        }
    }
}
