using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;

namespace sso.mms.fees.api.Interface.Dental.EXT
{
    public interface IHistoryWithdrawals 
    {
        Task<List<AaiDentalWithdrawTView>> GetHistorys();
    }
}
