using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IMoveOrderService : IService<MoveOrder>
    {
        long Insert(MoveOrderDetail entity);
        long Update(MoveOrderDetail entity);
        void Delete(long id);
        IEnumerable<MoveOrderDetail> GetAllMoveOrderDetail(long moveOrderId);

        IEnumerable<MoveOrder> GetAll(long companyId, long sobId);
    }
}
