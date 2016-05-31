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
    public class ReceivingService : IReceivingService
    {
        private IReceivingRepository repository;

        public ReceivingService(IReceivingRepository repo)
        {
            this.repository = repo;
        }

        public Receiving GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<ReceivingDetailView> GetAllReceivingDetail(long receivingId)
        {
            return this.repository.GetAllReceivingDetail(receivingId);
        }

        public ReceivingDetail GetSingleReceivingDetail(long id)
        {
            return this.repository.GetSingleReceivingDetail(id);
        }

        public string Insert(ReceivingDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(ReceivingDetail entity)
        {
            return this.repository.Update(entity);
        }

        public void DeleteReceivingDetail(long id)
        {
            this.repository.DeleteReceivingDetail(id);
        }

        public IEnumerable<Receiving> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<Receiving> GetAllByPOId(long companyId, long sobId, long poId)
        {
            return this.repository.GetAllByPOId(companyId, sobId, poId);
        }

        public IEnumerable<ReceivingDetail> GetAllByPODetailId(long poDetailId)
        {
            return this.repository.GetAllByPODetailId(poDetailId);
        }

        public IEnumerable<ReceivingView> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public string Insert(Receiving entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Receiving entity)
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
