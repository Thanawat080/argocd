using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.Base
{
    public interface IAaiHealthSetRefChecklistCfgBaseServices
    {
        Task<string> Create(List<AaiHealthSetRefChecklistCfgView> data);

        Task<string> Update(List<AaiHealthSetRefChecklistCfgView> data);
    }
}
