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
    public class SetOfBookService : ISetOfBookService
    {
        private ISetOfBookRepository repository;

        public SetOfBookService(ISetOfBookRepository setOfBookRepository)
        {
            this.repository = setOfBookRepository;
        }

        public SetOfBook GetSetOfBook(long companyId, string name)
        {
            return this.repository.GetSetOfBook(companyId, name);
        }
        
        public List<SetOfBook> GetByCompanyId(long companyId)
        {
            return this.repository.GetByCompanyId(companyId);
        }

        public SetOfBook GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<SetOfBook> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(SetOfBook entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(SetOfBook entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
