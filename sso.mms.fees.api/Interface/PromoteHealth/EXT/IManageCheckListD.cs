﻿using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IManageCheckListD
    {
        Task<List<CheckListDAndManageChecklistCfg>> GetCheckListD(decimal? checklistMId, string? hosCode);
    }
}
