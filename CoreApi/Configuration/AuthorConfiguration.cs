using CoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .Property(x => x.FirstName)
                .HasMaxLength(200);
            builder
                .Property(x => x.LastName)
                .HasMaxLength(200);
            builder
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn();
            builder
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
