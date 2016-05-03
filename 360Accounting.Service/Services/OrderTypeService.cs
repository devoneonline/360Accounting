﻿using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class OrderTypeService : IService<OrderType>
    {
        private IOrderTypeRepository repository;

        public OrderTypeService(IOrderTypeRepository repo)
        {
            this.repository = repo;
        }

        public OrderType GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<OrderType> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(OrderType entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(OrderType entity)
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
