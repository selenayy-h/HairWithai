﻿// <auto-generated />
using System;
using Hairr.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hairr.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241218103018_AddPersonelUygunlukGun")]
    partial class AddPersonelUygunlukGun
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hairr.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Role")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Hairr.Models.Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<int>("PersonelId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("IslemId");

                    b.HasIndex("PersonelId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Hairr.Models.Islem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("IslemAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Islems");
                });

            modelBuilder.Entity("Hairr.Models.Personel", b =>
                {
                    b.Property<int>("PersonelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonelId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<string>("Sehir")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("UygunlukBaslangic")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("UygunlukBitis")
                        .HasColumnType("time");

                    b.HasKey("PersonelId");

                    b.HasIndex("IslemId");

                    b.ToTable("Personels");
                });

            modelBuilder.Entity("Hairr.Models.PersonelUygunlukGun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Gun")
                        .HasColumnType("int");

                    b.Property<int>("PersonelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonelId");

                    b.ToTable("PersonelUygunlukGunler");
                });

            modelBuilder.Entity("Hairr.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hairr.Models.Appointment", b =>
                {
                    b.HasOne("Hairr.Models.Islem", "Islem")
                        .WithMany("Appointments")
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hairr.Models.Personel", "Personel")
                        .WithMany("Appointments")
                        .HasForeignKey("PersonelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Islem");

                    b.Navigation("Personel");
                });

            modelBuilder.Entity("Hairr.Models.Personel", b =>
                {
                    b.HasOne("Hairr.Models.Islem", "Islem")
                        .WithMany("Personels")
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Islem");
                });

            modelBuilder.Entity("Hairr.Models.PersonelUygunlukGun", b =>
                {
                    b.HasOne("Hairr.Models.Personel", "Personel")
                        .WithMany("UygunlukGunler")
                        .HasForeignKey("PersonelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personel");
                });

            modelBuilder.Entity("Hairr.Models.Islem", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Personels");
                });

            modelBuilder.Entity("Hairr.Models.Personel", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("UygunlukGunler");
                });
#pragma warning restore 612, 618
        }
    }
}
