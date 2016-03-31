using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class MoveOrderRepository : Repository, IMoveOrderRepository
    {
        public IEnumerable<MoveOrder> GetAll(long companyId, long sobId)
        {
            return this.GetAll(companyId).Where(x => x.SOBId == sobId);
        }

        public IEnumerable<MoveOrderDetail> GetAllMoveOrderDetail(long moveOrderId)
        {
            return this.Context.MoveOrderDetails.Where(x => x.MoveOrderId == moveOrderId);
        }

        public long Insert(MoveOrderDetail entity)
        {
            this.Context.MoveOrderDetails.Add(entity);
            this.Commit();
            return entity.Id;
        }

        public long Update(MoveOrderDetail entity)
        {
            var originalEntity = this.Context.MoveOrderDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id;
        }

        public void Delete(long id)
        {
            this.Context.MoveOrderDetails.Remove(this.Context.MoveOrderDetails.Find(id));
            this.Commit();
        }

        public MoveOrder GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            return this.Context.MoveOrders.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
        }

        public IEnumerable<MoveOrder> GetAll(long companyId)
        {
            return this.Context.MoveOrders.Where(x => x.CompanyId == companyId);
        }

        public string Insert(MoveOrder entity)
        {
            this.Context.MoveOrders.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(MoveOrder entity)
        {
            var originalEntity = this.Context.MoveOrders.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.MoveOrders.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            return this.GetAll(companyId).Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}
