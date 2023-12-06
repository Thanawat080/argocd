using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.PortalModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AaiHealthSetRefDoctorCfgServices : IAaiHealthSetRefDoctorBaseServices
    {
        private readonly PromoteHealthContext db;

        public AaiHealthSetRefDoctorCfgServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }

        public async Task<string> Create(List<AaiHealthSetRefDoctorView> data)
        {
            try
            {
                List<AaiHealthSetRefDoctorM> databaseModels = new List<AaiHealthSetRefDoctorM>();

                foreach (var item in data)
                {
               
                    AaiHealthSetRefDoctorM dtoListToAdd = new AaiHealthSetRefDoctorM
                    {
                        SetRefDoctorId = item.SetRefDoctorId,
                        DoctorName = item.DoctorName,
                        DeleteStatus = "A",
                        ChecklistId = item.ChecklistId,
                        HospitalCode = item.HospitalCode,
                        CreateDate = DateTime.Now,
                        CreateBy = item.CreateBy,
                        UpdateDate = DateTime.Now,
                        UpdateBy = item.UpdateBy,
                      
                    };

                    databaseModels.Add(dtoListToAdd);
                }

                await db.AaiHealthSetRefDoctorMs.AddRangeAsync(databaseModels);

                db.SaveChanges();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

      

        public async Task<string> Delete(List<AaiHealthSetRefDoctorView> data)
        {
            try
            {
                foreach (var item in data)
                {
                    AaiHealthSetRefDoctorM checklistcfg = new AaiHealthSetRefDoctorM();
                    checklistcfg = db.AaiHealthSetRefDoctorMs.FirstOrDefault(x => x.SetRefDoctorId == item.SetRefDoctorId);
                    checklistcfg.DeleteStatus = "D";
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

        public async Task<string> Update(List<AaiHealthSetRefDoctorView> data)
        {
            try
            {
                //update 
                foreach (var item in data)
                {
                    AaiHealthSetRefDoctorM checklistcfg = new AaiHealthSetRefDoctorM();
                    checklistcfg = db.AaiHealthSetRefDoctorMs.FirstOrDefault(x => x.SetRefDoctorId == item.SetRefDoctorId);
                    checklistcfg.DoctorName = item.DoctorName;
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
