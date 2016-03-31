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
    public class MoveOrderService : IMoveOrderService
    {
        private IMoveOrderRepository repository;

        public MoveOrderService(IMoveOrderRepository repo)
        {
            this.repository = repo;
        }

        public long Insert(MoveOrderDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public long Update(MoveOrderDetail entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(long id)
        {
            this.repository.Delete(id);
        }

        public IEnumerable<MoveOrderDetail> GetAllMoveOrderDetail(long moveOrderId)
        {
            return this.repository.GetAllMoveOrderDetail(moveOrderId);
        }

        public IEnumerable<MoveOrder> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public MoveOrder GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<MoveOrder> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(MoveOrder entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(MoveOrder entity)
        {
            return this.repository.Update(entity);
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
