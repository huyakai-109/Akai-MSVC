﻿using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options) { }


        public DbSet<Entities.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Customer>().HasIndex(p => p.UserName).IsUnique();
            modelBuilder.Entity<Entities.Customer>().HasIndex(p => p.Email).IsUnique();
        }
    }
}
