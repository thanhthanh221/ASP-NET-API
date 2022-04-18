using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using BackEnd.Entities;

namespace BackEnd.Repositories
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(enitty => 
            {
                enitty.HasIndex(p => p.Email).IsUnique(); // giá trị phải là duy nhất - Dùng để đánh chỉ mục
            });
        }
        public DbSet<User> users {get; set;}
    }
}