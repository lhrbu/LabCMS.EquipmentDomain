using LabCMS.ProjectDomain.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabCMS.ProjectDomain.Server.Repositories
{
    public class ProjectsRepository:DbContext
    {
        public ProjectsRepository(DbContextOptions<ProjectsRepository> options) :
            base(options)
        { }
        public DbSet<Project> Projects { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(item => item.No);
        }

    }
}
