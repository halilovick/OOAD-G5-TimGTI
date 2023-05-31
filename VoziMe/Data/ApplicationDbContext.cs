using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VoziMe.Models;

namespace VoziMe.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
       options)
        : base(options) {
        }
        public DbSet<Osoba> Osoba { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Klijent> Klijent { get; set; }
        public DbSet<Vozac> Vozac { get; set; }
        public DbSet<Firma> Firma { get; set; }
        public DbSet<Vozilo> Vozilo { get; set; }
        public DbSet<Voznje> Voznje { get; set; }
        public DbSet<TaxiStajaliste> TaxiStajaliste { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Osoba>().ToTable("Osoba");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Klijent>().ToTable("Klijent");
            modelBuilder.Entity<Vozac>().ToTable("Vozac");
            modelBuilder.Entity<Firma>().ToTable("Firma");
            modelBuilder.Entity<Vozilo>().ToTable("Vozilo");
            modelBuilder.Entity<Voznje>().ToTable("Voznje");
            modelBuilder.Entity<TaxiStajaliste>().ToTable("TaxiStajaliste");
            base.OnModelCreating(modelBuilder);
        }
    }
}
