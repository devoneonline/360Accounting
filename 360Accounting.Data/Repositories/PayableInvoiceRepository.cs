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
    public class PayableInvoiceRepository : Repository, IPayableInvoiceRepository
    {
        public PayableInvoice GetSingle(long companyId, long sobId, long periodId)
        {
            IEnumerable<PayableInvoice> entityList = this.GetAll(companyId);
            if (entityList.Count() > 0)
            {
                PayableInvoice entity = entityList.Where(x => x.SOBId == sobId && x.PeriodId == periodId)
                    .OrderByDescending(rec => rec.Id).First();
                return entity;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<PayableInvoice> GetAll(long companyId, long sobId, long periodId)
        {
            IEnumerable<PayableInvoice> list = this.GetAll(companyId).Where(x => x.SOBId == sobId && x.PeriodId == periodId);
            return list;

        }
        public PayableInvoice GetSingle(string id, long companyId)
        {
            PayableInvoice invoice = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return invoice;
        }

        public IEnumerable<PayableInvoice> GetAll(long companyId)
        {
            IEnumerable<PayableInvoice> list = this.Context.PayableInvoices;
            return list;
        }

        public string Insert(PayableInvoice entity)
        {
            this.Context.PayableInvoices.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PayableInvoice entity)
        {
            PayableInvoice originalEntity = this.Context.PayableInvoices.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PayableInvoices.Remove(this.GetSingle(id, companyId));
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
