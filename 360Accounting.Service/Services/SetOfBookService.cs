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

        public SetOfBook GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<SetOfBook> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(SetOfBook entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(SetOfBook entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id)
        {
            this.repository.Delete(id);
        }

        public int Count()
        {
            return this.repository.Count();
        }
    }
}
