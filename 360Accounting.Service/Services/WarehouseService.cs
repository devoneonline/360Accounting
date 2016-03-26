using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class WarehouseService : IWarehouseService
    {
        private IWarehouseRepository repository;

        public WarehouseService(IWarehouseRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<Warehouse> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public Warehouse GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Warehouse> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Warehouse entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Warehouse entity)
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
