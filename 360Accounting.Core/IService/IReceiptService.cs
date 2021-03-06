﻿using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IReceiptService : IService<Receipt>
    {
        IEnumerable<ReceiptView> GetReceipts(long sobId, long periodId, long customerId, long currencyId, long companyId);

        IEnumerable<ReceiptView> GetReceipts(long sobId, long bankId, long bankAccountId, DateTime? date = null);

        Receipt GetSingle(long companyId, long sobId, long periodId, long currencyId, long customerId);

        IEnumerable<Receipt> GetByCurrencyId(long companyId, long sobId, long currencyId);

        List<ReceiptAuditTrial> ReceiptAuditTrial(long companyId, long sobId, DateTime fromDate, DateTime toDate);
        List<ReceiptPrintout> ReceiptPrintout(long companyId, long sobId, DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId);
        List<CustomerwiseReceiptClearance> CustomerwiseReceiptClearance(long companyId, long sobId, DateTime fromDate, DateTime toDate, long customerId);
    }
}