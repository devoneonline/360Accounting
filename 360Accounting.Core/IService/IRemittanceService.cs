using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IRemittanceService : IService<Remittance>
    {
        IEnumerable<Remittance> GetAll(long companyId, long sobId, long bankId, long bankAccountId);
        //Invoice GetSingle(long companyId, long sobId, long periodId, long currencyId);

        IEnumerable<Remittance> GetByRemitNo(long companyId, string remitNo);

        Remittance GetByRemitNo(string remitNo);
    }
}
