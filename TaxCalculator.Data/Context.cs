using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Data.Interfaces;
using TaxCalculator.Model;

namespace TaxCalculator.Data
{
    public class Context : DbContext
    {
        /// <summary>
        /// Settings
        /// </summary>
        private readonly AppSettings _settings;
        public Context(IOptions<AppSettings> Configuration, DbContextOptions<Context> options) : base(options)
        {
        }      

        public virtual DbSet<PostalCodeDetail> PostalCodeDetails { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }
        public virtual DbSet<TaxCalculatedValue> TaxCalculatedValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCodeDetail>().ToTable("PostalCodeDetails");
            modelBuilder.Entity<TaxRate>(entity => {
                entity.HasKey(e => e.RatesID).HasName("RatesID");

                entity.Property(e => e.From).HasColumnName("From");
                entity.Property(e => e.To).HasColumnName("To");
                entity.Property(e => e.Rate).HasColumnName("Rate");


                entity.HasOne(e => e.TaxCalculationType).WithMany(e => e.TaxRates).HasForeignKey(e => e.FK_TaxCalculationID);
            });
            modelBuilder.Entity<TaxCalculationType>(entity => {
                entity.HasKey(e => e.TaxCalculationID).HasName("TaxCalculationID");
                entity.Property(e => e.TaxType).HasColumnName("TaxType");
            });
            modelBuilder.Entity<TaxCalculatedValue>().ToTable("TaxCalculatedValues");
        }
    }
}
