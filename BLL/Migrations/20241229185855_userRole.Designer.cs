// <auto-generated />
using System;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BLL.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20241229185855_userRole.Designer")]
    partial class UserRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Role", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("nvarchar(10)");

                b.HasKey("Id");

                b.ToTable("Roles");

                // Seed data for Roles
                b.HasData(
                    new { Id = 1, Name = "Admin" },
                    new { Id = 2, Name = "User" }
                );
            });

            modelBuilder.Entity("User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("UserName")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnType("nvarchar(12)");

                b.Property<bool>("isActive")
                    .HasColumnType("bit");

                b.Property<int?>("RoleId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("Users");
            });

            modelBuilder.Entity("User", b =>
            {
                b.HasOne("Role", "Role")
                    .WithMany()
                    .HasForeignKey("RoleId");
            });
#pragma warning restore 612, 618
        }
    }
}