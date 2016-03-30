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
    public class ItemService : IItemService
    {
        private IItemRepository repository;

        public ItemService(IItemRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<Item> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }
        
        public ItemWarehouse GetSingle(long id)
        {
            return this.repository.GetSingle(id);
        }

        public long Insert(ItemWarehouse entity)
        {
            return this.repository.Insert(entity);
        }

        public long Update(ItemWarehouse entity)
        {
            return this.repository.Update(entity);
        }

        public void DeleteItemWarehouse(long id)
        {
            this.repository.DeleteItemWarehouse(id);
        }

        public IEnumerable<ItemWarehouse> GetAllItemWarehouses(long itemId)
        {
            return this.repository.GetAllItemWarehouses(itemId);
        }

        public Item GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Item> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Item entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(Item entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
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
