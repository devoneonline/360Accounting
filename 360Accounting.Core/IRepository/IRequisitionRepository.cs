using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IRequisitionRepository : IRepository<Requisition>
    {
        IEnumerable<RequisitionDetail> GetAllRequisitionDetail(long requisitionId);
        
        string Insert(RequisitionDetail entity);
        
        string Update(RequisitionDetail entity);
        
        void DeleteRequisitionDetail(long id);

        IEnumerable<Requisition> GetAll(long companyId, long sobId);

        IEnumerable<RequisitionView> GetAllRequisition(long companyId, long sobId);

        RequisitionDetail GetSingleRequisitionDetail(long id);
    }
}
