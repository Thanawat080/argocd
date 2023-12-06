using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Entities.PromoteHealth;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class ManageBookHealthCheckupServices : IManageBookHealthCheckupExtServices
    {
        private readonly PromoteHealthContext db;

        public ManageBookHealthCheckupServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }
        public async Task<string> CreateBookCheckup(ManageBookHealthCheckupView data)
        {
            try
            {
                // insert reserveH
                AaiHealthReserveH ReserveH = new AaiHealthReserveH();
                ReserveH.HospitalCode = data.dataCompany.HospitalCode;
                ReserveH.CompanyName = data.dataCompany.CompanyName;
                ReserveH.CompanyBranch = data.dataCompany.CompanyBranch;
                ReserveH.CompanyTaxNo = data.dataCompany.CompanyTaxNo;
                ReserveH.CompanyAccNo = data.dataCompany.CompanyAccNo;
                ReserveH.CompanyAddr = data.dataCompany.CompanyAddr;
                ReserveH.HrName = data.dataCompany.HrName;
                ReserveH.HrEmail = data.dataCompany.HrEmail;
                ReserveH.HrPhone = data.dataCompany.HrPhone;
                ReserveH.ReserveStartDate = (DateTime)data.dataCompany.ReserveStartDate?.AddYears(-543);
                ReserveH.ReserveEndDate = (DateTime)data.dataCompany.ReserveEndDate?.AddYears(-543);
                ReserveH.DeleteStatus = "A";
                ReserveH.DocStatus = "1";
                ReserveH.CreateBy = data.dataCompany.CreateBy;
                ReserveH.UpdateBy = data.dataCompany.UpdateBy;
                ReserveH.CreateDate = DateTime.Now;
                ReserveH.UpdateDate = DateTime.Now;
                await db.AaiHealthReserveHs.AddAsync(ReserveH);
                db.SaveChanges();
                decimal ReserveHId = ReserveH.ReserveId;

                // insert checkup H 
                foreach (var item in data.person)
                {
                    AaiHealthCheckupH CheckupH = new AaiHealthCheckupH();
                    CheckupH.DeleteStatus = item.DeleteStatus;
                    CheckupH.PatientName = item.PatientName;
                    CheckupH.PersonalId = item.PersonalId;
                    CheckupH.Remark = item.Remark;
                    CheckupH.PatientSurname = item.PatientSurname;
                    CheckupH.PatientTel = item.PatientTel;
                    CheckupH.PatientSex = item.PatientSex;
                    CheckupH.UseStatus = "R";
                    CheckupH.IsUd = false;
                    CheckupH.HospitalCode = data.dataCompany.HospitalCode;
                    CheckupH.ReserveId = ReserveHId;
                    CheckupH.CreateDate = DateTime.Now;
                    CheckupH.CreateBy = data.dataCompany.CreateBy;
                    CheckupH.UpdateDate = DateTime.Now;
                    CheckupH.UpdateBy = data.dataCompany.UpdateBy;
                    CheckupH.IsFromReader = false;
                    CheckupH.BudgetYear = (DateTime.Now.Year + 543).ToString();
                    CheckupH.MonthBudyear = DateTime.Now.Month.ToString().PadLeft(2, '0');
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

                    var checkup = (DateTime.Now.Year + 543).ToString().Substring(2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + (number + 1).ToString().PadLeft(5, '0');
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
                                CheckListT.CreateBy = data.dataCompany.CreateBy;
                                await db.AaiHealthCheckupListTs.AddAsync(CheckListT);
                            }

                        }
                        db.SaveChanges();
                    }


                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> UpdateBookCheckup(ManageBookHealthCheckupView data)
        {
            try
            {
                //get reserve by id 
                var getReserveH = db.AaiHealthReserveHs.FirstOrDefault(f => f.ReserveId == data.dataCompany.ReserveId);
                //update data reserve
                if (getReserveH != null)
                {
                    getReserveH.HospitalCode = data.dataCompany.HospitalCode;
                    getReserveH.CompanyName = data.dataCompany.CompanyName;
                    getReserveH.CompanyBranch = data.dataCompany.CompanyBranch;
                    getReserveH.CompanyTaxNo = data.dataCompany.CompanyTaxNo;
                    getReserveH.CompanyAccNo = data.dataCompany.CompanyAccNo;
                    getReserveH.CompanyAddr = data.dataCompany.CompanyAddr;
                    getReserveH.HrName = data.dataCompany.HrName;
                    getReserveH.HrEmail = data.dataCompany.HrEmail;
                    getReserveH.HrPhone = data.dataCompany.HrPhone;
                    getReserveH.ReserveStartDate = (DateTime)data.dataCompany.ReserveStartDate;
                    getReserveH.ReserveEndDate = (DateTime)data.dataCompany.ReserveEndDate;
                    getReserveH.DeleteStatus = "A";
                    getReserveH.DocStatus = "1";
                    //getReserveH.CreateBy = data.dataCompany.CreateBy;
                    getReserveH.UpdateBy = data.dataCompany.UpdateBy;
                    //getReserveH.CreateDate = DateTime.Now;
                    getReserveH.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                }
                //find chack h where reserveh id
                var checkListUpH = db.AaiHealthCheckupHs.Where(w => w.ReserveId == data.dataCompany.ReserveId).ToList();
                if (checkListUpH != null)
                {
                    foreach (var checkh in checkListUpH)
                    {
                        //find chack list t where chack h id
                        var checkListUpT = db.AaiHealthCheckupListTs.Where(w => w.CheckupId == checkh.CheckupId).ToList();
                        if (checkListUpT != null)
                        {
                            foreach (var checkT in checkListUpT)
                            {
                                //remove check list t
                                db.AaiHealthCheckupListTs.Remove(checkT);
                                db.SaveChanges();
                            }
                        }
                        //remove check h
                        db.AaiHealthCheckupHs.Remove(checkh);
                        db.SaveChanges();
                    }
                }

                // insert checkup H 
                foreach (var item in data.person)
                {
                    AaiHealthCheckupH CheckupH = new AaiHealthCheckupH();
                    CheckupH.DeleteStatus = item.DeleteStatus;
                    CheckupH.PatientName = item.PatientName;
                    CheckupH.PersonalId = item.PersonalId;
                    CheckupH.Remark = item.Remark;
                    CheckupH.PatientSurname = item.PatientSurname;
                    CheckupH.PatientTel = item.PatientTel;
                    CheckupH.PatientSex = item.PatientSex;
                    CheckupH.UseStatus = "R";
                    CheckupH.IsUd = false;
                    CheckupH.HospitalCode = data.dataCompany.HospitalCode;
                    CheckupH.ReserveId = data.dataCompany.ReserveId;
                    CheckupH.CreateDate = DateTime.Now;
                    CheckupH.CreateBy = data.dataCompany.CreateBy;
                    CheckupH.UpdateDate = DateTime.Now;
                    CheckupH.UpdateBy = data.dataCompany.UpdateBy;
                    CheckupH.IsFromReader = false;
                    CheckupH.BudgetYear = DateTime.Now.Year.ToString();
                    CheckupH.MonthBudyear = DateTime.Now.Month.ToString();

                    //run number
                    var runningNo = db.AaiHealthCheckupHs.Where(x => x.BudgetYear == CheckupH.BudgetYear && x.MonthBudyear == CheckupH.MonthBudyear && x.HospitalCode == data.dataCompany.HospitalCode).OrderBy(y => y.CheckupId).Select(z => z.CheckupNo).LastOrDefault();
                    int number;
                    if (runningNo == null)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = Int32.Parse(runningNo.Substring(6));
                    }

                    var checkup = (DateTime.Now.Year + 543).ToString().Substring(2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + (number + 1).ToString().PadLeft(4, '0');
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
                                CheckListT.CreateBy = data.dataCompany.CreateBy;
                                await db.AaiHealthCheckupListTs.AddAsync(CheckListT);
                            }

                        }
                        db.SaveChanges();
                    }
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

