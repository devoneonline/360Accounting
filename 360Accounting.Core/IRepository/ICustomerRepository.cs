using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate);
        
        IEnumerable<Customer> GetAll(long companyId, long sobId);

        IEnumerable<Customer> GetAllByDate(long companyId, long sobId, DateTime date);
    }
}
