﻿using Microsoft.EntityFrameworkCore;
using Shoezone.Model;

namespace Shoezone.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)

    {
        public virtual DbSet<Product> Products { get; set; } = null!;

        public virtual DbSet<Customer> Customers { get; set; } = null!;

        public virtual DbSet<Order> Orders { get; set; }=null!;

    }
}
