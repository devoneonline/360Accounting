using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        IEnumerable<PurchaseOrderDetail> GetAllPODetail(long poId);
        
        string Insert(PurchaseOrderDetail entity);

        string Update(PurchaseOrderDetail entity);
        
        void DeletePODetail(long id);

        IEnumerable<PurchaseOrder> GetAll(long companyId, long sobId);

        IEnumerable<PurchaseOrderView> GetAllPO(long companyId, long sobId);

        PurchaseOrderDetail GetSinglePODetail(long id);
    }
}
