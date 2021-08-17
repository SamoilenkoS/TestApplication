﻿using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class EFCoreContext : DbContext
    {
        public DbSet<WeatherForecastDTO> WeatherForecasts { get; set;}
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<EmailDTO> Emails { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public EFCoreContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoles>()
                .HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
