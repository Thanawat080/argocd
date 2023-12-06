using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.PortalModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AaiHealthSetRefChecklistCfgServices : IAaiHealthSetRefChecklistCfgBaseServices
    {
        private readonly PromoteHealthContext db;

        public AaiHealthSetRefChecklistCfgServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }

        public async Task<string> Create(List<AaiHealthSetRefChecklistCfgView> data)
        {
            try
            {
                List<AaiHealthSetRefChecklistCfg> databaseModels = new List<AaiHealthSetRefChecklistCfg>();

                foreach (var item in data)
                {
                    AaiHealthSetRefChecklistCfg dtoListToAdd = new AaiHealthSetRefChecklistCfg
                    {
                        ChecklistId = (decimal)item.ChecklistId,
                        ChecklistDtId = (decimal)item.ChecklistDtId,
                        HospitalCode = item.HospitalCode,
                        StartAge = (long)item.StartAge,
                        EndAge = (long)item.EndAge,
                        Sex = item.Sex,
                        SetRefValue = item.SetRefValue,
                        CreateDate = DateTime.Now,
                        CreateBy = item.CreateBy,
                        UpdateDate = DateTime.Now,
                        UpdateBy = item.UpdateBy
                    };

                    databaseModels.Add(dtoListToAdd);
                }

                await db.AaiHealthSetRefChecklistCfgs.AddRangeAsync(databaseModels);

                db.SaveChanges();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> Update(List<AaiHealthSetRefChecklistCfgView> data)
        {
            try
            {
                //update 
                foreach (var item in data)
                {
                    AaiHealthSetRefChecklistCfg checklistcfg = new AaiHealthSetRefChecklistCfg();
                    checklistcfg = db.AaiHealthSetRefChecklistCfgs.Where(x => x.SetRefId == item.SetRefId).FirstOrDefault();
                    checklistcfg.StartAge = (long)item.StartAge;
                    checklistcfg.EndAge = (long)item.EndAge;
                    checklistcfg.Sex = item.Sex;
                    checklistcfg.SetRefValue = item.SetRefValue;
                    checklistcfg.UpdateDate = DateTime.Now;
                    checklistcfg.UpdateBy = item.UpdateBy;
                }
                db.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
