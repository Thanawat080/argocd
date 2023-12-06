using System;
using System.Collections.Generic;

namespace sso.mms.fees.api.Entities.Dental;

public partial class HosHospital
{
    public int HospId { get; set; }

    public string HospCode5 { get; set; } = null!;

    public string? HospCode7 { get; set; }

    public string HospCode9 { get; set; } = null!;

    public string HospName { get; set; } = null!;

    public string HospOfficialName { get; set; } = null!;

    public string HospDisplayName { get; set; } = null!;

    public string? HospAddrNo { get; set; }

    public string? HospRoad { get; set; }

    public int? HospSubdistId { get; set; }

    public int? HospDistId { get; set; }

    public int? HospProvId { get; set; }

    public string? HospPostcode { get; set; }

    public string? HospTel { get; set; }

    public string? HospFax { get; set; }

    public string? HospWebsite { get; set; }

    public string? HospEmail { get; set; }

    public string HospType { get; set; } = null!;

    public int? HospTypeId { get; set; }

    public int? AffId { get; set; }

    public int? CorpTypeId { get; set; }

    public string? CorpName { get; set; }

    public string? CorpTax { get; set; }

    public string? CorpAddrNo { get; set; }

    public string? CorpRoad { get; set; }

    public int? CorpSubdistId { get; set; }

    public int? CorpDistId { get; set; }

    public int? CorpProvId { get; set; }

    public string? CorpPostcode { get; set; }

    public string? CorpTel { get; set; }

    public string? CorpFax { get; set; }

    public string? CorpWebsite { get; set; }

    public string? CorpEmail { get; set; }

    public string? HospPerson { get; set; }

    public string CreateUser { get; set; } = null!;

    public DateTime CreateDtm { get; set; }

    public string UpdateUser { get; set; } = null!;

    public DateTime UpdateDtm { get; set; }
}
