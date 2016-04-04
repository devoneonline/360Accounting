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
        bool CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId);

        bool CheckSerialNumAvailability(long companyId, string lotNum, string serialNum);
    }
}
