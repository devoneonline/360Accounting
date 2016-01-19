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

        public DbSet<FeatureSet> FeatureSets { get; set; }

        public DbSet<FeatureSetList> FeatureSetLists { get; set; }

        public DbSet<FeatureSetAccess> FeatureSetAccesses { get; set; }

        #endregion

        #region Binders

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feature>().ToTable("tbFeature");
            modelBuilder.Entity<Feature>().HasKey(t => new { t.Id });

            modelBuilder.Entity<Company>().ToTable("tbCompany");
            modelBuilder.Entity<Company>().HasKey(t => new { t.Id });
                
            modelBuilder.Entity<SetOfBook>().ToTable("tbSetOfBook");
            modelBuilder.Entity<SetOfBook>().HasKey(t => t.Id);
                                    
            modelBuilder.Entity<Account>().ToTable("tbChartOfAccount");
            modelBuilder.Entity<Account>().HasKey(t => new { t.Id });

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