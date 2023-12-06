using sso.mms.fees.api.Configs;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Configuration;
using sso.mms.fees.api.Services.PromoteHealth.Base;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class ManageRecordChecklistServices : IManageRecordChecklistExtServices
    {
        private readonly PromoteHealthContext db;
        private readonly HttpClient httpClient;
        public ManageRecordChecklistServices(PromoteHealthContext DbContext, HttpClient httpClient)
        {
            db = DbContext;
            this.httpClient = httpClient;
        }

        public async Task<ManageRecordChecklistView> GetChecklistByCheckupId(decimal checkupId)
        {
            try
            {
                ManageRecordChecklistView response = new ManageRecordChecklistView();
                var checkupH = db.AaiHealthCheckupHs.Where(x => x.CheckupId == checkupId).Select(y => new RecordCheckupHView
                {
                    CheckupId = y.CheckupId,
                    CheckupNo = y.CheckupNo,
                    ReserveId = y.ReserveId,
                    HospitalCode = y.HospitalCode,
                    PersonalId = y.PersonalId,
                    PatientName = y.PatientName,
                    PatientSex = y.PatientSex,
                    PatientWeight = y.PatientWeight,
                    PatientHeight = y.PatientHeight,
                    PatientPressure = y.PatientPressure,
                    CheckupDate = y.CheckupDate,
                    IsUd = y.IsUd,
                    DeleteStatus = y.DeleteStatus,
                    UseStatus = y.UseStatus,
                    CreateDate = y.CreateDate,
                    CreateBy = y.CreateBy,
                    UpdateDate = y.UpdateDate,
                    UpdateBy = y.UpdateBy,
                    PatientSurname = y.PatientSurname,
                    PatientTel = y.PatientTel,
                    PatientAge = y.PatientAge
                }).FirstOrDefault();

                var disease = db.AaiHealthSetUnderlyingDiseaseMs.Where(x => x.CheckupId == checkupId).FirstOrDefault();
                response.DiseaseReason = disease == null ? null : disease.Remark;

                if (disease != null)
                {
                    response.DiseaseViews = new List<diseaseView>
                {
                new diseaseView() {
                Id = 1,
                value = "โรคเบาหวาน",
                active = (bool)disease.IsDb,
            },
        new diseaseView() {
                Id = 2,
                value = "โรคไขมันในเลือดสูง",
                active = (bool)disease.IsHpldm
            },
        new diseaseView() {
                Id = 3,
                value = "โรคอ้วน",
                active = (bool)disease.IsObst
            },new diseaseView() {
                Id = 4,
                value = "โรคความดันโลหิตสูง",
                active = (bool)disease.IsHpts
            },new diseaseView() {
                Id = 5,
                value = "โรคทางเดินหายใจเรื้อรัง",
                active = (bool)disease.IsCopd
            }
            ,new diseaseView() {
                Id = 6,
                value = "โรคไตวายเรื้อรัง",
                active = (bool)disease.IsCkd
            }
            ,new diseaseView() {
                Id = 7,
                value = "โรคมะเร็ง",
                active = (bool)disease.IsCc
            }
            ,new diseaseView() {
                Id = 12,
                value = "โรคหัวใจและหลอดเลือด",
                active = (bool)disease.IsCvds
            }

            ,new diseaseView() {
                Id = 8,
                value = "โรคถุงลมโป่งพอง",
                active = (bool)disease.IsEps
            }
            ,new diseaseView() {
                Id = 9,
                value = "โรคกระเพาะอาหาร",
                active = (bool)disease.IsDps
            }

            ,new diseaseView() {
                Id = 10,
                value = "โรคโลหิตจาง",
                active = (bool)disease.IsTlsm
            }
            ,new diseaseView() {
                Id = 11,
                value = "เป็นอื่นๆ",
                active = (bool)disease.IsOther
            }
        };
                }



                //var AlreadyCheckListServices = new AlreadyCheckListServices(db);
                // wait 850 api
                //var res850 = await AlreadyCheckListServices.api850(checkupH.PersonalId);
                var checklistMs = db.AaiHealthChecklistMs.Where(x => x.ChecklistStatus == "A").ToList().Select(y => new RecordChecklistMView
                {
                    ChecklistId = y.ChecklistId,
                    ChecklistName = y.ChecklistName,
                    ChecklistPrice = y.ChecklistPrice,
                    ChecklistCode = y.ChecklistCode,
                    IsSetRef = y.IsSetRef,
                    StatusCheck = false,

                }).ToList();

                var checklistTs = db.ViewAaiHealthCheckupListTs.Where(x => x.CheckupId == checkupId && x.DeleteStatus == "I").ToList();
                for (var i = 0; i < checklistTs.Count(); i++)
                {
                    for (var j = 0; j < checklistMs.Count(); j++)
                    {
                        if (checklistMs[j].ChecklistCode == checklistTs[i].ChecklistCode)
                        {
                            checklistMs[j].isExport = (bool)checklistTs[i].IsExport;
                            checklistMs[j].StatusCheck = true;
                        }
                    }
                }
                response.ChecklistMViews = checklistMs;
                List<RecordChecklistDView> checklistDs = new List<RecordChecklistDView>();
                RecordSetRefNickNameView nickName = new RecordSetRefNickNameView();
                RecordSetRefChecklistCfgView checklistCfg = new RecordSetRefChecklistCfgView();
                List<RecordSetRefDoctorView> doctorMs = new List<RecordSetRefDoctorView>();
                for (var i = 0; i < checklistMs.Count(); i++)
                {
                    if (response.ChecklistMViews[i].StatusCheck == true)
                    {
                        response.ChecklistMViews[i].IsCheck = db.AaiHealthCheckupListTs.Where(x => x.ChecklistId == response.ChecklistMViews[i].ChecklistId && x.CheckupId == checkupId).FirstOrDefault().IsChecked;
                        response.ChecklistMViews[i].ResultStatus = db.AaiHealthCheckupListTs.Where(x => x.ChecklistId == response.ChecklistMViews[i].ChecklistId && x.CheckupId == checkupId).FirstOrDefault().ResultStatus;
                        response.ChecklistMViews[i].SelectDoctorId = db.AaiHealthCheckupListTs.Where(x => x.ChecklistId == response.ChecklistMViews[i].ChecklistId && x.CheckupId == checkupId).FirstOrDefault().SetRefDoctorId;
                        response.ChecklistMViews[i].CheckupListId = db.AaiHealthCheckupListTs.Where(x => x.ChecklistId == response.ChecklistMViews[i].ChecklistId && x.CheckupId == checkupId).FirstOrDefault().CheckupListId;
                    }
                    checklistDs = db.AaiHealthChecklistDs.Where(x => x.ChecklistId == checklistMs[i].ChecklistId && x.ChecklistDtStatus == "A").ToList().Select(y => new RecordChecklistDView
                    {
                        ChecklistDtId = y.ChecklistDtId,
                        ChecklistDtName = y.ChecklistDtName,
                        ChecklistId = y.ChecklistId,
                        IsOption = y.IsOption,
                        ChecklistDtStatus = y.ChecklistDtStatus,
                        CheckValue = db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.ChecklistDtId == y.ChecklistDtId && x.CheckupListId == response.ChecklistMViews[i].CheckupListId) == null ? null : db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.ChecklistDtId == y.ChecklistDtId && x.CheckupListId == response.ChecklistMViews[i].CheckupListId).ResultCheckValue,
                        IsNormal = db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.ChecklistDtId == y.ChecklistDtId && x.CheckupListId == response.ChecklistMViews[i].CheckupListId) == null ? null : db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.ChecklistDtId == y.ChecklistDtId && x.CheckupListId == response.ChecklistMViews[i].CheckupListId).IsNormal,
                        Suggestion = db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.ChecklistDtId == y.ChecklistDtId && x.CheckupListId == response.ChecklistMViews[i].CheckupListId) == null ? null : db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.ChecklistDtId == y.ChecklistDtId && x.CheckupListId == response.ChecklistMViews[i].CheckupListId).Suggession,
                    }).ToList();
                    response.ChecklistMViews[i].ChecklistDViews = checklistDs;
                    for (var j = 0; j < checklistDs.Count(); j++)
                    {
                        nickName = db.AaiHealthSetRefNicknameCfgs.Where(x => x.ChecklistDId == checklistDs[j].ChecklistDtId && x.HospitalCode == checkupH.HospitalCode).Select(y => new RecordSetRefNickNameView
                        {
                            SetRefNnId = y.SetRefNnId,
                            HospitalCode = y.HospitalCode,
                            ChecklistDId = y.ChecklistDId,
                            SetRefName = y.SetRefName,
                        }).FirstOrDefault();

                        response.ChecklistMViews[i].ChecklistDViews[j].RecordSetRefNickNameView = nickName;
                        if (checklistMs[i].IsSetRef == true)
                        {
                            checklistCfg = db.AaiHealthSetRefChecklistCfgs.Where(x => x.ChecklistDtId == checklistDs[j].ChecklistDtId && x.HospitalCode == checkupH.HospitalCode && x.Sex == checkupH.PatientSex && x.EndAge >= checkupH.PatientAge && x.StartAge <= checkupH.PatientAge && x.DeleteStatus == "A").Select(y => new RecordSetRefChecklistCfgView
                            {
                                SetRefId = y.SetRefId,
                                ChecklistDtId = y.ChecklistDtId,
                                ChecklistId = y.ChecklistId,
                                HospitalCode = y.HospitalCode,
                                StartAge = y.StartAge,
                                EndAge = y.EndAge,
                                Sex = y.Sex,
                                SetRefValue = y.SetRefValue,
                                DeleteStatus = y.DeleteStatus
                            }).FirstOrDefault();
                            response.ChecklistMViews[i].ChecklistDViews[j].RecordSetRefChecklistCfgView = checklistCfg;
                        }
                    }

                    doctorMs = db.AaiHealthSetRefDoctorMs.Where(x => x.ChecklistId == checklistMs[i].ChecklistId && x.HospitalCode == checkupH.HospitalCode).ToList().Select(y => new RecordSetRefDoctorView
                    {
                        SetRefDoctorId = y.SetRefDoctorId,
                        DoctorName = y.DoctorName,
                        DeleteStatus = y.DeleteStatus,
                        ChecklistId = y.ChecklistId,
                        HospitalCode = y.HospitalCode,
                    }).ToList();
                    response.ChecklistMViews[i].SetRefDoctorViews = doctorMs;
                }
                response.RecordCheckupHView = checkupH;


                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<string> CheckLoginAdmin(string username, string password)
        {
            try
            {
                Dictionary<string, string> data;
                data = new Dictionary<string, string>
                {
                {"grant_type", "password"},
                {"client_id", $"client-sso-mms-hospital"},
                {"client_secret", "1M3AHCwWtqgFSWd1yNuYuoPIFIgrMRjx"},
                {"username", $"{username}"},
                {"password", $"{password}"},
                {"scope", "identification_number OrganizeId UserId"}
                };
                var content = new FormUrlEncodedContent(data);
                var response = await httpClient.PostAsync($"http://192.168.10.61/realms/sso-mms-hospital/protocol/openid-connect/token", content);
                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }


        public async Task<string> UpdateRecord(saverecord data)
        {
            try
            {

                // update checkup H 
                foreach (var item in data.person)
                {
                    var getCheckupH = db.AaiHealthCheckupHs.FirstOrDefault(f => f.CheckupId == item.CheckupId);
                    if (getCheckupH != null)
                    {
                        getCheckupH.PatientWeight = Convert.ToDecimal(item.PatientWeigth);
                        getCheckupH.PatientHeight = Convert.ToDecimal(item.PatientHeigth);
                        getCheckupH.PatientPressure = item.PatientPressure;
                        getCheckupH.CheckupDate = item.CheckupDate;
                        getCheckupH.IsUd = data.isDisease;
                        getCheckupH.UpdateBy = data.usernameupdate;
                        getCheckupH.UpdateDate = DateTime.Now;
                        getCheckupH.ReadDate = DateTime.Now;
                        getCheckupH.IsFromReader = false;
                        getCheckupH.ReadBy = data.usernameupdate;
                        getCheckupH.ConfirmBy = item.UsernameAdmin;
                        db.AaiHealthCheckupHs.Update(getCheckupH);
                    }
                    db.SaveChanges();


                    if (getCheckupH.IsUd == true)
                    {
                        if (data.isDisease)
                        {
                            AaiHealthSetUnderlyingDiseaseM rawdis = new AaiHealthSetUnderlyingDiseaseM();
                            foreach (var item3 in data.diseaseView)
                            {
                                if (item3.value == "โรคเบาหวาน")
                                {
                                    rawdis.IsDb = item3.active;
                                }
                                if (item3.value == "โรคไขมันในเลือดสูง")
                                {
                                    rawdis.IsHpldm = item3.active;
                                }
                                if (item3.value == "โรคอ้วน")
                                {
                                    rawdis.IsObst = item3.active;
                                }
                                if (item3.value == "โรคความดันโลหิตสูง")
                                {
                                    rawdis.IsHpts = item3.active;
                                }
                                if (item3.value == "โรคหัวใจและหลอดเลือด")
                                {
                                    rawdis.IsCvds = item3.active;
                                }
                                if (item3.value == "โรคทางเดินหายใจเรื้อรัง")
                                {
                                    rawdis.IsCopd = item3.active;
                                }
                                if (item3.value == "โรคไตวายเรื้อรัง")
                                {
                                    rawdis.IsCkd = item3.active;
                                }
                                if (item3.value == "โรคมะเร็ง")
                                {
                                    rawdis.IsCc = item3.active;
                                }
                                if (item3.value == "โรคถุงลมโป่งพอง")
                                {
                                    rawdis.IsEps = item3.active;
                                }
                                if (item3.value == "โรคกระเพาะอาหาร")
                                {
                                    rawdis.IsDps = item3.active;
                                }
                                if (item3.value == "โรคโลหิตจาง")
                                {
                                    rawdis.IsTlsm = item3.active;
                                }
                                if (item3.value == "เป็นอื่นๆ")
                                {
                                    rawdis.IsOther = item3.active;
                                    rawdis.Remark = data.remarkdisease;
                                }
                            }
                            rawdis.CreateDate = DateTime.Now;
                            rawdis.UpdateDate = DateTime.Now;
                            rawdis.UpdateBy = data.usernameupdate;
                            rawdis.CreateBy = data.usernameupdate;
                            rawdis.CheckupId = getCheckupH.CheckupId;

                            await db.AaiHealthSetUnderlyingDiseaseMs.AddAsync(rawdis);
                        }
                    }

                    foreach (var item2 in item.checklistMs)
                    {
                        if (item2.statuscheck == false && item2.DefaultStatuscheck == true)
                        {
                            var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupListId == item2.Id);
                            if (checkupListT != null)
                            {
                                db.AaiHealthCheckupListTs.Remove(checkupListT);
                            }
                        }
                    }

                    db.SaveChanges();


                }

                return "success";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> UpdateResult(ManageRecordChecklistView checklist)
        {
            try
            {
                //update reserveH if (reserveId != null)
                if (checklist.RecordCheckupHView.ReserveId != null)
                {
                    var reserve = db.AaiHealthReserveHs.Where(x => x.ReserveId == checklist.RecordCheckupHView.ReserveId).FirstOrDefault();
                    if(reserve != null)
                    {
                        if (reserve.DocStatus != "2" && reserve.DocStatus != "3")
                        {
                            reserve.DocStatus = "2";
                        }
                    }
                    

                }

                if (checklist.ChecklistMViews.Count(x => x.StatusCheck == true) == checklist.ChecklistMViews.Count(y => y.IsCheck == true))
                {
                    var checkup = db.AaiHealthCheckupHs.Where(x => x.CheckupId == checklist.RecordCheckupHView.CheckupId).FirstOrDefault();
                    if (checkup != null)
                    {
                        checkup.UseStatus = "D";
                    }
                }

                // update list t
                foreach (var item in checklist.ChecklistMViews.Where(x => x.IsCheck == true).ToList())
                {
                    var x = db.AaiHealthCheckupListTs.Where(x => x.CheckupListId == item.CheckupListId).FirstOrDefault();
                    x.SetRefDoctorId = item.SelectDoctorId;
                    x.ResultStatus = item.ResultStatus;
                    x.IsChecked = (bool)item.IsCheck;
                    x.UpdateDate = DateTime.Now;
                    x.UpdateBy = checklist.RecordCheckupHView.UpdateBy;
                    db.Update(x);


                    // insert list t
                    foreach (var item2 in item.ChecklistDViews)
                    {
                        var y = db.AaiHealthCheckupResultTs.FirstOrDefault(x => x.CheckupListId == item.CheckupListId && x.ChecklistDtId == item2.ChecklistDtId);
                        if (y == null)
                        {
                            AaiHealthCheckupResultT resultT = new AaiHealthCheckupResultT();
                            resultT.CheckupListId = (decimal)item.CheckupListId;
                            resultT.ChecklistDtId = item2.ChecklistDtId;
                            resultT.ResultCheckValue = item2.CheckValue;
                            resultT.RefValue = item2.RecordSetRefChecklistCfgView == null ? null : item2.RecordSetRefChecklistCfgView.SetRefValue;
                            resultT.IsNormal = item2.IsNormal;
                            resultT.Suggession = item2.Suggestion;
                            resultT.CreateDate = DateTime.Now;
                            resultT.CreateBy = checklist.RecordCheckupHView.UpdateBy;
                            resultT.UpdateDate = DateTime.Now;
                            resultT.UpdateBy = checklist.RecordCheckupHView.UpdateBy;
                            await db.AaiHealthCheckupResultTs.AddAsync(resultT);
                        }
                        else
                        {
                            AaiHealthCheckupResultT resultTupdate = new AaiHealthCheckupResultT();
                            resultTupdate = db.AaiHealthCheckupResultTs.Where(x => x.CheckupResultId == y.CheckupResultId).FirstOrDefault();
                            resultTupdate.ResultCheckValue = item2.CheckValue;
                            resultTupdate.RefValue = item2.RecordSetRefChecklistCfgView == null ? null : item2.RecordSetRefChecklistCfgView.SetRefValue;
                            resultTupdate.IsNormal = item2.IsNormal;
                            resultTupdate.Suggession = item2.Suggestion;
                            resultTupdate.UpdateDate = DateTime.Now;
                            resultTupdate.UpdateBy = checklist.RecordCheckupHView.UpdateBy;
                            db.Update(resultTupdate);
                        }

                    }

                }
                db.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> SaveRcord(saverecord data)
        {
            try
            {
                // insert checkup H 
                foreach (var item in data.person)
                {
                    AaiHealthCheckupH CheckupH = new AaiHealthCheckupH();
                    CheckupH.DeleteStatus = "I";
                    CheckupH.PatientName = item.PatientName;
                    CheckupH.PersonalId = item.PersonalId;
                    CheckupH.Remark = item.Remark;
                    CheckupH.PatientSurname = item.PatientSurname;
                    CheckupH.PatientTel = item.PatientTel;
                    CheckupH.PatientSex = item.PatientSex;
                    CheckupH.PatientWeight = Convert.ToDecimal(item.PatientWeigth);
                    CheckupH.PatientHeight = Convert.ToDecimal(item.PatientHeigth);
                    CheckupH.PatientPressure = item.PatientPressure;
                    CheckupH.UseStatus = "W";
                    CheckupH.IsUd = data.isDisease;
                    CheckupH.HospitalCode = data.hoscode;
                    CheckupH.CreateDate = DateTime.Now;
                    CheckupH.CreateBy = data.usernameupdate;
                    CheckupH.UpdateDate = DateTime.Now;
                    CheckupH.UpdateBy = data.usernameupdate;
                    CheckupH.ReadDate = DateTime.Now;
                    CheckupH.IsFromReader = false;
                    CheckupH.BudgetYear = (DateTime.Now.Year + 543).ToString();
                    CheckupH.MonthBudyear = DateTime.Now.Month.ToString().PadLeft(2, '0');
                    CheckupH.ReadBy = data.usernameupdate;
                    CheckupH.Reason = item.Reson;
                    CheckupH.ConfirmBy = item.UsernameAdmin;
                    CheckupH.CheckupDate = item.CheckupDate;
                    CheckupH.PatientAge = item.PatientAge;
                    //run number
                    var runningNo = db.AaiHealthCheckupHs.Where(x => x.BudgetYear == CheckupH.BudgetYear && x.MonthBudyear == CheckupH.MonthBudyear).OrderBy(y => y.CheckupId).Select(z => z.CheckupNo).LastOrDefault();
                    int number;
                    if (runningNo == null)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = Int32.Parse(runningNo.Substring(6));
                    }

                    var checkup = (DateTime.Now.Year + 543).ToString().Substring(2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString() + (number + 1).ToString().PadLeft(5, '0');
                    CheckupH.CheckupNo = checkup;
                    await db.AaiHealthCheckupHs.AddAsync(CheckupH);
                    db.SaveChanges();
                    decimal CheckupHId = CheckupH.CheckupId;


                    // insert Checklist T
                    if (item.DeleteStatus != "N")
                    {
                        foreach (var item2 in item.checklistMs)
                        {
                            if (item2.statuscheck == true)
                            {
                                AaiHealthCheckupListT CheckListT = new AaiHealthCheckupListT();
                                CheckListT.ChecklistId = item2.ChecklistId;
                                CheckListT.CheckupId = CheckupHId;
                                CheckListT.ResultStatus = "W";
                                CheckListT.IsChecked = false;
                                CheckListT.CreateDate = DateTime.Now;
                                CheckListT.CreateBy = data.usernameupdate;
                                db.AaiHealthCheckupListTs.Add(CheckListT);
                            }

                        }
                        db.SaveChanges();
                    }

                    // insert disease
                    if (data.isDisease)
                    {
                        AaiHealthSetUnderlyingDiseaseM rawdis = new AaiHealthSetUnderlyingDiseaseM();
                        foreach (var item3 in data.diseaseView)
                        {
                            if (item3.value == "โรคเบาหวาน")
                            {
                                rawdis.IsDb = item3.active;
                            }
                            if (item3.value == "โรคไขมันในเลือดสูง")
                            {
                                rawdis.IsHpldm = item3.active;
                            }
                            if (item3.value == "โรคอ้วน")
                            {
                                rawdis.IsObst = item3.active;
                            }
                            if (item3.value == "โรคความดันโลหิตสูง")
                            {
                                rawdis.IsHpts = item3.active;
                            }
                            if (item3.value == "โรคหัวใจและหลอดเลือด")
                            {
                                rawdis.IsCvds = item3.active;
                            }
                            if (item3.value == "โรคทางเดินหายใจเรื้อรัง")
                            {
                                rawdis.IsCopd = item3.active;
                            }
                            if (item3.value == "โรคไตวายเรื้อรัง")
                            {
                                rawdis.IsCkd = item3.active;
                            }
                            if (item3.value == "โรคมะเร็ง")
                            {
                                rawdis.IsCc = item3.active;
                            }
                            if (item3.value == "โรคถุงลมโป่งพอง")
                            {
                                rawdis.IsEps = item3.active;
                            }
                            if (item3.value == "โรคกระเพาะอาหาร")
                            {
                                rawdis.IsDps = item3.active;
                            }
                            if (item3.value == "โรคโลหิตจาง")
                            {
                                rawdis.IsTlsm = item3.active;
                            }
                            if (item3.value == "เป็นอื่นๆ")
                            {
                                rawdis.IsOther = item3.active;
                                rawdis.Remark = data.remarkdisease;
                            }
                        }
                        rawdis.CreateDate = DateTime.Now;
                        rawdis.UpdateDate = DateTime.Now;
                        rawdis.UpdateBy = data.usernameupdate;
                        rawdis.CreateBy = data.usernameupdate;
                        rawdis.CheckupId = CheckupHId;

                        await db.AaiHealthSetUnderlyingDiseaseMs.AddAsync(rawdis);
                    }

                    db.SaveChanges();


                }

                return "success";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}

