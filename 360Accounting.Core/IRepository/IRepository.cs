using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IRepository<T>
    {
        T GetSingle(string id);

        IEnumerable<T> GetAll();

        string Insert(T entity);

        string Update(T entity);

        void Delete(string id);

        int Count();
    }
}
