using System;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Identity;

namespace Data
{
    public class AusOuvidosContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherProfessionals> VoucherProfessionals { get; set; }

        public AusOuvidosContext(DbContextOptions<AusOuvidosContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Availability>()
                .Property(p => p.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder
               .Entity<Company>()
               .Property(p => p.RowVersion)
               .IsConcurrencyToken()
               .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Voucher>()
                .HasOne(a => a.Company)
                .WithMany(a => a.Vouchers)
                .HasForeignKey(a => a.CompanyId);

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Voucher)
                .WithMany(a => a.Availabilities)
                .HasForeignKey(a => a.VoucherId)
                .IsRequired(false);

            modelBuilder.Entity<VoucherProfessionals>()
                .HasOne(a => a.Voucher)
                .WithMany(a => a.VoucherProfessionals)
                .HasForeignKey(a => a.VoucherId)
                .IsRequired(true);

            modelBuilder.Entity<VoucherProfessionals>()
                .HasOne(a => a.User)
                .WithMany(a => a.VoucherProfessionals)
                .HasForeignKey(a => a.UserId)
                .IsRequired(true);
                
        }
    }
}
