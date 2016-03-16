using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class InvoiceRepository : Repository, IInvoiceRepository
    {
        public Invoice GetSingle(long companyId, long sobId, long periodId, long currencyId)
        {
            Invoice entity = this.GetAll(companyId, sobId, periodId, currencyId)
                .OrderByDescending(rec => rec.Id).First();
            return entity;
        }

        public IEnumerable<Invoice> GetAll(long companyId, long sobId, long periodId, long currencyId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices
                .Where(x => x.SOBId == sobId && 
                    x.PeriodId == periodId &&
                    x.CurrencyId == currencyId).ToList();
            return list;
        }

        public Invoice GetSingle(string id, long companyId)
        {
            Invoice invoice = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return invoice;
        }

        public IEnumerable<Invoice> GetAll(long companyId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices;
            return list;
        }

        public string Insert(Invoice entity)
        {
            this.Context.Invoices.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Invoice entity)
        {
            Invoice originalEntity = this.Context.Invoices.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Invoices.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            return this.GetAll(companyId).Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}
