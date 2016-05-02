using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        //ItemWarehouse GetSingle(long id);

        IEnumerable<ItemWarehouse> GetAllItemWarehouses(long itemId);

        long Insert(ItemWarehouse entity);

        long Update(ItemWarehouse entity);

        void DeleteItemWarehouse(long id);

        IEnumerable<Item> GetAll(long companyId, long sobId);

        IEnumerable<Item> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId);
    }
}
