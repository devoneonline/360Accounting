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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository repository;

        public PurchaseOrderService(IPurchaseOrderRepository repo)
        {
            this.repository = repo;
        }

        public PurchaseOrder GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<PurchaseOrderDetail> GetAllPODetail(long poId)
        {
            return this.repository.GetAllPODetail(poId);
        }

        public PurchaseOrderDetail GetSinglePODetail(long id)
        {
            return this.repository.GetSinglePODetail(id);
        }

        public string Insert(PurchaseOrderDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(PurchaseOrderDetail entity)
        {
            return this.repository.Update(entity);
        }

        public void DeletePODetail(long id)
        {
            this.repository.DeletePODetail(id);
        }

        public IEnumerable<PurchaseOrder> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<PurchaseOrder> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<PurchaseOrderView> GetAllPO(long companyId, long sobId)
        {
            return this.repository.GetAllPO(companyId, sobId);
        }

        public string Insert(PurchaseOrder entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(PurchaseOrder entity)
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
