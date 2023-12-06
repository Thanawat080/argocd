using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.ViewModels.Dental;

namespace sso.mms.fees.api.Services.Dental.EXT
{
    public class HistoryWithdrawalServices : IHistoryWithdrawals
    {
        public readonly DentalContext db;
        public HistoryWithdrawalServices(DentalContext dentalContext)
        {
            this.db = dentalContext;
        }
        public async Task<List<AaiDentalWithdrawTView>> GetHistorys()
        {
            //var x = 1;
            //return await db.AaiDentalCheckHs.GroupJoin(db.AaiDentalCheckDs, H => H.CheckHId, D => D.CheckHId,
            //(H, D) => new { H, D }).Select(A => new AaiDentalCheckHView
            //{
            //    CheckHId = A.H.CheckHId,
            //    HospitalId = A.H.HospitalId,
            //    PersonalId = A.H.PersonalId,
            //    SsoOrgId = A.H.SsoOrgId,
            //    PatientName = A.H.PatientName,
            //    Sex = A.H.Sex,
            //    BirthDate = A.H.BirthDate,
            //    National = A.H.National,
            //    SsoStatus = A.H.SsoStatus,
            //    CheckDate = A.H.CheckDate,
            //    PhoneNo = A.H.PhoneNo,
            //    BalanceMoney = A.H.BalanceMoney,
            //    PortalPort = A.H.PortalPort,
            //    DentalCarDId = A.H.DentalCarDId,
            //    IsFromReader = A.H.IsFromReader,
            //    Reason = A.H.Reason,
            //    ConfirmBy = A.H.ConfirmBy,
            //    CheckStatus = A.H.CheckStatus,
            //    CheckDocu = A.H.CheckDocu,
            //    CreateDate = A.H.CreateDate,
            //    CreateBy = A.H.CreateBy,
            //    UpdateDate = A.H.UpdateDate,
            //    UpdateBy = A.H.UpdateBy,
            //    BalanceUsed = A.H.BalanceUsed,

            //    AaiDentalCheckDs = A.H.AaiDentalCheckDs.ToList()
            //}).ToListAsync();
            //var result = await (from W  in db.AaidentalWith)
            var result = await (from W in db.AaiDentalWithdrawTs
                                join H in db.AaiDentalCheckHs on W.CheckHId equals H.CheckHId
                                join D in db.AaiDentalCheckDs on W.CheckDId equals D.CheckDId
                                select new AaiDentalWithdrawTView
                                {

                                    CheckDId =  W.CheckDId,
                                    CheckH = new AaiDentalCheckHView { 
                                        BirthDate = DateTime.Now,
                                    },
                                    CheckD = new AaiDentalCheckDView
                                    {
                                        DoctorName = D.DoctorName,  
                                    }
                                    


                                }).ToListAsync();
            return result;

        }
    }
}
