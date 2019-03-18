using Identity.API.Infrastructure.Entities;
using Identity.API.Infrastructure.EntitiesConfiguration;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    public class IdentityDbContext : DbContext
    {


        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsersEntityConfiguration());
        }

    }
}
