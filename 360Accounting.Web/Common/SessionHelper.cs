using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public class SessionHelper
    {
        private const string Session_RFQ = "RFQ";
        private const string Session_MoveOrder = "MoveOrder";
        private const string Sesson_Locator = "Locator";
        private const string Sesson_Item = "Item";
        private const string Session_DocumentDate = "DocumentDate";
        private const string Session_Calendar = "Calendar";
        private const string Session_Receipts = "Receipts";
        private const string Session_Bank = "Bank";
        private const string Session_PrecisionLimit = "PrecisionLimit";
        private const string Session_SOBId = "SOBId";
        private const string Session_BankAccount = "BankAccount";
        private const string Session_JV = "JV";
        private const string Session_Tax = "Tax";
        private const string Session_Invoice = "Invoice";
        private const string Session_Remittance = "Remittance";
        private const string Sesson_Payment = "Payment";
        private const string Sesson_PayableInvoice = "PayableInvoice";
        private const string Session_MiscellaneousTransaction = "MiscellaneousTransaction";
        private const string Session_Order = "Order";
        private const string Session_Shipment = "Shipment";
        private const string Session_Requisition = "Requisition";
        private const string Session_Selected_Value = "SelectedValue";

        public static RFQModel RFQ
        {
            get
            {
                return HttpContext.Current.Session[Session_RFQ] == null ? null :
                    (RFQModel)HttpContext.Current.Session[Session_RFQ];
            }
            set
            {
                HttpContext.Current.Session[Session_RFQ] = value;
            }
        }

        public static MoveOrderModel MoveOrder
        {
            get
            {
                return HttpContext.Current.Session[Session_MoveOrder] == null ? null :
                    (MoveOrderModel)HttpContext.Current.Session[Session_MoveOrder];
            }
            set
            {
                HttpContext.Current.Session[Session_MoveOrder] = value;
            }
        }

        public static LocatorModel Locator
        {
            get
            {
                return HttpContext.Current.Session[Sesson_Locator] == null ? null :
                    (LocatorModel)HttpContext.Current.Session[Sesson_Locator];
            }
            set
            {
                HttpContext.Current.Session[Sesson_Locator] = value;
            }
        }

        public static ItemModel Item
        {
            get
            {
                return HttpContext.Current.Session[Sesson_Item] == null ? null :
                    (ItemModel)HttpContext.Current.Session[Sesson_Item];
            }
            set
            {
                HttpContext.Current.Session[Sesson_Item] = value;
            }
        }

        public static PayableInvoiceModel PayableInvoice
        {
            get
            {
                return HttpContext.Current.Session[Sesson_PayableInvoice] == null ? null :
                    (PayableInvoiceModel)HttpContext.Current.Session[Sesson_PayableInvoice];
            }
            set
            {
                HttpContext.Current.Session[Sesson_PayableInvoice] = value;
            }
        }

        public static ReceiptModel Receipts
        {
            get
            {
                return HttpContext.Current.Session[Session_Receipts] == null ? null :
                    (ReceiptModel)HttpContext.Current.Session[Session_Receipts];
            }
            set
            {
                HttpContext.Current.Session[Session_Receipts] = value;
            }
        }

        public static BankAccountModel BankAccount
        {
            get
            {
                return HttpContext.Current.Session[Session_BankAccount] == null ? null :
                    (BankAccountModel)HttpContext.Current.Session[Session_BankAccount];
            }
            set
            {
                HttpContext.Current.Session[Session_BankAccount] = value;
            }
        }

        public static BankModel Bank
        {
            get
            {
                return HttpContext.Current.Session[Session_Bank] == null ? null :
                    (BankModel)HttpContext.Current.Session[Session_Bank];
            }
            set
            {
                HttpContext.Current.Session[Session_Bank] = value;
            }
        }

        public static RemittanceModel Remittance
        {
            get
            {
                return HttpContext.Current.Session[Session_Remittance] == null ? null :
                    (RemittanceModel)HttpContext.Current.Session[Session_Remittance];
            }
            set
            {
                HttpContext.Current.Session[Session_Remittance] = value;
            }
        }
        
        public static InvoiceModel Invoice
        {
            get
            {
                return HttpContext.Current.Session[Session_Invoice] == null ? null :
                    (InvoiceModel)HttpContext.Current.Session[Session_Invoice];
            }
            set
            {
                HttpContext.Current.Session[Session_Invoice] = value;
            }
        }

        public static TaxModel Tax
        {
            get
            {
                return HttpContext.Current.Session[Session_Tax] == null ? null :
                    (TaxModel)HttpContext.Current.Session[Session_Tax];
            }
            set
            {
                HttpContext.Current.Session[Session_Tax] = value;
            }
        }

        public static GLHeaderModel JV
        {
            get
            {
                return HttpContext.Current.Session[Session_JV] == null ? null :
                    (GLHeaderModel)HttpContext.Current.Session[Session_JV];
            }
            set
            {
                HttpContext.Current.Session[Session_JV] = value;
            }
        }

        public static OrderModel Order
        {
            get
            {
                return HttpContext.Current.Session[Session_Order] == null ? null :
                    (OrderModel)HttpContext.Current.Session[Session_Order];
            }
            set
            {
                HttpContext.Current.Session[Session_Order] = value;
            }
        }

        public static OrderShipmentModel Shipment
        {
            get
            {
                return HttpContext.Current.Session[Session_Shipment] == null ? null :
                    (OrderShipmentModel)HttpContext.Current.Session[Session_Shipment];
            }
            set
            {
                HttpContext.Current.Session[Session_Shipment] = value;
            }
        }

        public static RequisitionModel Requisition
        {
            get
            {
                return HttpContext.Current.Session[Session_Requisition] == null ? null :
                    (RequisitionModel)HttpContext.Current.Session[Session_Requisition];
            }
            set
            {
                HttpContext.Current.Session[Session_Requisition] = value;
            }
        }

        public static PaymentViewModel Payment
        {
            get
            {
                return HttpContext.Current.Session[Sesson_Payment] == null ? null :
                    (PaymentViewModel)HttpContext.Current.Session[Sesson_Payment];
            }
            set
            {
                HttpContext.Current.Session[Sesson_Payment] = value;
            }
        }

        public static DateTime DocumentDate
        {
            get
            {
                return Convert.ToDateTime(HttpContext.Current.Session[Session_DocumentDate].ToString());
            }
            set
            {
                HttpContext.Current.Session[Session_DocumentDate] = value;
            }
        }

        //For Shipment Grid bhund.. will be resolved..
        public static long ItemId
        {
            get
            {
                return HttpContext.Current.Session["ItemId"] == null ? 0 : Convert.ToInt64(HttpContext.Current.Session["ItemId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["ItemId"] = value;
            }
        }

        public static long SOBId
        {
            get
            {
                return HttpContext.Current.Session["SOBId"]==null ? 0 :
                    Convert.ToInt64(HttpContext.Current.Session["SOBId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["SOBId"] = value;
            }
        }

        public static string SOBName
        {
            get
            {
                if (HttpContext.Current.Session["SOBName"]==null)
                {
                    HttpContext.Current.Session["SOBName"] = "Click here to select Set of Book";
                }
                return HttpContext.Current.Session["SOBName"].ToString();
            }
            set
            {
                HttpContext.Current.Session["SOBName"] = value;
            }
        }

        public static long PeriodId
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["PeriodId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["PeriodId"] = value;
            }
        }

        public static long CodeCombinitionId
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["CodeCombinitionId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["CodeCombinitionId"] = value;
            }
        }

        public static long VendorId
        {
            get
            {
                return Convert.ToInt64(HttpContext.Current.Session["VendorId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["VendorId"] = value;
            }
        }

        //TODO: this is temporary because of the issue 40 in task list.
        public static int PrecisionLimit
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["PrecisionLimit"].ToString());
            }
            set
            {
                HttpContext.Current.Session["PrecisionLimit"] = value;
            }
        }

        //TODO: I am using it in JV to check the GL Date in GLHeader and Account in GL Lines..?
        public static CalendarViewModel Calendar
        {
            get
            {
                return HttpContext.Current.Session[Session_Calendar] == null ? null :
                    (CalendarViewModel)HttpContext.Current.Session[Session_Calendar];
            }
            set
            {
                HttpContext.Current.Session[Session_Calendar] = value;
            }
        }

        public static MiscellaneousTransactionModel MiscellaneousTransaction
        {
            get
            {
                return HttpContext.Current.Session[Session_MiscellaneousTransaction] == null ? null :
                    (MiscellaneousTransactionModel)HttpContext.Current.Session[Session_MiscellaneousTransaction];
            }
            set
            {
                HttpContext.Current.Session[Session_MiscellaneousTransaction] = value;
            }
        }

        public static string SelectedValue
        {
            get
            {
                return HttpContext.Current.Session[Session_Selected_Value] == null ? null : HttpContext.Current.Session[Session_Selected_Value].ToString();
            }
            set
            {
                HttpContext.Current.Session[Session_Selected_Value] = value;
            }
        }
    }
}