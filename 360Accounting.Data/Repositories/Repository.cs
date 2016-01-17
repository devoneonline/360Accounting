using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class Repository
    {
        public Repository()
        {
            this.Context = new ApplicationDbContext();
        }

        protected ApplicationDbContext Context { get; private set; }
    }
}
