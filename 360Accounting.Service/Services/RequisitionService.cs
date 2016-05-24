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
    public class RequisitionService : IRequisitionService
    {
        private IRequisitionRepository repository;

        public RequisitionService(IRequisitionRepository repo)
        {
            this.repository = repo;
        }

        public Requisition GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<RequisitionDetail> GetAllRequisitionDetail(long requisitionId)
        {
            return this.repository.GetAllRequisitionDetail(requisitionId);
        }

        public RequisitionDetail GetSingleRequisitionDetail(long id)
        {
            return this.repository.GetSingleRequisitionDetail(id);
        }

        public string Insert(RequisitionDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(RequisitionDetail entity)
        {
            return this.repository.Update(entity);
        }

        public void DeleteRequisitionDetail(long id)
        {
            this.repository.DeleteRequisitionDetail(id);
        }

        public IEnumerable<Requisition> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<Requisition> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<RequisitionView> GetAllRequisition(long companyId, long sobId)
        {
            return this.repository.GetAllRequisition(companyId, sobId);
        }

        public string Insert(Requisition entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Requisition entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            throw new NotImplementedException();
        }
    }
}
