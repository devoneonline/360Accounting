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
    public class RequisitionRepository : Repository, IRequisitionRepository
    {
        public IEnumerable<RequisitionDetail> GetAllRequisitionDetail(long requisitionId)
        {
            IEnumerable<RequisitionDetail> list = this.Context.RequisitionDetails.Where(x => x.RequisitionId == requisitionId);
            return list;
        }

        public string Insert(RequisitionDetail entity)
        {
            this.Context.RequisitionDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(RequisitionDetail entity)
        {
            var originalEntity = this.Context.RequisitionDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void DeleteRequisitionDetail(long id)
        {
            this.Context.RequisitionDetails.Remove(this.Context.RequisitionDetails.FirstOrDefault(x => x.Id == id));
        }

        public RequisitionDetail GetSingleRequisitionDetail(long id)
        {
            return this.Context.RequisitionDetails.FirstOrDefault(rec => rec.Id == id);
        }

        public IEnumerable<Requisition> GetAll(long companyId)
        {
            IEnumerable<Requisition> list = this.Context.Requisitions.Where(x => x.CompanyId == companyId);
            return list;
        }

        public IEnumerable<Requisition> GetAll(long companyId, long sobId)
        {
            IEnumerable<Requisition> list = this.Context.Requisitions.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return list;
        }

        public IEnumerable<RequisitionView> GetAllRequisition(long companyId, long sobId)
        {
            var query = from a in this.Context.Requisitions
                        join b in this.Context.Buyers on a.BuyerId equals b.Id
                        where a.CompanyId == companyId && b.SOBId == sobId
                        select new RequisitionView
                        {
                            BuyerId = a.BuyerId,
                            BuyerName = b.Name,
                            CompanyId = a.CompanyId,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            Description = a.Description,
                            Id = a.Id,
                            RequisitionDate = a.RequisitionDate,
                            RequisitionNo = a.RequisitionNo,
                            SOBId = a.SOBId,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate
                        };

            return query;
        }

        public Requisition GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);

            Requisition requisition = this.Context.Requisitions.FirstOrDefault(x => x.Id == longId);
            return requisition;
        }

        public string Insert(Requisition entity)
        {
            this.Context.Requisitions.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Requisition entity)
        {
            var originalEntity = this.Context.Requisitions.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            this.Context.Requisitions.Remove(this.Context.Requisitions.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}