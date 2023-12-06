using System;
using System.Collections.Generic;

namespace sso.mms.helper.Data;

public partial class SessionUserT
{
    public int Id { get; set; }

    public string AccessToken { get; set; } = null!;

    public string ShortToken { get; set; } = null!;

    public bool IsActive { get; set; }

    public int IsStatus { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string? RealmGroup { get; set; }

    public string? ChannelLogin { get; set; }

    public int? HospitalUserMId { get; set; }

    public int? SsoUserMId { get; set; }

    public int? AuditorUserMId { get; set; }

    public string? AccessType { get; set; }

    public virtual AuditorUserM? AuditorUserM { get; set; }

    public virtual HospitalUserM? HospitalUserM { get; set; }

    public virtual SsoUserM? SsoUserM { get; set; }
}
