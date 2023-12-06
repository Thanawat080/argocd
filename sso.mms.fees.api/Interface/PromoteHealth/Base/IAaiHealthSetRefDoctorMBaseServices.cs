using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAaiHealthSetRefDoctorBaseServices
    {
        Task<string> Create(List<AaiHealthSetRefDoctorView> data);

        Task<string> Update(List<AaiHealthSetRefDoctorView> data);
        Task<string> Delete(List<AaiHealthSetRefDoctorView> data);
    }
}
