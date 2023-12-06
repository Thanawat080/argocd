using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IManageAaiHealthSetRefDoctorCfg
    {
        Task<List<AaiHealthSetRefDoctorMView>> GetDoctor(decimal? checklistMId, string? hosCode);
    }
}
