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
        #region Constructor
        public ApplicationDbContext()
            : base("_360Accounting")
        {
        }
        #endregion

        #region DbSets

        public DbSet<SetOfBook> SetOfBooks { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountValue> AccountValues { get; set; }

        public DbSet<FeatureSet> FeatureSets { get; set; }

        public DbSet<FeatureSetList> FeatureSetLists { get; set; }

        public DbSet<FeatureSetAccess> FeatureSetAccesses { get; set; }

        public DbSet<CodeCombinition> CodeCombinitions { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<JournalVoucher> JournalVouchers { get; set; }

        public DbSet<JournalVoucherDetail> JournalVoucherDetails { get; set; }

        #endregion

        #region Binders

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JournalVoucherDetail>().ToTable("tbGLLines");
            modelBuilder.Entity<JournalVoucherDetail>().HasKey(t => t.Id);
            
            modelBuilder.Entity<JournalVoucher>().ToTable("tbGLHeader");
            modelBuilder.Entity<JournalVoucher>().HasKey(t => t.Id);
            
            modelBuilder.Entity<Calendar>().ToTable("tbCalendar");
            modelBuilder.Entity<Calendar>().HasKey(t => t.Id);

            modelBuilder.Entity<Currency>().ToTable("tbCurrency");
            modelBuilder.Entity<Currency>().HasKey(t => t.Id);

            modelBuilder.Entity<CodeCombinition>().ToTable("tbCodeCombinition");
            modelBuilder.Entity<CodeCombinition>().HasKey(t => t.Id);

            modelBuilder.Entity<Feature>().ToTable("tbFeature");
            modelBuilder.Entity<Feature>().HasKey(t => new { t.Id });

            modelBuilder.Entity<Company>().ToTable("tbCompany");
            modelBuilder.Entity<Company>().HasKey(t => new { t.Id });

            modelBuilder.Entity<SetOfBook>().ToTable("tbSetOfBook");
            modelBuilder.Entity<SetOfBook>().HasKey(t => t.Id);

            modelBuilder.Entity<Account>().ToTable("tbChartOfAccount");
            modelBuilder.Entity<Account>().HasKey(t => new { t.Id });

            modelBuilder.Entity<AccountValue>().ToTable("tbChartOfAccountValues");
            modelBuilder.Entity<AccountValue>().HasKey(t => new { t.Id });

            modelBuilder.Entity<FeatureSet>().ToTable("tbFeatureSet");
            modelBuilder.Entity<FeatureSet>().HasKey(t => t.Id);

            modelBuilder.Entity<FeatureSetList>().ToTable("tbFeatureSetList");
            modelBuilder.Entity<FeatureSetList>().HasKey(t => t.Id);

            modelBuilder.Entity<FeatureSetAccess>().ToTable("dbo.tbFeatureSetAccess");
            modelBuilder.Entity<FeatureSetAccess>().HasKey(t => t.Id);
        }

        #endregion
    }
}