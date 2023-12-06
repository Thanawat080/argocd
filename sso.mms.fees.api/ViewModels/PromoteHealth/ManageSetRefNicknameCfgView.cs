using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class ManageSetRefNicknameCfgView
    {
        public string HospitalCode { get; set; }

        public List<AaiHealthCheckListDView> ChecklistD { get; set; }
    }
}
