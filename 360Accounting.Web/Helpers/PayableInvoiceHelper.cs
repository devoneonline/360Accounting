using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace _360Accounting.Web
{
    public static class PayableInvoiceHelper
    {
        private static IPayableInvoiceService service;
        private static IPayableInvoiceDetailService detailService;

        static PayableInvoiceHelper()
        {
            service = IoC.Resolve<IPayableInvoiceService>("PayableInvoiceService");
            detailService = IoC.Resolve<IPayableInvoiceDetailService>("PayableInvoiceDetailService");
        }

        #region Private Methods
        private static IList<PayableInvoiceDetailModel> getInvoiceDetailByInvoiceId(string invoiceId)
        {
            IList<PayableInvoiceDetailModel> modelList = detailService
                .GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(invoiceId))
                .Select(x => new PayableInvoiceDetailModel(x)).ToList();
            return modelList;
        }

        private static IList<PayableInvoiceDetailModel> getInvoiceDetail()
        {
            return SessionHelper.PayableInvoice.InvoiceDetail;
        }
        #endregion

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static IList<PayableInvoiceDetailModel> GetInvoiceDetail([Optional]string invoiceId)
        {
            if (invoiceId == null)
                return getInvoiceDetail();
            else
                return getInvoiceDetailByInvoiceId(invoiceId);        
        }

        public static PayableInvoiceModel GetInvoice(string id)
        {
            return new PayableInvoiceModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static IList<PayableInvoiceModel> GetInvoices(long sobId, long periodId)
        {
            IList<PayableInvoiceModel> modelList = service.GetAll(AuthenticationHelper.User.CompanyId, sobId, periodId).Select(x => new PayableInvoiceModel(x)).ToList();
            return modelList;
        }
    }
}