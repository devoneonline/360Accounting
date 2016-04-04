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
    public class LotNumberService : ILotNumberService
    {
        private ILotNumberRepository repository;

        public LotNumberService(ILotNumberRepository repo)
        {
            this.repository = repo;
        }

        public LotNumber GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<LotNumber> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public bool CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId)
        {
            return this.repository.CheckLotNumAvailability(companyId, lotNum, itemId, sobId);
        }

        public bool CheckSerialNumAvailability(long companyId, string lotNum, string serialNum)
        {
            return this.repository.CheckSerialNumAvailability(companyId, lotNum, serialNum);
        }

        public string Insert(LotNumber entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(LotNumber entity)
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
