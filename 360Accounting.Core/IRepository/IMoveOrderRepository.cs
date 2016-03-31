using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IMoveOrderRepository : IRepository<MoveOrder>
    {
        IEnumerable<MoveOrder> GetAll(long companyId, long sobId);

        IEnumerable<MoveOrderDetail> GetAllMoveOrderDetail(long moveOrderId);
        long Insert(MoveOrderDetail entity);
        long Update(MoveOrderDetail entity);
        void Delete(long id);
    }
}