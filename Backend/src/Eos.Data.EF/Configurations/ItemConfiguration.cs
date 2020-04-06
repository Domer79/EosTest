using Eos.Abstracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eos.Data.EF.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(_ => _.ItemId);
            builder.Property(_ => _.ItemId).IsRequired();
            builder.Property(_ => _.ParentId);
            builder.Property(_ => _.Title)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(_ => _.Value).IsRequired();

            builder.HasIndex(_ => _.ParentId);
            builder.HasIndex(_ => _.Title).HasName("IX_Items.Title");

            builder
                .HasOne(_ => _.Parent)
                .WithMany(_ => _.OwnerItems)
                .HasForeignKey(_ => _.ParentId)
                .HasPrincipalKey(_ => _.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
