using sso.mms.fees.api.Entities.Dental;

namespace sso.mms.fees.api.ViewModels.Dental
{
    public class AaiDentalWithdrawTView
    {
        public int WithdrawId { get; set; }

        public string WithdrawNo { get; set; } = null!;

        public decimal CheckDId { get; set; }

        public decimal CheckHId { get; set; }

        public decimal Expense { get; set; }

        public decimal SsoPay { get; set; }

        public string CheckStatus { get; set; } = null!;

        public DateTime WithdrawDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; } = null!;

        public DateTime UpdateDate { get; set; }

        public string UpdateBy { get; set; } = null!;

        public virtual AaiDentalCheckDView CheckD { get; set; } = null!;

        public virtual AaiDentalCheckHView CheckH { get; set; } = null!;
    }
}
