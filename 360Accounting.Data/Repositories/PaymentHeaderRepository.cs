using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using _360Accounting.Common;
using _360Accounting.Core;

namespace _360Accounting.Data.Repositories
{
    public class PaymentHeaderRepository:Repository, IPaymentHeaderRepository
    {
        public PaymentHeader GetSingle(string id, long companyId)
        {
            PaymentHeader entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<PaymentHeader> GetAll(long companyId)
        {
            var query = from a in this.Context.PaymentHeaders
                        join b in this.Context.SetOfBooks on a.SOBId equals b.Id
                        join c in this.Context.Companies on b.CompanyId equals c.Id
                        select a;
            return query;
        }

        public string Insert(PaymentHeader entity)
        {
            this.Context.PaymentHeaders.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PaymentHeader entity)
        {
            var originalEntity = this.Context.GLHeaders.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.PaymentHeaders.Remove(this.GetSingle(id, companyId));
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