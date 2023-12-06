using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class GetViewPayOrderServices : IGetViewPayOrderExtServices
    {
        private readonly PromoteHealthContext db;

        public GetViewPayOrderServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }
        public async Task<List<PayOrderView>> GetByHoscode(string hoscode)
        {
            try
            {
                var result = db.AaiHealthPayOrderTs.Where(x => x.HospitalCode == hoscode).Select(y => new PayOrderView
                {
                    PayOrderId = y.PayOrderId,
                    PayOrderSetNo = y.PayOrderSetNo,
                    PayOrderNo = y.PayOrderNo,
                    WithdrawalNo = y.WithdrawalNo,
                    HospitalCode = y.HospitalCode,
                    PayOrderStatus = y.PayOrderStatus,
                    PayAmount = y.PayAmount,
                    SignDate = y.SignDate,
                    SignBy = y.SignBy,
                    ApproveDate = y.ApproveDate,
                    ApproveBy = y.ApproveBy,
                }).ToList();

                
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public async Task<List<ViewAaiHealthCheckupH>> GetCheckupHInPayOrder(string hoscode, string withdrawalNo)
        //{
        //    try
        //    {
        //        //var result = db.ViewAaiHealthCheckupHs.Where(x => x.HospitalCode == hoscode && x.BudgetYear == withdrawalNo.Substring(0, 4) && x.MonthBudyear == withdrawalNo.Substring(5)).ToList();

        //        //var result = null;
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        
        
        //}
    }
}
