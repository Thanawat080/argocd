using System;
using System.Collections.Generic;

namespace sso.mms.helper.PortalModel;

public partial class CardM
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public int? IsStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<CardT> CardTs { get; set; } = new List<CardT>();
}
