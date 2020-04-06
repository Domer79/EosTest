using Eos.Abstracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eos.Data.EF.Configurations
{
    public class GlobalItemConfiguration: IEntityTypeConfiguration<GlobalItem>
    {
        public void Configure(EntityTypeBuilder<GlobalItem> builder)
        {
            builder.ToTable("GlobalItems");
            builder.HasKey(_ => new { _.ParentId, _.ItemId });
            
            builder
                .Property(_ => _.ParentId)
                .IsRequired();

            builder
                .Property(_ => _.ItemId)
                .IsRequired();
            
            builder
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(_ => _.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(_ => _.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}