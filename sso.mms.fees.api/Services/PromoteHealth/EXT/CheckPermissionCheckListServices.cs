using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.Services.PromoteHealth.Base;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class CheckPermissionCheckListServices : ICheckPermissionCheckListExtServices
    {
        private readonly PromoteHealthContext db;

        public CheckPermissionCheckListServices()
        {
        }
        public CheckPermissionCheckListServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }


        public async Task<apiview850> CheckPersonFrom850Api(string identificationnumber)
        {
            try 
            {
                apiview850 res850 = new apiview850();
                var AlreadyCheckListServices = new AlreadyCheckListServices(db);
                res850 = await AlreadyCheckListServices.api850(identificationnumber);
                return res850;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<List<CheckPermissionCheckListView>> CheckPerson(List<person> data)
        {
            try
            {
                apiview850 res850 = new apiview850();
                var AlreadyCheckListServices = new AlreadyCheckListServices(db);
                List<CheckPermissionCheckListView> response = new List<CheckPermissionCheckListView>();
                response = data.Select(x => new CheckPermissionCheckListView
                {
                    PatientName = x.PatientName,
                    PersonalId = x.PersonalId,
                    PatientSurname = x.PatientSurname,
                    PatientSex = x.PatientSex,
                    PatientTel = x.PatientTel,
                    Reson = x.Reson,
                    UsernameAdmin = x.UsernameAdmin,
                    check850 = x.check850,
                    CheckupId = x.CheckupId == null ? 0 : x.CheckupId,
                    typeReserve = x.typeReserve,
                    CheckChecklistM = x.CheckChecklistM,
                    default850 = x.default850
                }).ToList();

                foreach (var person in response)
                {
                    decimal checkupid = 0;
                    if (person.CheckupId == null) 
                    {
                        person.CheckupId = 0;
                    }
                    checkupid = person.CheckupId;

                    if (person.check850 == true)
                    {
                        // wait 850 api
                        res850 = await CheckPersonFrom850Api(person.PersonalId);
                        if (person.typeReserve != "Reserve")
                        {
                            person.PatientName = res850.Name;
                            person.PatientSurname = res850.Sname;
                            person.PatientSex = res850.Sex;
                            person.PatientAge = res850.age;
                        }
                        else
                        {
                            person.PatientAge = res850.age;
                        }

                        // check name sname 
                        //else {
                        //    if (res850.Name != person.PatientName || person.PatientSurname != res850.Sname || person.PatientSex != res850.Sex) 
                        //    {
                        //        res850.status = false;

                        //    }
                        //}
                    }
                    else
                    {
                        res850.status = true;
                    }
                    if (res850.status || person.default850)
                    {
                        person.DeleteStatus = "I";
                        List<AaiHealthChecklistM> reschecklistM = new List<AaiHealthChecklistM>();
                        reschecklistM = db.AaiHealthChecklistMs.Where(x => x.ChecklistStatus != "I").ToList();
                        if (person.CheckChecklistM == true)
                        {
                            foreach (var checklistm in reschecklistM)
                            {
                                bool res = false;
                                if (checklistm.ChecklistCode == "Hearing")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "Hearing");
                                        if (res850.age >= 15 && res == false)
                                        {
                                            res = true;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }

                                    checklistM.No = 1;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "Breast")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 30 && res850.age <= 39)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList2(person.PersonalId, DateTime.Now.Year.ToString(), "Breast", 3);
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else if (res850.age >= 40 && res850.age <= 54)
                                        {
                                            res = true;
                                        }
                                        else if (res850.age >= 55)
                                        {
                                            res = true;
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 2;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "Eye1")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 40 && res850.age <= 54)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, null, "Eye1");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else if (res850.age >= 55)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "Eye1");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 4;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "Snelleneye")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 55)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "Snelleneye");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                    }
                                    else
                                    {

                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    checklistM.No = 3;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);

                                }
                                else if (checklistm.ChecklistCode == "CBC")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 18 && res850.age <= 54)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, null, "CBC");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else if (res850.age >= 55 && res850.age <= 70)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "CBC");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    else
                                    {

                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }

                                    checklistM.No = 5;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "UA")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 55)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "UA");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    checklistM.No = 6;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "FBS")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 35 && res850.age <= 54)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList2(person.PersonalId, DateTime.Now.Year.ToString(), "FBS", 3);
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else if (res850.age >= 55)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "FBS");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 7;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "CR")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 55)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "CR");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }


                                    }
                                    checklistM.No = 8;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "LP")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        res = await AlreadyCheckListServices.Already_CheckList2(person.PersonalId, DateTime.Now.Year.ToString(), "LP", 5);
                                        if ((res850.age >= 20) && res == false)
                                        {
                                            res = true;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }

                                    checklistM.No = 9;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "HBaAg")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, null, "HBaAg");
                                        if ((((DateTime.Now.Year + 543) - res850.age) <= 2534) && res == false)
                                        {
                                            res = true;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 10;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "PAPSmear")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 30 && res850.age <= 54)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList2(person.PersonalId, DateTime.Now.Year.ToString(), "PAPSmear", 3);
                                            if (person.PatientSex == "F" && res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else if (res850.age >= 55)
                                        {
                                            if(person.PatientSex == "F")
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                            
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 11;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "Via")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 30 && res850.age <= 54)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList2(person.PersonalId, DateTime.Now.Year.ToString(), "Via", 5);
                                            if (person.PatientSex == "F" && res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else if (res850.age >= 55)
                                        {
                                            if (person.PatientSex == "F")
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 12;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "FOBT")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        if (res850.age >= 50)
                                        {
                                            res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, (DateTime.Now.Year + 543).ToString(), "FOBT");
                                            if (res == false)
                                            {
                                                res = true;
                                            }
                                            else
                                            {
                                                res = false;
                                            }
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }

                                    }
                                    checklistM.No = 13;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else if (checklistm.ChecklistCode == "ChestX-ray")
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    if (person.check850 == true)
                                    {
                                        res = await AlreadyCheckListServices.Already_CheckList(person.PersonalId, null, "ChestX-ray");
                                        if ((res850.age >= 15) && res == false)
                                        {
                                            res = true;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    else
                                    {
                                        var checkupListT = db.AaiHealthCheckupListTs.FirstOrDefault(x => x.CheckupId == checkupid && x.ChecklistId == checklistm.ChecklistId);
                                        if (checkupListT != null)
                                        {
                                            res = true;
                                            checklistM.Id = checkupListT.CheckupListId;
                                        }
                                        else
                                        {
                                            res = false;
                                        }
                                    }
                                    checklistM.No = 14;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }
                                else
                                {
                                    ChecklistM checklistM = new ChecklistM();
                                    res = true;
                                    var lastNo = person.checklistMs.Count();
                                    checklistM.No = lastNo + 1;
                                    checklistM.ChecklistCode = checklistm.ChecklistCode;
                                    checklistM.ChecklistPrice = checklistm.ChecklistPrice;
                                    checklistM.ChecklistId = checklistm.ChecklistId;
                                    checklistM.ChecklistName = checklistm.ChecklistName;
                                    checklistM.ChecklistShortname = checklistm.ChecklistShortname;
                                    checklistM.statuscheck = res;
                                    checklistM.DefaultStatuscheck = res;
                                    person.checklistMs.Add(checklistM);
                                }

                            }
                            person.ChecklistPriceAll = person.checklistMs.Where(x => x.statuscheck == true).Sum(x => x.ChecklistPrice);
                            person.Allcount = person.checklistMs.Count(x => x.statuscheck == true);
                            person.checklistMs = person.checklistMs.OrderBy(x => x.No).ToList();
                        }
                    }
                    else
                    {
                        person.DeleteStatus = "N";
                        person.Remark = "ยังส่งเงินสมทบไม่ครบตามกำหนด";
                    }
                }

                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
