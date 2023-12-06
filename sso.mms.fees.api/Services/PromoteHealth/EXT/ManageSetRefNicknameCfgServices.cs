using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Collections.Generic;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class ManageSetRefNicknameCfgServices : IManageSetRefNicknameCfgExt
    {
        private readonly PromoteHealthContext db;

        public ManageSetRefNicknameCfgServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }

        public async Task<List<AaiHealthSetRefNicknameCfg>> GetSetRefNicknameCfg(ManageSetRefNicknameCfgView data)
        {
            try
            {
                List<AaiHealthSetRefNicknameCfg> result = new List<AaiHealthSetRefNicknameCfg>();
                foreach (var item in data.ChecklistD)
                {
                    var item1 = db.AaiHealthSetRefNicknameCfgs.Where(x => x.ChecklistDId == item.ChecklistDtId && x.HospitalCode == data.HospitalCode).FirstOrDefault();
                    if (item1 != null) { 
                        result.Add(item1);
                    }
                }


                if (result.Count() == 0)
                {
                    foreach (var item in data.ChecklistD)
                    {
                        AaiHealthSetRefNicknameCfg setRefNicknameToadd = new AaiHealthSetRefNicknameCfg
                        {
                            HospitalCode = data.HospitalCode,
                            ChecklistDId = item.ChecklistDtId,
                            SetRefName = item.ChecklistDtName,
                            CreateDate = item.CreateDate,
                            CreateBy = item.CreateBy,
                            UpdateDate = item.UpdateDate,
                            UpdateBy = item.UpdateBy
                        };
                        await db.AaiHealthSetRefNicknameCfgs.AddRangeAsync(setRefNicknameToadd);
                    }
                    db.SaveChanges();
                    foreach (var item in data.ChecklistD)
                    {
                        var item1 = db.AaiHealthSetRefNicknameCfgs.Where(x => x.ChecklistDId == item.ChecklistDtId && x.HospitalCode == data.HospitalCode).FirstOrDefault();
                        if (item1 != null)
                        {
                            result.Add(item1);
                        }
                    }
                    return result;
                }
                else {
                    if (result.Count() != data.ChecklistD.Count()) 
                    {
                        var res = data.ChecklistD.Where(x => result.All(y => x.ChecklistDtId  != y.ChecklistDId));
                        foreach (var item in res)
                        {
                            AaiHealthSetRefNicknameCfg setRefNicknameToadd = new AaiHealthSetRefNicknameCfg
                            {
                                HospitalCode = data.HospitalCode,
                                ChecklistDId = item.ChecklistDtId,
                                SetRefName = item.ChecklistDtName,
                                UpdateDate = item.UpdateDate,
                                UpdateBy = item.UpdateBy
                            };
                            await db.AaiHealthSetRefNicknameCfgs.AddRangeAsync(setRefNicknameToadd);
                        }
                        db.SaveChanges();
                        foreach (var item in data.ChecklistD)
                        {
                            var item1 = db.AaiHealthSetRefNicknameCfgs.Where(x => x.ChecklistDId == item.ChecklistDtId && x.HospitalCode == data.HospitalCode).FirstOrDefault();
                            if (item1 != null)
                            {
                                result.Add(item1);
                            }
                        }
                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<string> Update(List<AaiHealthSetRefNicknameCfg> cfg)
        {
            try
            {
                //update 
                foreach (var item in cfg)
                {
                    AaiHealthSetRefNicknameCfg nicknamecfg = new AaiHealthSetRefNicknameCfg();
                    nicknamecfg = db.AaiHealthSetRefNicknameCfgs.Where(x => x.SetRefNnId == item.SetRefNnId).FirstOrDefault();
                    nicknamecfg.SetRefName = item.SetRefName;
                    nicknamecfg.UpdateDate = DateTime.Now;
                    nicknamecfg.UpdateBy = item.UpdateBy;
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
