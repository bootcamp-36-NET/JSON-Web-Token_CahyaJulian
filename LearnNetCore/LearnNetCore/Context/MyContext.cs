using LearnNetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Division> divisions { get;set; }
        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            
            builder.Entity<Employee>()
                .HasOne<User>(s => s.Users)
                .WithOne(ad => ad.Employees)
                .HasForeignKey<Employee>(ad => ad.Id);

            base.OnModelCreating(builder);
        }
    }
}
