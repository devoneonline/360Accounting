using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IRemittanceRepository : IRepository<Remittance>
    {
        IEnumerable<Remittance> GetAll(long companyId, long sobId, long bankId, long bankAccountId);

        IEnumerable<Remittance> GetByRemitNo(long companyId, string remitNo);

        Remittance GetByRemitNo(string remitNo);

        void DeleteRemittanceDetail(string id, long companyId);
    }
}
