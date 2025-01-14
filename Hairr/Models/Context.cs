﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hairr.Models
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-ACDH94DG\\SQLEXPRESS01;database=SELENAY;integrated security=true;TrustServerCertificate=True;");
        }

        public DbSet<Personel> Personels { get; set; }
        public DbSet<Islem> Islems { get; set; }


        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Personel -> Appointment İlişkisi
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Personel)
                .WithMany(p => p.Appointments) // Appointments ile ilişkilendirildi
                .HasForeignKey(a => a.PersonelId)
                .OnDelete(DeleteBehavior.Restrict);

            // Islem -> Appointment İlişkisi
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Islem)
                .WithMany(i => i.Appointments) // Appointments ile ilişkilendirildi
                .HasForeignKey(a => a.IslemId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
