﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IInvoiceSourceRepository : IRepository<InvoiceSource>
    {
        List<InvoiceSource> GetAll(long companyId, long sobId);

        IEnumerable<InvoiceSource> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId);
    }
}
