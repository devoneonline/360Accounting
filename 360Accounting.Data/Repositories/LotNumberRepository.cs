using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class LotNumberRepository : Repository, ILotNumberRepository
    {
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
            if (this.Context.LotNumbers.Where(x => x.CompanyId == companyId && x.LotNo == lotNum && x.SOBId == sobId && x.ItemId == itemId).Count() > 0)
                return false;
            else
                return true;
        }

        public bool CheckSerialNumAvailability(long companyId, string lotNum, string serialNum)
        {
            if (this.Context.SerialNumbers.Where(x => x.LotNo == lotNum && x.CompanyId == companyId && x.SerialNo == serialNum).Count() > 0)
                return false;
            else
                return true;
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

        public int Commit()
        {
            throw new NotImplementedException();
        }
    }
}
