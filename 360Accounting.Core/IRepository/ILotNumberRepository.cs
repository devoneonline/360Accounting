using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ILotNumberRepository : IRepository<LotNumber>
    {
        IEnumerable<LotNumber> CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId);

        IEnumerable<SerialNumber> CheckSerialNumAvailability(long companyId, string lotNum, string serialNum);

        SerialNumber GetSingleSerialNum(string id, long companyId);

        string InsertSerialNum(SerialNumber entity);

        string UpdateSerialNum(SerialNumber entity);

        void DeleteSerialNum(string id, long companyId);

        IEnumerable<LotNumber> GetAvailableLots(long companyId, long sobId, long itemId);
    }
}
