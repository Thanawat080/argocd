using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAaiHealthReserveHBaseServices
    {
        Task<string> UpdateReserveStatus(decimal reserveId, string status);
    }
}
