﻿using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IPayableInvoiceDetailRepository : IRepository<PayableInvoiceDetail>
    {
        IList<PayableInvoiceDetail> GetAll(long companyId, long invoiceId);
    }
}