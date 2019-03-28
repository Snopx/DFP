﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DFDbContext))]
    partial class DFDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Model.Article", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleType");

                    b.Property<string>("Author")
                        .HasMaxLength(30);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("ImgUrl")
                        .HasMaxLength(80);

                    b.Property<string>("Remark");

                    b.Property<string>("Summary")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("ID");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Domain.Model.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Domain.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Account")
                        .HasMaxLength(10);

                    b.Property<string>("Email")
                        .HasMaxLength(30);

                    b.Property<int>("Gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Personality")
                        .HasMaxLength(80);

                    b.HasKey("ID");

                    b.HasIndex("Account")
                        .IsUnique()
                        .HasFilter("[Account] IS NOT NULL");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Account = "admin",
                            Gender = 0,
                            Name = "DarrenFang",
                            Password = "Ki0t+7zyx5fGIVXerifvzQ=="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
