using _360Accounting.Core;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("_360Accounting")
        {
        }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feature>().ToTable("tbFeature");
            modelBuilder.Entity<Feature>().HasKey(t => new { t.Id });

            modelBuilder.Entity<Company>().ToTable("tbCompany");
            modelBuilder.Entity<Company>().HasKey(t => new { t.Id });

            ////modelBuilder.Entity<Account>().ToTable("tbChartOfAccount");
            ////modelBuilder.Entity<Account>().HasKey(t => new { t.Id });
        }
    }
}