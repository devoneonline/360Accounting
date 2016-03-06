using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IJournalVoucherRepository : IRepository<GLHeader>
    {
        //IEnumerable<GLHeader> GetAll(long companyId, string searchText, bool paging, int page, string sort, string sortDir);

        //IEnumerable<GLLines> GetAll(string headerId);

        //string Insert(GLLines entity);

        //string Update(GLLines entity);

        

        //void DeleteJvDetail(string id);
    }
}
