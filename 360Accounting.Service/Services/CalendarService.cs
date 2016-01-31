using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class CalendarService : ICalendarService
    {
        private ICalendarRepository repository;

        public CalendarService(ICalendarRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<Calendar> GetAll(long companyId, long sobId, string searchText, bool paging, int? page, string sort, string sortDir)
        {
            return this.repository.GetAll(companyId, sobId, searchText, paging, page ?? 1, sort, sortDir);
        }

        public Calendar GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Calendar> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Calendar entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Calendar entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
