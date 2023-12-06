
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Interface.PromoteHealth.Admin
{
    public interface IWithdrawalRequestListPromoteAdminServices
    {
        Task<AaiHealthCheckupH> GetByCheckId(int CheckupId);

        Task<List<AaiHealthWithdrawalHViewForAi>> GetAllForAI();
        Task<HospitalAi> GetAllHosForAI(string withdrawNo);

        Task<PersonForAi> GetAllPersonForAI(string withdrawNo, string hosCode);
        

    }
}
