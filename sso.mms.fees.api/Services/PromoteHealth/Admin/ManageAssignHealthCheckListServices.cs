using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.Admin
{
    public class ManageAssignHealthCheckListServices : IManageAssignHealthCheckListAdminServices
    {
        private readonly PromoteHealthContext db;

        public ManageAssignHealthCheckListServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<string> ManageTypeCreate(ManageAssignHealthCheckListView data)
        {

            data.dataM.UpdateDate = DateTime.Now;
            data.dataM.UpdateBy = "system";
            data.dataM.CreateDate = DateTime.Now;
            data.dataM.CreateBy = "system";
            await db.AaiHealthChecklistMs.AddAsync(data.dataM);
            db.SaveChanges();
            decimal checklistid = data.dataM.ChecklistId;
            foreach (var item1 in data.dataD)
            {
                AaiHealthChecklistD checklistd = new AaiHealthChecklistD();
                checklistd.ChecklistDtName = item1.ChecklistDtName;
                checklistd.ChecklistDtStatus = (item1.ChecklistDtStatus ? "A" : "I");
                checklistd.ChecklistId = checklistid;
                checklistd.IsOption = item1.IsOption;
                checklistd.CreateDate = item1.CreateDate;
                checklistd.CreateBy = item1.CreateBy;
                checklistd.UpdateDate = item1.UpdateDate;
                checklistd.UpdateBy = item1.UpdateBy;
                await db.AaiHealthChecklistDs.AddAsync(checklistd);
            }
            db.SaveChanges();

            return "success";
        }

        public async Task<string> ManageTypeEdit(ManageAssignHealthCheckListView data)
        {
            if (data.dataM.ChecklistId != null)
            {
                AaiHealthChecklistM checklistd = new AaiHealthChecklistM();
                checklistd = db.AaiHealthChecklistMs.Where(x => x.ChecklistId == data.dataM.ChecklistId).FirstOrDefault();
                checklistd.IsSetRef = data.dataM.IsSetRef;
                checklistd.ChecklistName = data.dataM.ChecklistName;
                checklistd.ChecklistPrice = data.dataM.ChecklistPrice;
                checklistd.ChecklistStatus = data.dataM.ChecklistStatus;
                checklistd.ChecklistCode = data.dataM.ChecklistCode;
                checklistd.ChecklistShortname = data.dataM.ChecklistShortname;
                checklistd.UpdateDate = DateTime.Now;
                checklistd.UpdateBy = "system";
                db.SaveChanges();
            }
            foreach (var item in data.dataD) 
            {
                if (item.ChecklistDtId == 0)
                {
                    AaiHealthChecklistD checklistd1 = new AaiHealthChecklistD();
                    checklistd1.ChecklistDtName = item.ChecklistDtName;
                    checklistd1.ChecklistDtStatus = (item.ChecklistDtStatus ? "A" : "I");
                    checklistd1.ChecklistId = item.ChecklistId;
                    checklistd1.IsOption = item.IsOption;
                    checklistd1.CreateDate = item.CreateDate;
                    checklistd1.CreateBy = item.CreateBy;
                    checklistd1.UpdateDate = item.UpdateDate;
                    checklistd1.UpdateBy = item.UpdateBy;
                    await db.AaiHealthChecklistDs.AddAsync(checklistd1);
                    db.SaveChanges();
                }
                else {
                    AaiHealthChecklistD checklistd = new AaiHealthChecklistD();
                    checklistd = db.AaiHealthChecklistDs.Where(x => x.ChecklistDtId == item.ChecklistDtId).FirstOrDefault();
                    checklistd.ChecklistDtName = item.ChecklistDtName;
                    checklistd.ChecklistDtStatus = (item.ChecklistDtStatus ? "A" : "I");
                    checklistd.UpdateDate = DateTime.Now;
                    checklistd.UpdateBy = "system";
                    db.SaveChanges();
                }
                
            }
            return "success";
        }
    }
}
