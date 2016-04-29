using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class AccountHelper
    {
        private static IAccountService service;

        static AccountHelper()
        {
            service = IoC.Resolve<IAccountService>("AccountService");
        }

        private static Account getEntityByModel(AccountCreateViewModel model)
        {
            if (model == null) return null;

            Account entity = new Account();
            entity.Id = model.Id;
            entity.SegmentChar1 = model.SegmentChar1;
            entity.SegmentChar2 = model.SegmentChar2;
            entity.SegmentChar3 = model.SegmentChar3;
            entity.SegmentChar4 = model.SegmentChar4;
            entity.SegmentChar5 = model.SegmentChar5;
            entity.SegmentChar6 = model.SegmentChar6;
            entity.SegmentChar7 = model.SegmentChar7;
            entity.SegmentChar8 = model.SegmentChar8;
            entity.SegmentEnabled1 = model.SegmentEnabled1;
            entity.SegmentEnabled2 = model.SegmentEnabled2;
            entity.SegmentEnabled3 = model.SegmentEnabled3;
            entity.SegmentEnabled4 = model.SegmentEnabled4;
            entity.SegmentEnabled5 = model.SegmentEnabled5;
            entity.SegmentEnabled6 = model.SegmentEnabled6;
            entity.SegmentEnabled7 = model.SegmentEnabled7;
            entity.SegmentEnabled8 = model.SegmentEnabled8;
            entity.SegmentName1 = model.SegmentName1;
            entity.SegmentName2 = model.SegmentName2;
            entity.SegmentName3 = model.SegmentName3;
            entity.SegmentName4 = model.SegmentName4;
            entity.SegmentName5 = model.SegmentName5;
            entity.SegmentName6 = model.SegmentName6;
            entity.SegmentName7 = model.SegmentName7;
            entity.SegmentName8 = model.SegmentName8;
            entity.SOBId = model.SOBId;
            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static int? GetSegmentCharacters(string segment, Account account)
        {
            if (segment == account.SegmentName1)
            {
                return account.SegmentChar1;
            }
            else if (segment == account.SegmentName2)
            {
                return account.SegmentChar2;
            }
            else if (segment == account.SegmentName3)
            {
                return account.SegmentChar3;
            }
            else if (segment == account.SegmentName4)
            {
                return account.SegmentChar4;
            }
            else if (segment == account.SegmentName5)
            {
                return account.SegmentChar5;
            }
            else if (segment == account.SegmentName6)
            {
                return account.SegmentChar6;
            }
            else if (segment == account.SegmentName7)
            {
                return account.SegmentChar7;
            }
            else if (segment == account.SegmentName8)
            {
                return account.SegmentChar8;
            }
            else
            {
                return 0;
            }
        }

        public static List<SelectListItem> GetSegmentList(string sobId)
        {
            Account account = GetAccountBySOBId(sobId);
            var lst = new List<SelectListItem>();
            if (account != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName1,
                    Value = account.SegmentName1,
                    Selected = true
                });
                if (account.SegmentName2 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName2,
                        Value = account.SegmentName2
                    });
                }

                if (account.SegmentName3 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName3,
                        Value = account.SegmentName3
                    });
                }

                if (account.SegmentName4 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName4,
                        Value = account.SegmentName4
                    });
                }

                if (account.SegmentName5 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName5,
                        Value = account.SegmentName5
                    });
                }

                if (account.SegmentName6 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName6,
                        Value = account.SegmentName6
                    });
                }

                if (account.SegmentName7 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName7,
                        Value = account.SegmentName7
                    });
                }

                if (account.SegmentName8 != null)
                {
                    lst.Add(new SelectListItem
                    {
                        Text = account.SegmentName8,
                        Value = account.SegmentName8
                    });
                }
            }

            return lst;
        }

        public static List<Segment> GetSegmentListForCodeCombination(string sobId, bool fetchSaved = false)
        {
            List<Segment> segmentList = new List<Segment>();
            Account account = AccountHelper.GetAccountBySOBId(sobId);

            #region Filling Segments

            if (account != null)
            {
                if (account.SegmentEnabled1 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 1,
                        SegmentName = account.SegmentName1,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName1, 1, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled2 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 2,
                        SegmentName = account.SegmentName2,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName2, 2, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled3 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 3,
                        SegmentName = account.SegmentName3,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName3, 3, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled4 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 4,
                        SegmentName = account.SegmentName4,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName4, 4, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled5 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 5,
                        SegmentName = account.SegmentName5,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName5, 5, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled6 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 6,
                        SegmentName = account.SegmentName6,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName6, 6, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled7 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 7,
                        SegmentName = account.SegmentName7,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName7, 7, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }

                if (account.SegmentEnabled8 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 8,
                        SegmentName = account.SegmentName8,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SOBId, account.SegmentName8, 8, fetchSaved)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }
            }

            #endregion

            return segmentList;
        }

        public static Account GetAccountBySOBId(string sobId)
        {
            return service.GetAccountBySOBId
                (sobId.ToString(), AuthenticationHelper.CompanyId.Value);
        }

        public static List<AccountViewModel> GetAccounts(string searchText, bool paging, int? page, string sort, string sortDir)
        {
            List<AccountViewModel> modelList = service
                .GetAll(SessionHelper.SOBId, AuthenticationHelper.CompanyId.Value, searchText, paging, page, sort, sortDir)
                .Select(x => new AccountViewModel(x)).ToList();
            return modelList;
        }

        public static AccountCreateViewModel GetAccount(string id)
        {
            AccountCreateViewModel account = new AccountCreateViewModel
                (service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            return account;
        }

        public static string SaveChartOfAccount(AccountCreateViewModel model)
        {
            if (model.Id > 0)
            {
                if (!string.IsNullOrEmpty(model.SegmentName1) && !model.SegmentEnabled1)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName1);
                    if (accountValues.Any())
                        return "Segment 1 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName2) && !model.SegmentEnabled2)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName2);
                    if (accountValues.Any())
                        return "Segment 2 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName3) && !model.SegmentEnabled3)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName3);
                    if (accountValues.Any())
                        return "Segment 3 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName4) && !model.SegmentEnabled4)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName4);
                    if (accountValues.Any())
                        return "Segment 4 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName5) && !model.SegmentEnabled5)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName5);
                    if (accountValues.Any())
                        return "Segment 5 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName6) && !model.SegmentEnabled6)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName6);
                    if (accountValues.Any())
                        return "Segment 6 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName7) && !model.SegmentEnabled7)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName7);
                    if (accountValues.Any())
                        return "Segment 7 can not be marked as disabled";
                }
                else if (!string.IsNullOrEmpty(model.SegmentName8) && !model.SegmentEnabled8)
                {
                    List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValues(model.Id, SessionHelper.SOBId, model.SegmentName8);
                    if (accountValues.Any())
                        return "Segment 8 can not be marked as disabled";
                }
                return service.Update(getEntityByModel(model));
            }
            else
            {
                return service.Insert(getEntityByModel(model));
            }
        }

        public static void Delete(string id)
        {
            List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValuesbyChartId(Convert.ToInt64(id), SessionHelper.SOBId);
            if (accountValues.Any())
                throw new Exception("The account having values, cannot be deleted");

            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static long GetAccountIdBySegments(string segments)
        {
            return service.GetAccountIdBySegments(segments, AuthenticationHelper.CompanyId.Value);
        }
    }
}