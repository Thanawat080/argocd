using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using OracleInternal.Sharding;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.ViewModels.Dental;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.CodeDom;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sso.mms.fees.api.Services.Dental.EXT
{
    public class TreatmentRecordServices : ITreatmentRecord
    {
        private readonly DentalContext db;
        public TreatmentRecordServices(DentalContext context)
        {
            db=context;
        }
        public async Task<List<AaiDentalCheckHView>> TreatmentRecordList()
        {
         
                return await db.AaiDentalCheckHs
                .Where(x => x.CheckStatus == "P" || x.CheckStatus == "D" || x.CheckStatus == "E")
                .Select(x =>
                new AaiDentalCheckHView
                {
                    CheckHId = x.CheckHId,
                    HospitalId = x.HospitalId,
                    SsoOrgId = x.SsoOrgId,
                   PatientName = x.PatientName,
                   PersonalId = x.PersonalId,
                   BirthDate = x.BirthDate,
                    BalanceMoney = x.BalanceMoney,
                   CreateDate = x.CreateDate, 
                   CheckStatus = x.CheckStatus
                }).ToListAsync();
           
        }
        public async Task<string> TreatmentCreate(AaiDentalCheckHView data)
        {
            try
            {
              
                AaiDentalCheckH Dental = new AaiDentalCheckH();
                    Dental.CheckHId = data.CheckHId;
                    Dental.PersonalId = data.PersonalId;
                    Dental.Reason = data.Reason;
                    Dental.HospitalId = data.HospitalId;
                Dental.PatientName = data.PatientName;
                    Dental.CheckDate = DateTime.Now;
                    Dental.PhoneNo = data.PhoneNo;
                    Dental.PortalPort = data.PortalPort;
                    Dental.UpdateDate = DateTime.Now;
                    Dental.UpdateBy = "system";
                Dental.CheckStatus = "P";
                Dental.CreateDate = DateTime.Now;
                Dental.CreateBy = "system";
                await db.AaiDentalCheckHs.AddAsync(Dental);
                
                db.SaveChanges();
                return "succes";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<AaiDentalCheckHView> TreatmentRecordById(int id)
        {

            return await db.AaiDentalCheckHs
                .Include(b => b.AaiDentalCheckDs)
                .Where(x => x.CheckHId == id)
                .Select(a => new AaiDentalCheckHView
                {
                    CheckHId = a.CheckHId,
                    HospitalId = a.HospitalId,
                    SsoOrgId = a.SsoOrgId,
                    PersonalId = a.PersonalId,
                    PatientName = a.PatientName,
                    Sex = a.Sex,
                    BirthDate = a.BirthDate,
                    National = a.National,
                    SsoStatus = a.SsoStatus,
                    CheckDate = a.CheckDate,
                    PhoneNo = a.PhoneNo,
                    BalanceMoney = a.BalanceMoney,
                    PortalPort = a.PortalPort,
                    DentalCarDId = a.DentalCarDId,
                    IsFromReader = a.IsFromReader,
                    Reason = a.Reason,
                    ConfirmBy = a.ConfirmBy,
                    CheckStatus = a.CheckStatus,
                    CheckDocu = a.CheckDocu,
                    CreateDate = a.CreateDate,
                    CreateBy = a.CreateBy,
                    UpdateDate = a.UpdateDate,
                    UpdateBy = a.UpdateBy,
                    AaiDentalCheckDs = a.AaiDentalCheckDs.ToList()

                })
                .FirstOrDefaultAsync();



        }

        public async Task<apiview850> api850(string? personid)
        {
            try
            {
                // Initialize our list
                List<bool> mylist = new List<bool>(new bool[] { true, false });

                Random R = new Random();

                // get random number from 0 to 2. 
                int someRandomNumber = R.Next(0, mylist.Count());

                apiview850 res = new apiview850();
                res.age = 50;
                res.Sex = "F";
                res.Name = "Test";
                res.Sname = "Test";
                res.status = mylist.ElementAt(someRandomNumber);
                res.BirthDate = DateTime.Now;
                res.statusPerson = "a";
                res.price = 900;
                res.personalId = personid;
                res.Phone = "0999999999";
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<AaiDentalToothTypeM>> ToothList()
        {
            return await db.AaiDentalToothTypeMs.ToListAsync();
        }

        public async Task<string> DentalCheckupCreate(AaiDentalCheckDView data)
        {
            try
            {
                AaiDentalCheckD DentalCheck = new AaiDentalCheckD();
              
                DentalCheck.CheckHId = data.CheckHId;
                DentalCheck.ToothTypeId = data.ToothTypeId;
                DentalCheck.DentListId = data.DentListId;
                DentalCheck.Icd9Id = data.Icd9Id;
                DentalCheck.DoctorName = data.DoctorName;
                DentalCheck.CreateBy = data.CreateBy;
                DentalCheck.Decision = data.Decision;
                DentalCheck.Icd10Id = data.Icd10Id;
                DentalCheck.CheckDate = DateTime.Today;
                DentalCheck.CreateDate = data.CreateDate;
                DentalCheck.UpdateDate = DateTime.Today;
                DentalCheck.UpdateBy = data.UpdateBy;

                await db.AaiDentalCheckDs.AddAsync(DentalCheck);
                db.SaveChanges();
                return "suscess";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<AaiDentalCheckDView>> CheckDList(int id)
        {
            try
            {
                return await db.AaiDentalCheckDs.Where(x => x.CheckHId == id).Select(x => new AaiDentalCheckDView
                {
                    CheckDId = x.CheckDId,
                    CheckHId = x.CheckHId,
                    DentListId = x.DentListId,
                    ToothTypeId =x.ToothTypeId,
                    Icd10Id = x.Icd10Id,
                    Icd9Id = x.Icd9Id,
                    Decision =x.Decision,
                    DoctorName = x.DoctorName ,
                    CheckDate = x.CheckDate,
                    Expense =x.Expense,
                    SsoPay =x.SsoPay,

                }).ToListAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
