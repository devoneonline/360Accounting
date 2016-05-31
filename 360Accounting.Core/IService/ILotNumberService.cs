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

        bool CheckSerialNumAvailability(long companyId, long lotNoId, string serialNum);

        SerialNumber GetSingleSerialNum(string id, long companyId);

        string InsertSerialNum(SerialNumber entity);

        string UpdateSerialNum(SerialNumber entity);

        void DeleteSerialNum(string id, long companyId);

        IEnumerable<LotNumber> GetAvailableLots(long companyId, long sobId, long itemId);

        IEnumerable<SerialNumber> GetAvailableSerials(LotNumber entity, long companyId, long sobId);

        SerialNumber GetSerialNo(string serial, long lotNoId, long companyId, long sobId);

        List<SerialNumber> GetSerialsbyLotNo(long lotNoId, long companyId, long sobId);

        LotNumber GetLotBySourceId(long sourceId, long companyId, long sobId);

        IEnumerable<LotNumber> GetAllbyLotNo(long companyId, long sobId, string lotNo, long itemId);
    }
}
