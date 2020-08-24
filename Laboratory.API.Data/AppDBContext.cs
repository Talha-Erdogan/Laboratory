using Laboratory.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Data
{
    public class AppDBContext : DbContext
    {

        private IConfiguration _config;

        public AppDBContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("AppDbConnectionString"));
            }
        }

        //entities
        public DbSet<Auth> Auth { get; set; }
        public DbSet<Appliance> Appliance { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Lab> Lab { get; set; }
        public DbSet<Move> Move { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileDetail> ProfileDetail { get; set; }
        public DbSet<ProfileEmployee> ProfileEmployee { get; set; }

    }
}
