using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IItemService : IService<Item>
    {
        //ItemWarehouse GetSingle(long id);
        long Insert(ItemWarehouse entity);
        long Update(ItemWarehouse entity);
        void DeleteItemWarehouse(long id);
        IEnumerable<ItemWarehouse> GetAllItemWarehouses(long itemId);

        IEnumerable<Item> GetAll(long companyId, long sobId);
    }
}
