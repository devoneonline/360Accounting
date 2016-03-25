using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace _360Accounting.Web
{
    public static class RemittanceHelper
    {
        private static IRemittanceService service;

        static RemittanceHelper()
        {
            service = IoC.Resolve<IRemittanceService>("RemittanceService");
        }

        #region Private Methods
        private static IList<RemittanceDetailModel> getRemittanceByRemitNo(string remitNo)
        {

            IList<RemittanceDetailModel> modelList = service
                .GetByRemitNo(AuthenticationHelper.User.CompanyId, remitNo)
                .Select(x => new RemittanceDetailModel(x)).ToList();
            return modelList;
        }

        private static IList<RemittanceDetailModel> getRemittanceDetail()
        {
            return SessionHelper.Remittance.Remittances;
        }
        #endregion

        public static void Delete(string remitNo)
        {
            service.Delete(remitNo, AuthenticationHelper.User.CompanyId);
        }

        public static RemittanceModel GetRemittance(string remitNo)
        {
            return new RemittanceModel(service.GetByRemitNo(remitNo));
        }

        public static void Update(RemittanceModel remittanceModel)
        {
            var savedDetail = getRemittanceByRemitNo(remittanceModel.RemitNo);
            if (savedDetail.Count() > remittanceModel.Remittances.Count())
            {
                var tobeDeleted = savedDetail.Take(savedDetail.Count() - remittanceModel.Remittances.Count());
                foreach (var item in tobeDeleted)
                {
                    service.Delete(item.Id.ToString(), AuthenticationHelper.User.CompanyId);
                }
                savedDetail = getRemittanceByRemitNo(remittanceModel.RemitNo);
            }

            foreach (var detail in remittanceModel.Remittances)
            {
                Remittance entity = Mappers.GetEntityByModel(detail);
                if (entity.IsValid())
                {
                    if (savedDetail.Count() > 0)
                    {
                        entity.Id = savedDetail.FirstOrDefault().Id;
                        savedDetail.Remove(savedDetail.FirstOrDefault(rec => rec.Id == entity.Id));
                        service.Update(entity);
                    }
                    else
                        service.Insert(entity);
                }
            }
        }

        public static string GetRemitNo(long companyId, long sobId, long bankId, long bankAccountId)
        {
            ///TODO: plz audit this code
            var previousRemittance = service.GetAll(companyId, sobId, bankId, bankAccountId);
            string newRemitNo = "";
            if (previousRemittance.Any())
            {
                int outVal;
                bool isNumeric = int.TryParse(previousRemittance.FirstOrDefault().RemitNo, out outVal);
                if (isNumeric && previousRemittance.FirstOrDefault().RemitNo.Length == 8)
                {
                    newRemitNo = (int.Parse(previousRemittance.FirstOrDefault().RemitNo) + 1).ToString();
                    return newRemitNo;
                }
            }

            //Create New Invoice #...
            string yearDigit = SessionHelper.Remittance.RemitDate.ToString("yy");
            string monthDigit = SessionHelper.Remittance.RemitDate.ToString("MM");
            string remitNo = int.Parse("1").ToString().PadLeft(4, '0');

            return yearDigit + monthDigit + remitNo;
        }

        public static void DeleteRemittanceDetail(RemittanceDetailModel model)
        {
            RemittanceModel remittance = SessionHelper.Remittance;
            RemittanceDetailModel remittanceDetail = remittance.Remittances.FirstOrDefault(x => x.Id == model.Id);
            remittance.Remittances.Remove(remittanceDetail);
        }

        public static void UpdateRemittanceDetail(RemittanceDetailModel model)
        {
            RemittanceModel remittance = SessionHelper.Remittance;
            remittance.Remittances.FirstOrDefault(x => x.Id == model.Id).ReceiptId = model.ReceiptId;
        }

        public static void Insert(RemittanceDetailModel model)
        {
            RemittanceModel remittance = SessionHelper.Remittance;
            remittance.Remittances.Add(model);
        }

        public static IList<RemittanceDetailModel> GetRemittanceDetail([Optional]string remitNo)
        {
            if (remitNo == "New")
                return getRemittanceDetail();
            else
                return getRemittanceByRemitNo(remitNo);   
        }
        
        public static IList<RemittanceModel> GetRemittances(long sobId, long bankId, long bankAccountId)
        {
            IList<RemittanceModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, sobId, bankId, bankAccountId).ToList()
                .Select(x => new RemittanceModel(x)).ToList();
            return modelList;
        }

    }
}