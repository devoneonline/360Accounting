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
    public class ItemRepository : Repository, IItemRepository
    {
        public ItemWarehouse GetSingle(long id)
        {
            return this.Context.ItemWarehouses.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ItemWarehouse> GetAllItemWarehouses(long itemId)
        {
            return this.Context.ItemWarehouses.Where(x => x.ItemId == itemId);
        }

        public long Insert(ItemWarehouse entity)
        {
            this.Context.ItemWarehouses.Add(entity);
            this.Commit();
            return entity.Id;
        }

        public long Update(ItemWarehouse entity)
        {
            var originalEntity = this.Context.ItemWarehouses.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id;
        }

        public void DeleteItemWarehouse(long id)
        {
            this.Context.ItemWarehouses.Remove(this.Context.ItemWarehouses.Find(id));
            this.Commit();
        }

        public Item GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            return this.Context.Items.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
        }

        public IEnumerable<Item> GetAll(long companyId)
        {
            return this.Context.Items.Where(x => x.CompanyId == companyId);
        }

        public string Insert(Item entity)
        {
            this.Context.Items.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Item entity)
        {
            var originalEntity = this.Context.Items.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Items.Remove(this.GetSingle(id, companyId));
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
