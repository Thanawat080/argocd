using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IGetViewPayOrderExtServices
    {
        Task<List<PayOrderView>> GetByHoscode(string hoscode);

        //Task<List<ViewAaiHealthCheckupH>> GetCheckupHInPayOrder(string hoscode, string withdrawalNo);
    }
}
