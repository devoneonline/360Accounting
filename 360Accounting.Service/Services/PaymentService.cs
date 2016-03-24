using _360Accounting.Core.IService;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _360Accounting.Core.IRepository;

namespace _360Accounting.Service
{
    public class PaymentService : IPaymentService
    {
        #region Declaration

        private IPaymentRepository repository;

        #endregion

        #region Constructor

        public PaymentService(IPaymentRepository repo)
        {
            this.repository = repo;
        }

        #endregion

        #region Methods

        public PaymentHeader GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public PaymentHeader GetSingle(long companyId, long vendorId, long bankId, long periodId, long sobId)
        {
            return this.repository.GetSingle(companyId, vendorId, bankId, sobId, periodId);
        }

        public IEnumerable<PaymentHeader> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<PaymentHeaderView> GetAll(long companyId, long vendorId, long bankId, long sobId, long periodId)
        {
            return this.repository.GetAll(companyId, vendorId, bankId, sobId, periodId);
        }

        public string Insert(PaymentHeader entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(PaymentHeader entity)
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
   
        public IEnumerable<PaymentInvoiceLines> GetAllLines(long paymentId, long companyId)
        {
            return this.repository.GetAllLines(paymentId, companyId);
        }

        public long Insert(PaymentInvoiceLines entity)
        {
            return this.repository.Insert(entity);
        }

        public long Update(PaymentInvoiceLines entity)
        {
            return this.repository.Update(entity);
        }

        public void DeleteLine(long id, long companyId)
        {
            this.repository.DeleteLine(id, companyId);
        }

        public PaymentInvoiceLines GetSingle(long id)
        {
            return this.repository.GetSingle(id);
        }

        #endregion
    }
}
