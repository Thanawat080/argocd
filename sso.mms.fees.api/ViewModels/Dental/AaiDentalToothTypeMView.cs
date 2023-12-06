using sso.mms.fees.api.Entities.Dental;

namespace sso.mms.fees.api.ViewModels.Dental
{
    public class AaiDentalToothTypeMView
    {
        public decimal ToothTypeId { get; set; }

        public string ToothNo { get; set; } = null!;

        public string ToothName { get; set; } = null!;

        public string ToothType { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime UpdateDate { get; set; }

        public string UpdateBy { get; set; } = null!;

        public string ImagePath { get; set; } = null!;
        public bool SelectTooth { get; set; }

        public virtual ICollection<AaiDentalCheckD> AaiDentalCheckDs { get; set; } = new List<AaiDentalCheckD>();

    }
}
