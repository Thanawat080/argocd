using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class SaveDetermineReferenceServices : ISaveDetermineReferenceValueExt
    {
        private readonly PromoteHealthContext db;

        public SaveDetermineReferenceServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }
        public async Task<string> saveandupdate(SaveDetermineReferenceValue data)
        {
            try
            {
                List<AaiHealthSetRefChecklistCfg> checklistcfg = new List<AaiHealthSetRefChecklistCfg>();
                List<AaiHealthSetRefNicknameCfg> checklistnickname = new List<AaiHealthSetRefNicknameCfg>();
                //List<AaiHealthSetRefDoctorM> setRefDoctorM = new List<AaiHealthSetRefDoctorM>();
                // make AaiHealthSetRefChecklistCfg
                foreach (var value in data.saveconfig)
                {
                    foreach (var cfg in value.CheckListCfgView.ToList())
                    {
                        AaiHealthSetRefChecklistCfg x = new AaiHealthSetRefChecklistCfg();
                        x.StartAge = cfg.StartAge;
                        x.SetRefId = cfg.SetRefId;
                        x.ChecklistId = cfg.ChecklistId;
                        x.ChecklistDtId = cfg.ChecklistDtId;
                        x.HospitalCode = cfg.HospitalCode;
                        x.EndAge = cfg.EndAge;
                        x.Sex = cfg.Sex;
                        x.SetRefValue = cfg.SetRefValue;
                        x.CreateDate = cfg.CreateDate;
                        x.CreateBy = cfg.CreateBy;
                        x.UpdateDate = cfg.UpdateDate;
                        x.UpdateBy = cfg.UpdateBy;
                        x.DeleteStatus = cfg.Delete ? "D" : "A";
                        checklistcfg.Add(x);

                    }
                }

                // make ChecklistDAndSetRefNickName
                foreach (var value1 in data.savenickname)
                {
                    AaiHealthSetRefNicknameCfg y = new AaiHealthSetRefNicknameCfg();
                    y.SetRefNnId = (decimal)value1.nickname.SetRefNnId;
                    y.SetRefName = value1.nickname.SetRefName;
                    y.UpdateDate = value1.nickname.UpdateDate;
                    y.UpdateBy = value1.nickname.UpdateBy;

                    checklistnickname.Add(y);
                }

    
                // doctor
                //foreach (var item in data.savedoctor)
                //{
                //    if (item.SetRefDoctorId == 0)
                //    {
                //        AaiHealthSetRefDoctorM doctor = new AaiHealthSetRefDoctorM
                //        {
                //            DoctorName = item.DoctorName,
                //            DeleteStatus = item.Delete ? "D" : "A",
                //            CreateDate = item.CreateDate,
                //            CreateBy = item.CreateBy,
                //            UpdateDate = item.UpdateDate,
                //            UpdateBy = item.UpdateBy,
                //            ChecklistId = item.ChecklistId,
                //            HospitalCode = item.HospitalCode
                //        };
                //        await db.AaiHealthSetRefDoctorMs.AddAsync(doctor);
                //    }
                //    else
                //    {
                //        AaiHealthSetRefDoctorM doctorupdate = new AaiHealthSetRefDoctorM();
                //        doctorupdate = db.AaiHealthSetRefDoctorMs.Where(x => x.SetRefDoctorId == item.SetRefDoctorId).FirstOrDefault();
                //        doctorupdate.UpdateDate = DateTime.Now;
                //        doctorupdate.DeleteStatus = item.DeleteStatus;
                //        doctorupdate.UpdateBy = data.username;
                //        doctorupdate.DoctorName = item.DoctorName;
                //        db.Update(doctorupdate);
                //    }
                //}


                // nickname 
                foreach (var item1 in checklistnickname)
                {

                    AaiHealthSetRefNicknameCfg nickname = new AaiHealthSetRefNicknameCfg();
                    nickname = db.AaiHealthSetRefNicknameCfgs.Where(x => x.SetRefNnId == item1.SetRefNnId).FirstOrDefault();
                    nickname.SetRefName = item1.SetRefName;
                    nickname.UpdateBy = item1.UpdateBy;
                    nickname.UpdateDate = item1.UpdateDate;
                    db.Update(nickname);
                }

                // cfg
                foreach (var item2 in checklistcfg)
                {
                    if (item2.SetRefId == 0)
                    {
                        await db.AaiHealthSetRefChecklistCfgs.AddAsync(item2);
                    }
                    else
                    {
                        AaiHealthSetRefChecklistCfg cfgupdate = new AaiHealthSetRefChecklistCfg();
                        cfgupdate = db.AaiHealthSetRefChecklistCfgs.Where(x => x.SetRefId == item2.SetRefId).FirstOrDefault();
                        cfgupdate.StartAge = item2.StartAge;
                        cfgupdate.EndAge = item2.EndAge;
                        cfgupdate.Sex = item2.Sex;
                        cfgupdate.SetRefValue = item2.SetRefValue;
                        cfgupdate.UpdateBy = data.username;
                        cfgupdate.UpdateDate = DateTime.Now;
                        cfgupdate.DeleteStatus = item2.DeleteStatus;
                        db.Update(cfgupdate);
                    }
                }
                db.SaveChanges();

                return "success";
            }
            catch {
                return null;
            }
        }

        public async Task<string> saveandupdateDoctor(DataSaveDoctor data) 
        {
            try
            {
                // doctor
                foreach (var item in data.doctor)
                {
                    if (item.SetRefDoctorId == 0)
                    {
                        AaiHealthSetRefDoctorM doctor = new AaiHealthSetRefDoctorM
                        {
                            DoctorName = item.DoctorName,
                            DeleteStatus = item.Delete ? "D" : "A",
                            CreateDate = item.CreateDate,
                            CreateBy = item.CreateBy,
                            UpdateDate = item.UpdateDate,
                            UpdateBy = item.UpdateBy,
                            ChecklistId = item.ChecklistId,
                            HospitalCode = item.HospitalCode
                        };
                        await db.AaiHealthSetRefDoctorMs.AddAsync(doctor);
                    }
                    else
                    {
                        AaiHealthSetRefDoctorM doctorupdate = new AaiHealthSetRefDoctorM();
                        doctorupdate = db.AaiHealthSetRefDoctorMs.Where(x => x.SetRefDoctorId == item.SetRefDoctorId).FirstOrDefault();
                        doctorupdate.UpdateDate = DateTime.Now;
                        doctorupdate.DeleteStatus = item.Delete ? "D" : "A";
                        doctorupdate.UpdateBy = data.username;
                        doctorupdate.DoctorName = item.DoctorName;
                        db.Update(doctorupdate);
                    }

                    db.SaveChanges();
                }
                return "success";
            }
            catch (Exception e) {
                return e.Message;
            }
        }
    }
}
