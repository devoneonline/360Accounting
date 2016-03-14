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

        public static List<Segment> GetSegmentListForCodeCombination(string sobId)
        {
            List<Segment> segmentList = new List<Segment>();
            Account account = AccountHelper.GetAccountBySOBId(sobId);
            if (account != null)
            {
                if (account.SegmentEnabled1 == true)
                {
                    segmentList.Add(new Segment
                    {
                        SegmentCount = 1,
                        SegmentName = account.SegmentName1,
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName1)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName2)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName3)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName4)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName5)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName6)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName7)
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
                        SegmentValueList = AccountValueHelper.GetAccountValues(account.Id, account.SegmentName8)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Value,
                            Text = x.ValueName
                        }).ToList()
                    });
                }
            }

            return segmentList;
        }

        public static Account GetAccountBySOBId(string sobId)
        {
            return service.GetAccountBySOBId
                (sobId.ToString(), AuthenticationHelper.User.CompanyId);
        }

        public static List<AccountViewModel> GetAccounts(string searchText, bool paging, int? page, string sort, string sortDir)
        {
            List<AccountViewModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, searchText, paging, page, sort, sortDir)
                .Select(x => new AccountViewModel(x)).ToList();
            return modelList;
        }

        public static AccountCreateViewModel GetAccount(string id)
        {
            AccountCreateViewModel account = new AccountCreateViewModel
                (service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return account;
        }

        public static string SaveChartOfAccount(AccountCreateViewModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(Mappers.GetEntityByModel(model));
            }
            else
            {
                return service.Insert(Mappers.GetEntityByModel(model));
            }
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}