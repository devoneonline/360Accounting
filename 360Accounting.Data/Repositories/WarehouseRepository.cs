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
    public class WarehouseRepository : Repository, IWarehouseRepository
    {
        public IEnumerable<Warehouse> GetAll(long companyId, long sobId)
        {
            IEnumerable<Warehouse> list = this.GetAll(companyId).Where(x => x.SOBId == sobId);
            return list;
        }

        public IEnumerable<Warehouse> GetAllforCombo(long companyId, long sobId)
        {
            var query = from a in this.Context.Warehouses
                        join b in this.Context.LocatorWarehouses on a.Id equals b.WarehouseId
                        join c in this.Context.ItemWarehouses on a.Id equals c.WarehouseId
                        where a.CompanyId == companyId && a.SOBId == sobId && a.Id == b.WarehouseId && a.Id == c.WarehouseId
                        select a;

            return query;
        }

        public Warehouse GetSingle(string id, long companyId)
        {
            Warehouse warehouse = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return warehouse;
        }

        public IEnumerable<Warehouse> GetAll(long companyId)
        {
            return this.Context.Warehouses.Where(x => x.CompanyId == companyId);
        }

        public string Insert(Warehouse entity)
        {
            this.Context.Warehouses.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Warehouse entity)
        {
            var originalEntity = this.Context.Warehouses.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Warehouses.Remove(this.GetSingle(id, companyId));
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
