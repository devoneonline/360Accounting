using _360Accounting.Core;
using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class GLHeaderService : IGLHeaderService
    {
        private IGLHeaderRepository repository;

        public GLHeaderService(IGLHeaderRepository repo)
        {
            this.repository = repo;
        }

        public GLHeader GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<GLHeader> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(GLHeader entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(GLHeader entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
