using Identity.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure.EntitiesConfiguration
{
    public class UsersEntityConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(c => c.ID);

            builder.Property(c => c.UserName).IsRequired();
            builder.Property(c => c.Password).IsRequired();
        }
    }
}
