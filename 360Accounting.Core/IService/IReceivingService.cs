﻿using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IReceivingService : IService<Receiving>
    {
        IEnumerable<ReceivingDetailView> GetAllReceivingDetail(long receivingId);

        string Insert(ReceivingDetail entity);

        string Update(ReceivingDetail entity);

        void DeleteReceivingDetail(long id);

        IEnumerable<ReceivingView> GetAll(long companyId, long sobId);

        IEnumerable<Receiving> GetAllByPOId(long companyId, long sobId, long poId);

        ReceivingDetail GetSingleReceivingDetail(long id);

        IEnumerable<ReceivingDetail> GetAllByPODetailId(long poDetailId);
    }
}
