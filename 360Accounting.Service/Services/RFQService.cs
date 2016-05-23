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
    public class RFQService : IRFQService
    {
        private IRFQRepository repository;

        public RFQService(IRFQRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<RFQ> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public List<RFQView> GetAllRFQs(long companyId, long sobId)
        {
            return this.repository.GetAllRFQs(companyId, sobId);
        }

        public IEnumerable<RFQDetail> GetAllRFQDetail(long rfqId)
        {
            return this.repository.GetAllRFQDetail(rfqId);
        }

        public string InsertRFQDetail(RFQDetail entity)
        {
            return this.repository.InsertRFQDetail(entity);
        }

        public string UpdateRFQDetail(RFQDetail entity)
        {
            return this.repository.UpdateRFQDetail(entity);
        }

        public void DeleteRFQDetail(long id)
        {
            this.repository.DeleteRFQDetail(id);
        }

        public RFQ GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<RFQ> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(RFQ entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(RFQ entity)
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
