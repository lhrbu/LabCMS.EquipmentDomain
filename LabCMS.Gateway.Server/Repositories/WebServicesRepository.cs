using System;
using LabCMS.Gateway.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LabCMS.Gateway.Server.Repositories
{
    public class WebServicesRepository:DbContext
    {
        public WebServicesRepository(DbContextOptions<WebServicesRepository> options):base(options)
        {}
        public DbSet<WebService> WebServices {get;set;}=null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebService>().HasKey(item=>item.Id);
        }
    }
}