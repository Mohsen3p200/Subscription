using Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistance.EFCore.EntityTypeConfiguration
{
    public class UserETC : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Firstname).IsRequired();
            builder.Property(p => p.Lastname).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.Timestamp).IsRequired();
        }
    }
}
