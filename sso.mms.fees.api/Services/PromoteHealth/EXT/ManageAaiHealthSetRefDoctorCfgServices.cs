using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class ManageAaiHealthSetRefDoctorCfgServices : IManageAaiHealthSetRefDoctorCfg
    {
        private readonly PromoteHealthContext db;

        public ManageAaiHealthSetRefDoctorCfgServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<List<AaiHealthSetRefDoctorMView>> GetDoctor(decimal? checklistMId, string? hosCode)
        {
            try
            {
                var result = db.AaiHealthSetRefDoctorMs
                    .Where(w => w.ChecklistId == checklistMId && w.DeleteStatus == "A" && w.HospitalCode == hosCode)
                    .ToList().Select(x => new AaiHealthSetRefDoctorMView
                    {
                        SetRefDoctorId = x.SetRefDoctorId,
                        DoctorName = x.DoctorName,
                        DeleteStatus = x.DeleteStatus,
                        CreateDate = x.CreateDate,
                        CreateBy = x.CreateBy,
                        UpdateDate = x.UpdateDate,
                        UpdateBy = x.UpdateBy,
                        ChecklistId = x.ChecklistId,
                        HospitalCode = x.HospitalCode
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
