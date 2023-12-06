using System;
using System.Collections.Generic;

namespace sso.mms.helper.OracleIdpModels;

public partial class UserMAuditor
{
    public int Id { get; set; }

    public string? PrefixMCode { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? IdenficationNumber { get; set; }

    public string? CertNo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageName { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? UserName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? Position { get; set; }
}
