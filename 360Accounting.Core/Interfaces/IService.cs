using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IService<T>
    {
        T GetSingle(string id, long companyId);

        IEnumerable<T> GetAll(long companyId);

        string Insert(T entity);

        string Update(T entity);

        void Delete(string id, long companyId);

        int Count(long companyId);
    }
}
