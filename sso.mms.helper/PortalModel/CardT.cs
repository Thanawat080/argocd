using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class CardT
{
    public int Id { get; set; }

    public int? CardMId { get; set; }

    public int? HospitalId { get; set; }

    public string? Name { get; set; }

    public string? UploadPath { get; set; }

    public string? UploadFile { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual CardM? CardM { get; set; }
}
