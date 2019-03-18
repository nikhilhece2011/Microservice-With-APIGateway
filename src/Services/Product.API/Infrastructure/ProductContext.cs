using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Product.API.Infrastructure.Entities;
using Product.API.Infrastructure.EntityConfiguration;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Infrastructure
{
    public class ProductContext : DbContext
    {
        private readonly IOptions<GlobalIdentitySettings> _settings;
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {

        }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductsEntityConfiguration());
        }

    }
}
