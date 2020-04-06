﻿// <auto-generated />
using System;
using Eos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eos.Data.EF.Migrations
{
    [DbContext(typeof(EosContext))]
    [Migration("20200405083205_create_GlobalItem")]
    partial class create_GlobalItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Eos.Abstracts.Entities.GlobalItem", b =>
                {
                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ParentId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("GlobalItem");
                });

            modelBuilder.Entity("Eos.Abstracts.Entities.Item", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("ParentId");

                    b.HasIndex("Title")
                        .HasName("IX_Items.Title");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Eos.Abstracts.Entities.GlobalItem", b =>
                {
                    b.HasOne("Eos.Abstracts.Entities.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Eos.Abstracts.Entities.Item", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Eos.Abstracts.Entities.Item", b =>
                {
                    b.HasOne("Eos.Abstracts.Entities.Item", "Parent")
                        .WithMany("OwnerItems")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
