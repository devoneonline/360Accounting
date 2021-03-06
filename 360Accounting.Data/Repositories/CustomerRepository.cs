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
    public class CustomerRepository : Repository, ICustomerRepository
    {
        public Customer GetSingle(string id, long companyId)
        {
            Customer customer = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return customer;
        }

        public IEnumerable<Customer> GetAll(long companyId)
        {
            IEnumerable<Customer> customerList = this.Context.Customers
                .Where(x => x.CompanyId == companyId);
            return customerList;
        }

        public IEnumerable<Customer> GetAll(long companyId, long sobId)
        {
            IEnumerable<Customer> customerList = this.Context.Customers
                .Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return customerList;
        }

        public IEnumerable<Customer> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate)
        {
            //IEnumerable<Customer> customerList = this.Context.Customers
            //    .Where(x => x.CompanyId == companyId && x.SOBId == sobId && (x.StartDate <= startDate || x.StartDate == null) && (x.EndDate >= endDate || x.EndDate == null));
            IEnumerable<Customer> customerList = this.Context.Customers
                  .Where(x => x.CompanyId == companyId && x.SOBId == sobId && (x.EndDate >= endDate || x.EndDate == null));
            return customerList;
        }

        public IEnumerable<Customer> GetAllByDate(long companyId, long sobId, DateTime date)
        {
            IEnumerable<Customer> customerList = this.Context.Customers
                .Where(x => x.CompanyId == companyId && x.SOBId == sobId && (x.EndDate >= date || x.EndDate == null));
            return customerList;
        }

        public string Insert(Customer entity)
        {
            this.Context.Customers.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Customer entity)
        {
            Customer originalEntity = this.Context.Customers.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Customers.Remove(this.GetSingle(id, companyId));
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
