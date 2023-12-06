using sso.mms.fees.api.Entities.Dental;
using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.ViewModels.Dental;

public partial class AaiDentalCarHView
{
    public decimal? DentalCarHId { get; set; }

    public string? HospitalCode { get; set; }

    public string PlaceName { get; set; } = null!;

    public DateTime ServiceDate { get; set; }

    public DateTime ServiceStartDate { get; set; }

    public DateTime ServiceEndDate { get; set; }

    public string PlaceProvince { get; set; } = null!;

    public string PlaceDistrict { get; set; } = null!;

    public string PlaceSubDistrict { get; set; } = null!;

    public string? CarType { get; set; } = null!;

    public string? RegisterDoc { get; set; }

    public string? DantalCarStatus { get; set; }

    public string? ChangeDesc { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }
    public string? RegisterDocFileName { get; set; }

    public virtual ICollection<AaiDentalCarD> AaiDentalCarDs { get; set; } = new List<AaiDentalCarD>();

    public int sumCar { get; set; }
    public string AllLicensePlate { get; set; }
}
public  class InsertDataViewModel
{ 
    public List<AaiDentalCarDViewModel>? carD { get; set; }
    public AaiDentalCarHViewModel? carH { get; set; }
     
}
public class AaiDentalCarDViewModel
{
    public decimal? DentalCarDId { get; set; }

    public decimal? DentalCarHId { get; set; }

    public string LicensePlate { get; set; } 

    public string? Remark { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } 

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } 

    public string? CarType { get; set; }
}
public class AaiDentalCarHViewModel {
    public decimal? DentalCarHId { get; set; }

    public string? HospitalCode { get; set; }

    public string PlaceName { get; set; } 

    public DateTime ServiceDate { get; set; }

    public DateTime ServiceStartDate { get; set; }

    public DateTime ServiceEndDate { get; set; }

    public string PlaceProvince { get; set; }

    public string PlaceDistrict { get; set; }

    public string PlaceSubDistrict { get; set; }

    public string? RegisterDoc { get; set; }

    public string? DantalCarStatus { get; set; }

    public string? ChangeDesc { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public int? SsoOrgId { get; set; }
    public string? RegisterDocFileName { get; set; }
}
public class ChangeDesViewModel
{
    public decimal? DentalCarHId { get; set; }
    public string? ChangeDesc { get; set; }

}