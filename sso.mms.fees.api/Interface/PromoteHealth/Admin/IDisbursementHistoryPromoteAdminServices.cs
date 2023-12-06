using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Admin
{
    public interface IDisbursementHistoryPromoteAdminServices
    {
        Task<List<PayOrderHistoryView>> GetDisbursementList();
    }
}
