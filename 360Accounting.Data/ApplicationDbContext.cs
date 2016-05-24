using _360Accounting.Core;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public DbSet<Requisition> Requisitions { get; set; }

        public DbSet<RequisitionDetail> RequisitionDetails { get; set; }

        public DbSet<RFQDetail> RFQDetails { get; set; }

        public DbSet<RFQ> RFQs { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<PurchasingPeriod> PurchasingPeriods { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderType> OrderTypes { get; set; }

        public DbSet<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }

        public DbSet<MoveOrder> MoveOrders { get; set; }

        public DbSet<MoveOrderDetail> MoveOrderDetails { get; set; }

        public DbSet<LotNumber> LotNumbers { get; set; }

        public DbSet<SerialNumber> SerialNumbers { get; set; }

        public DbSet<ReceivablePeriod> ReceivablePeriods { get; set; }

        public DbSet<LocatorWarehouse> LocatorWarehouses { get; set; }

        public DbSet<Locator> Locators { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<InventoryPeriod> InventoryPeriods { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemWarehouse> ItemWarehouses { get; set; }

        public DbSet<PayableInvoiceDetail> PayableInvoiceDetails { get; set; }

        public DbSet<PayableInvoice> PayableInvoices { get; set; }

        public DbSet<Remittance> Remittances { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<TaxDetail> TaxDetails { get; set; }

        public DbSet<Tax> Taxes { get; set; }

        public DbSet<InvoiceSource> InvoiceSources { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerSite> CustomerSites { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<VendorSite> VendorSites { get; set; }

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

        public DbSet<aspnet_User> Users { get; set; }

        public DbSet<GLHeader> GLHeaders { get; set; }

        public DbSet<GLLines> GLLines { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<PayablePeriod> PayablePeriods { get; set; }

        public DbSet<Withholding> Withholdings { get; set; }

        public DbSet<InvoiceType> InvoiceTypes { get; set; }

        public DbSet<PaymentHeader> PaymentHeaders { get; set; }

        public DbSet<PaymentInvoiceLines> PaymentInvoiceLines { get; set; }

        public DbSet<UserSetofBook> UserSetofBooks { get; set; }

        #endregion

        #region Binders

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseOrder>().ToTable("tbPurchaseOrder");
            modelBuilder.Entity<PurchaseOrder>().HasKey(t => t.Id);

            modelBuilder.Entity<PurchaseOrderDetail>().ToTable("tbPurchaseOrderDetail");
            modelBuilder.Entity<PurchaseOrderDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<Requisition>().ToTable("tbRequisition");
            modelBuilder.Entity<Requisition>().HasKey(t => t.Id);

            modelBuilder.Entity<RequisitionDetail>().ToTable("tbRequisitionDetail");
            modelBuilder.Entity<RequisitionDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<RFQDetail>().ToTable("tbRFQDetail");
            modelBuilder.Entity<RFQDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<RFQ>().ToTable("tbRFQ");
            modelBuilder.Entity<RFQ>().HasKey(t => t.Id);

            modelBuilder.Entity<Buyer>().ToTable("tbBuyer");
            modelBuilder.Entity<Buyer>().HasKey(t => t.Id);

            modelBuilder.Entity<PurchasingPeriod>().ToTable("tbPurchasingPeriod");
            modelBuilder.Entity<PurchasingPeriod>().HasKey(t => t.Id);

            modelBuilder.Entity<Shipment>().ToTable("tbShipment");
            modelBuilder.Entity<Shipment>().HasKey(t => t.Id);

            modelBuilder.Entity<OrderType>().ToTable("tbOrderType");
            modelBuilder.Entity<OrderType>().HasKey(t => t.Id);

            modelBuilder.Entity<Order>().ToTable("tbOrderMaster");
            modelBuilder.Entity<Order>().HasKey(t => t.Id);

            modelBuilder.Entity<OrderDetail>().ToTable("tbOrderDetail");
            modelBuilder.Entity<OrderDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<MiscellaneousTransaction>().ToTable("tbMiscellaneousTransaction");
            modelBuilder.Entity<MiscellaneousTransaction>().HasKey(t => t.Id);

            modelBuilder.Entity<MoveOrder>().ToTable("tbMoveOrder");
            modelBuilder.Entity<MoveOrder>().HasKey(t => t.Id);

            modelBuilder.Entity<MoveOrderDetail>().ToTable("tbMoveOrderDetail");
            modelBuilder.Entity<MoveOrderDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<LotNumber>().ToTable("tbLotNumber");
            modelBuilder.Entity<LotNumber>().HasKey(t => t.Id);

            modelBuilder.Entity<SerialNumber>().ToTable("tbSerialNumber");
            modelBuilder.Entity<SerialNumber>().HasKey(t => t.Id);

            modelBuilder.Entity<ReceivablePeriod>().ToTable("tbReceivablePeriod");
            modelBuilder.Entity<ReceivablePeriod>().HasKey(t => t.Id);

            modelBuilder.Entity<Locator>().ToTable("tbLocator");
            modelBuilder.Entity<Locator>().HasKey(t => t.Id);

            modelBuilder.Entity<LocatorWarehouse>().ToTable("tbLocatorWarehouse");
            modelBuilder.Entity<LocatorWarehouse>().HasKey(t => t.Id);

            modelBuilder.Entity<Warehouse>().ToTable("tbWarehouse");
            modelBuilder.Entity<Warehouse>().HasKey(t => t.Id);

            modelBuilder.Entity<InventoryPeriod>().ToTable("tbInventoryPeriod");
            modelBuilder.Entity<InventoryPeriod>().HasKey(t => t.Id);

            modelBuilder.Entity<Item>().ToTable("tbItem");
            modelBuilder.Entity<Item>().HasKey(t => t.Id);

            modelBuilder.Entity<ItemWarehouse>().ToTable("tbItemWarehouse");
            modelBuilder.Entity<ItemWarehouse>().HasKey(t => t.Id);

            modelBuilder.Entity<PayableInvoice>().ToTable("tbPayableInvoice");
            modelBuilder.Entity<PayableInvoice>().HasKey(t => t.Id);

            modelBuilder.Entity<PayableInvoiceDetail>().ToTable("tbPayableInvoiceDetail");
            modelBuilder.Entity<PayableInvoiceDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<Remittance>().ToTable("tbRemittance");
            modelBuilder.Entity<Remittance>().HasKey(t => t.Id);

            modelBuilder.Entity<Invoice>().ToTable("tbInvoice");
            modelBuilder.Entity<Invoice>().HasKey(t => t.Id);

            modelBuilder.Entity<InvoiceDetail>().ToTable("tbInvoiceDetail");
            modelBuilder.Entity<InvoiceDetail>().HasKey(t => t.Id);

            modelBuilder.Entity<Tax>().ToTable("tbTax");
            modelBuilder.Entity<Tax>().HasKey(t => t.Id);

            modelBuilder.Entity<TaxDetail>().ToTable("tbTaxDetail");
            modelBuilder.Entity<TaxDetail>().HasKey(t => t.Id);
            
            modelBuilder.Entity<InvoiceSource>().ToTable("tbInvoiceSource");
            modelBuilder.Entity<InvoiceSource>().HasKey(t => t.Id);

            modelBuilder.Entity<Customer>().ToTable("tbCustomer");
            modelBuilder.Entity<Customer>().HasKey(t => t.Id);

            modelBuilder.Entity<CustomerSite>().ToTable("tbCustomerSite");
            modelBuilder.Entity<CustomerSite>().HasKey(t => t.Id);

            modelBuilder.Entity<Vendor>().ToTable("tbVendor");
            modelBuilder.Entity<Vendor>().HasKey(t => t.Id);

            modelBuilder.Entity<VendorSite>().ToTable("tbVendorSite");
            modelBuilder.Entity<VendorSite>().HasKey(t => t.Id);

            //modelBuilder.Entity<TaxDum>().ToTable("tbTaxDum");
            //modelBuilder.Entity<TaxDum>().HasKey(t => t.Id);

            modelBuilder.Entity<aspnet_User>().ToTable("aspnet_Users");
            modelBuilder.Entity<aspnet_User>().HasKey(t => t.UserId);

            //modelBuilder.Entity<JournalVoucherDetail>().ToTable("tbGLLines");
            //modelBuilder.Entity<JournalVoucherDetail>().HasKey(t => t.Id);

            //modelBuilder.Entity<JournalVoucher>().ToTable("tbGLHeader");
            //modelBuilder.Entity<JournalVoucher>().HasKey(t => t.Id);


            modelBuilder.Entity<GLHeader>().ToTable("tbGLHeader");
            modelBuilder.Entity<GLHeader>().HasKey(t => t.Id);

            modelBuilder.Entity<GLLines>().ToTable("tbGLLines");
            modelBuilder.Entity<GLLines>().HasKey(t => t.Id);
            
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

            modelBuilder.Entity<Bank>().ToTable("tbBank");
            modelBuilder.Entity<Bank>().HasKey(t => t.Id);

            modelBuilder.Entity<BankAccount>().ToTable("tbBankAccount");
            modelBuilder.Entity<BankAccount>().HasKey(t => t.Id);

            modelBuilder.Entity<Receipt>().ToTable("dbo.tbReceipt");
            modelBuilder.Entity<Receipt>().HasKey(t => t.Id);

            modelBuilder.Entity<PayablePeriod>().ToTable("tbPayablePeriod");
            modelBuilder.Entity<PayablePeriod>().HasKey(t => t.Id);

            modelBuilder.Entity<Withholding>().ToTable("tbWithholding");
            modelBuilder.Entity<Withholding>().HasKey(t => t.Id);

            modelBuilder.Entity<InvoiceType>().ToTable("tbInvoiceType");
            modelBuilder.Entity<InvoiceType>().HasKey(t => t.Id);

            modelBuilder.Entity<PaymentHeader>().ToTable("tbPaymentHeader");
            modelBuilder.Entity<PaymentHeader>().HasKey(t => t.Id);

            modelBuilder.Entity<PaymentInvoiceLines>().ToTable("tbPaymentInvoiceLines");
            modelBuilder.Entity<PaymentInvoiceLines>().HasKey(t => t.Id);

            modelBuilder.Entity<UserSetofBook>().ToTable("tbUserSetofBook");
            modelBuilder.Entity<UserSetofBook>().HasKey(t => t.Id);
        }

        #endregion

        #region Overriding

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                //Undoing the change in context..
                foreach (DbEntityEntry entry in this.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        default: break;
                    }
                }
                if (ex.GetBaseException().HResult == -2146232060)
                {
                    throw new Exception("Delete Error", new Exception { Source = "Record is in use somewhere in the application" });
                }
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}