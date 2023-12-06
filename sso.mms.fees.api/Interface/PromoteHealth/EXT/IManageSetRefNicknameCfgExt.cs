using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IManageSetRefNicknameCfgExt
    {
        Task<List<AaiHealthSetRefNicknameCfg>> GetSetRefNicknameCfg(ManageSetRefNicknameCfgView data);

        Task<string> Update(List<AaiHealthSetRefNicknameCfg> cfg);
    }
}
