using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class ManageCheckListDService : IManageCheckListD
    {
        private readonly PromoteHealthContext db;

        public ManageCheckListDService(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<List<CheckListDAndManageChecklistCfg>> GetCheckListD(decimal? checklistMId, string? hosCode)
        {
            try
            {

                var result = db.AaiHealthChecklistDs
                    .Where(w => w.ChecklistId == checklistMId)
                    .Include(s => s.AaiHealthSetRefChecklistCfgs.Where(z => z.HospitalCode == hosCode && z.DeleteStatus == "A"))
                    .ToList().Select(x => new CheckListDAndManageChecklistCfg
                    {
                        ChecklistDtName = db.AaiHealthSetRefNicknameCfgs.FirstOrDefault(y => x.ChecklistDtId == y.ChecklistDId && y.HospitalCode == hosCode) != null ? db.AaiHealthSetRefNicknameCfgs.FirstOrDefault(y => x.ChecklistDtId == y.ChecklistDId && y.HospitalCode == hosCode).SetRefName : null,
                        ChecklistDtId = x.ChecklistDtId,
                        ChecklistId = x.ChecklistId,
                        IsOption = x.IsOption,
                        ChecklistDtStatus = x.ChecklistDtStatus,
                        CreateDate = x.CreateDate,
                        CreateBy = x.CreateBy,
                        UpdateDate = x.UpdateDate,
                        UpdateBy = x.UpdateBy,
                        CheckListCfgView = x.AaiHealthSetRefChecklistCfgs.Select(y => new CheckListCfgView
                        {
                            SetRefId = y.SetRefId,
                            ChecklistId = y.ChecklistId,
                            ChecklistDtId = y.ChecklistDtId,
                            HospitalCode = y.HospitalCode,
                            StartAge = y.StartAge,
                            EndAge = y.EndAge,
                            Sex = y.Sex,
                            SetRefValue = y.SetRefValue,
                            CreateDate = y.CreateDate,
                            CreateBy = y.CreateBy,
                            UpdateDate = y.UpdateDate,
                            UpdateBy = y.UpdateBy,
                            DeleteStatus = y.DeleteStatus,
                        }).ToList()
                    }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
