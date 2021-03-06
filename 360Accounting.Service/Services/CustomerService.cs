﻿using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository repository;

        public CustomerService(ICustomerRepository repo)
        {
            this.repository = repo;
        }

        public Customer GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Customer> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<Customer> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate)
        {
            return this.repository.GetAll(companyId, sobId, startDate, endDate);
        }

        public IEnumerable<Customer> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<Customer> GetAllByDate(long companyId, long sobId, DateTime date)
        {
            return this.repository.GetAllByDate(companyId, sobId, date);
        }

        public string Insert(Customer entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Customer entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
