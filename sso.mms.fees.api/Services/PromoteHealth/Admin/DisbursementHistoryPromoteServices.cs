using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Reflection.Emit;

namespace sso.mms.fees.api.Services.PromoteHealth.Admin
{
    public class DisbursementHistoryPromoteServices : IDisbursementHistoryPromoteAdminServices
    {
        private readonly PromoteHealthContext db;
        private readonly IMapper mapper;

        public DisbursementHistoryPromoteServices(PromoteHealthContext DbContext,IMapper mapper)
        {
            this.db = DbContext;
            this.mapper = mapper;
        }


        public async Task<List<PayOrderHistoryView>> GetDisbursementList()
        {
            DateTime currentDateTime = DateTime.Now;

            var DateYear =  currentDateTime.Year + 543;
            var StringDate = DateYear.ToString().Split('/')[0];

            var result = db.AaiHealthPayOrderTs.Join(db.AaiHealthWithdrawalTs,
                payorder => payorder.WithdrawalNo, withdrawal => withdrawal.WithdrawalNo,
                    (payorder, withdrawal) => new PayOrderHistoryView
                    {
                        WithdrawalNo = withdrawal.WithdrawalNo,
                        PayOrderSetNo = payorder.PayOrderSetNo,
                        CountHospitals = db.AaiHealthWithdrawalTs
                        .Where(x=> x.WithdrawalNo == withdrawal.WithdrawalNo && x.HospitalCode == payorder.HospitalCode).Count(),
                        DateApprove = payorder.ApproveDate,
                        SumChecklistPrice = db.ViewAaiHealthWithdrawalTs
                        .Where(x => x.WithdrawalNo == withdrawal.WithdrawalNo && x.HospitalCode == payorder.HospitalCode)
                        .Sum(s => s.ChecklistPrice),
                        Status = payorder.PayOrderStatus
                    }).Distinct()
                    .ToList();


            var FindCurentDate = result.Where(x => x.WithdrawalNo.Split('/')[0] != null && x.WithdrawalNo.Split('/')[0] == StringDate).ToList();

            return FindCurentDate;
        }

    }
}
