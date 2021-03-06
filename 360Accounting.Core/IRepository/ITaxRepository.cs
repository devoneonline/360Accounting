﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface ITaxRepository : IRepository<Tax>
    {
        IEnumerable<Tax> GetAll(long companyId, long sobId);
    }
}
