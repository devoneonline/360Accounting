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
    public class CodeCombinitionService : ICodeCombinitionService
    {
        private ICodeCombinitionRepository repository;

        public CodeCombinitionService(ICodeCombinitionRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<CodeCombinitionView> GetAll(long sobId, string searchText, bool paging, int? page, string sort, string sortDir)
        {
            return this.repository.GetAll(sobId, searchText, paging, page ?? 1, sort, sortDir);
        }

        public CodeCombinition GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<CodeCombinition> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(CodeCombinition entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(CodeCombinition entity)
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
