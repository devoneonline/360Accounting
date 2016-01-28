using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ISetOfBookRepository : IRepository<SetOfBook>
    {
        List<SetOfBook> GetByCompanyId(long companyId);

        SetOfBook GetSetOfBook(long companyId, string name);
    }
}
