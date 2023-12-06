using Microsoft.AspNetCore.Components.Forms;

namespace sso.mms.fees.api.ViewModels.PromoteHealth
{
    public class WithdrawalView
    {

        public string WithdrawalNo { get; set; } = null!;

        public string HospitalCode { get; set; } = null!;

        public string? WithdrawalDoc { get; set; }

        public decimal? SumChecklistPrice { get; set; }

    }

    public class WithdrawalDoc
    {
        public string WithdrawalNo { get; set; } = null!;

        public string HospitalCode { get; set; } = null!;

        public IBrowserFile? File { get; set; }

    }

    public class WithdrawalDocView
    {
        public string WithdrawalNo { get; set; } = null!;

        public string HospitalCode { get; set; } = null!;

        public IFormFile? File { get; set; }

    }

}
