﻿using _360Accounting.Core.Entities;
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


            IEnumerable<Invoice> entityList = this.GetAll(companyId);
            if (entityList.Count() > 0)
            {
                Invoice entity = entityList.Where(x => x.SOBId == sobId && x.PeriodId == periodId &&
                x.CurrencyId == currencyId).OrderByDescending(rec => rec.Id).First();
                return entity;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Invoice> GetAll(long companyId, long sobId, long periodId, long currencyId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices.Where(x => x.SOBId == sobId && 
                    x.PeriodId == periodId &&
                    x.CurrencyId == currencyId).ToList();
            return list;
        }

        public IEnumerable<Invoice> GetInvoices(long companyId, long sobId, long periodId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices.Where(x => x.SOBId == sobId &&
                    x.PeriodId == periodId).ToList();
            return list;
        }

        public Invoice GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            Invoice invoice = this.Context.Invoices.FirstOrDefault(a => a.CompanyId == companyId && a.Id == longId);
            return invoice;
        }

        public IEnumerable<Invoice> GetAll(long companyId)
        {
            IEnumerable<Invoice> list = this.Context.Invoices.Where(x => x.CompanyId == companyId);
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
