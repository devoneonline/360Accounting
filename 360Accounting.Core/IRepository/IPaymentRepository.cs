using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.IRepository
{
    public interface IPaymentRepository : IRepository<PaymentHeader>
    {
        PaymentInvoiceLines GetSingle(long id);
        long Insert(PaymentInvoiceLines entity);
        long Update(PaymentInvoiceLines entity);
        void DeleteLine(long id, long companyId);
        IEnumerable<PaymentInvoiceLines> GetAllLines(long paymentId, long companyId);

        IEnumerable<PaymentHeaderView> GetAll(long companyId, long vendorId, long bankId, long sobId, long periodId);
        PaymentHeader GetSingle(long companyId, long vendorId, long bankId, long sobId, long periodId);
    }
}
