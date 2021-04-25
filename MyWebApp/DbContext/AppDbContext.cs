using Microsoft.EntityFrameworkCore;
using MyWebApp.Dto;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Products>().HasKey(i => i.Id);
            modelBuilder.Entity<Products>().Property(i=>i.Id).ValueGeneratedNever();
            modelBuilder.Entity<Products>().Property(i=>i.Row).ValueGeneratedOnAdd();
            modelBuilder.Entity<Products>().Property(i=>i.Price).HasColumnType("decimal(18,0)");
            modelBuilder.Entity<Products>().ToTable("Products");
        }
    }
}
