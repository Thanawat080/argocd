using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class AaiDentalCheckH
{
    public decimal CheckHId { get; set; }

    public decimal HospitalId { get; set; }

    public int SsoOrgId { get; set; }

    public string PersonalId { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public int National { get; set; }

    public string SsoStatus { get; set; } = null!;

    public DateTime CheckDate { get; set; }

    public string PhoneNo { get; set; } = null!;

    public decimal BalanceMoney { get; set; }

    public string PortalPort { get; set; } = null!;

    public decimal DentalCarDId { get; set; }

    public bool IsFromReader { get; set; }

    public string Reason { get; set; } = null!;

    public string ConfirmBy { get; set; } = null!;

    public string CheckStatus { get; set; } = null!;

    public string CheckDocu { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public decimal BalanceUsed { get; set; }

    public virtual ICollection<AaiDentalCheckD> AaiDentalCheckDs { get; set; } = new List<AaiDentalCheckD>();

    public virtual ICollection<AaiDentalTreatD> AaiDentalTreatDs { get; set; } = new List<AaiDentalTreatD>();

    public virtual ICollection<AaiDentalWithdrawT> AaiDentalWithdrawTs { get; set; } = new List<AaiDentalWithdrawT>();

    public virtual AaiDentalCarD DentalCarD { get; set; } = null!;
}
