using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.ext.ViewModels.PromoteHealth
{
    public class DetermineReferenceView
    {
    }

    public class menu
    {
        public string title { get; set; }
        public int Id { get; set; }
        public int type { get; set; }
        public string typeitem { get; set; }
        public List<AaiHealthSetRefChecklistCfgView> table = new List<AaiHealthSetRefChecklistCfgView>();
    }

    public class model
    {
        public int Id { get; set; }
        public int? startAge { get; set; }
        public int? endAge { get; set; }
        public string sex { get; set; }
        public string value { get; set; }
        public bool insert { get; set; }

    }
}
