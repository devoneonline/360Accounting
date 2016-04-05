using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface ILotNumberService : IService<LotNumber>
    {
        IEnumerable<LotNumber> CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId);

        IEnumerable<SerialNumber> CheckSerialNumAvailability(long companyId, string lotNum, string serialNum);

        SerialNumber GetSingleSerialNum(string id, long companyId);

        string InsertSerialNum(SerialNumber entity);

        string UpdateSerialNum(SerialNumber entity);

        void DeleteSerialNum(string id, long companyId);
    }
}
