using FishingApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Data.Context
{
    public class FishingDbContext : DbContext
    {
        public FishingDbContext(DbContextOptions<FishingDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Fishing> Fishings { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Discription> Discriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Fishings)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Fishing>()
                .HasMany(f => f.Fishes)
                .WithOne(fh => fh.Fishing)
                .HasForeignKey(fh => fh.FishingId);

            modelBuilder.Entity<Fish>()
                .HasOne(f => f.Discription)
                .WithOne(d => d.Fish)
                .HasForeignKey<Discription>(d => d.FishId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
