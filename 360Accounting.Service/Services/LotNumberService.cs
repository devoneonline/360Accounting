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
            throw new NotImplementedException();
        }

        public IEnumerable<LotNumber> GetAll(long companyId)
        {
            throw new NotImplementedException();
        }

        public bool CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId)
        {
            throw new NotImplementedException();
        }

        public bool CheckSerialNumAvailability(long companyId, string lotNum, string serialNum)
        {
            throw new NotImplementedException();
        }

        public string Insert(LotNumber entity)
        {
            throw new NotImplementedException();
        }

        public string Update(LotNumber entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id, long companyId)
        {
            throw new NotImplementedException();
        }

        public int Count(long companyId)
        {
            throw new NotImplementedException();
        }
    }
}
