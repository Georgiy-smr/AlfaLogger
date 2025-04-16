using Microsoft.EntityFrameworkCore;
using System;
using Data.Entities;
using Data.Enums;

namespace ContextEf
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Log>()
                .Property(e => e.TypeEvent)
                .HasConversion<string>();
            modelBuilder.Entity<Log>().HasKey(log => log.Id);
            modelBuilder.Entity<Log>().HasIndex(log => log.Id);
        }

        public DbSet<Log> Logs { get; set; }
    }
}
