using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class UserMHospital
{
    public int Id { get; set; }

    public string? PrefixMCode { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? SsoBranchCode { get; set; }

    public string? MedicalName { get; set; }

    public string? MedicalCode { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageName { get; set; }

    public int? GroupId { get; set; }

    public string? Address { get; set; }

    public int? Moo { get; set; }

    public string? DistrictCode { get; set; }

    public string? SubdistrictCode { get; set; }

    public string? ProvinceCode { get; set; }

    public string? ZipCode { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public bool? IsActive { get; set; }

    public int? HospitalMId { get; set; }

    public string? UserName { get; set; }

    public string? IdenficationNumber { get; set; }

    public bool? IsCheckDopa { get; set; }

    public string? PositionName { get; set; }

    public string? HospitalMCode { get; set; }
}
