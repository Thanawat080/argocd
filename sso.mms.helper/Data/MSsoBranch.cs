using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class MSsoBranch
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public short? Moo { get; set; }

    public int? SubDistrictId { get; set; }

    public int? DistrictId { get; set; }

    public int? ZipCode { get; set; }

    public short? IsActive { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? Fax { get; set; }

    public string? Tel { get; set; }
}
