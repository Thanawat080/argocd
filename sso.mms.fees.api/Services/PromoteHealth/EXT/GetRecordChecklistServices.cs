using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using sso.mms.helper.Data;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class GetRecordChecklistServices : IGetRecordChecklistExtServices
    {
        private readonly PromoteHealthContext db;
        private readonly IdpDbContext Idpdb;
        public GetRecordChecklistServices(PromoteHealthContext DbContext, IdpDbContext IDbContext)
        {
            db = DbContext;
            Idpdb = IDbContext;
        }

        public async Task<List<GetReServeHView>> GetAllRecordChecklist(string hoscode)
        {
            try
            {
                var res = db.AaiHealthReserveHs.Where(x => x.HospitalCode == hoscode).ToList().Select(x => new GetReServeHView
                {
                    ReserveId = x.ReserveId,
                    HospitalCode = x.HospitalCode,
                    CompanyName = x.CompanyName,
                    CompanyBranch = x.CompanyBranch,
                    CompanyTaxNo = x.CompanyTaxNo,
                    CompanyAccNo = x.CompanyAccNo,
                    CompanyAddr = x.CompanyAddr,
                    HrName = x.HrName,
                    HrEmail = x.HrEmail,
                    HrPhone = x.HrPhone,
                    ReserveStartDate = x.ReserveStartDate,
                    ReserveEndDate = x.ReserveEndDate,
                    DeleteStatus = x.DeleteStatus,
                    DocStatus = x.DocStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    CancleDate = x.CancleDate,
                    CancleBy = x.CancleBy,
                    DeleteDate = x.DeleteDate,
                    DeleteBy = x.DeleteBy,
                    PersonCount = db.AaiHealthReserveHs.Where(y => y.ReserveId == x.ReserveId && y.DocStatus == "3").Count() > 0 ? db.AaiHealthCheckupHs.Where(y => y.ReserveId == x.ReserveId && y.ReadDate != null).Count() : 0,
                    PersonCountPrivileges = db.AaiHealthCheckupHs.Where(y => y.ReserveId == x.ReserveId && y.DeleteStatus != "N").Count(),
                    PriceCountPrivileges = db.AaiHealthReserveHs.Where(y => y.ReserveId == x.ReserveId && y.DocStatus == "3").Count() > 0 ? db.ViewAaiHealthCheckupListTs.Where(y => y.ReserveId == x.ReserveId && y.DeleteStatus != "N" && y.ReadDate != null).ToList().Sum(y => y.ChecklistPrice) : 0,
                    DiffDate = Math.Round(Math.Abs(x.ReserveStartDate.Subtract(x.ReserveEndDate).TotalDays))

                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GetRecordChecklistView>> GetAllRecordChecklistByUser(string hoscode)
        {
            try 
            {
                var res = db.AaiHealthCheckupHs.Where(x => x.DeleteStatus == "I" && x.ReserveId == null && x.HospitalCode == hoscode).OrderBy(x => x.CheckupDate).Select(x => new GetRecordChecklistView
                {
                    CheckupId = x.CheckupId,
                    CheckupNo = x.CheckupNo,
                    ReserveId = x.ReserveId,
                    HospitalCode = x.HospitalCode,
                    PersonalId = x.PersonalId,
                    PatientName = x.PatientName,
                    PatientSurname = x.PatientSurname,
                    UseStatus = x.UseStatus,
                    CheckupDate = x.CheckupDate,
                    CountIsCheck = (from cup in db.AaiHealthCheckupHs
                                   join clt in db.AaiHealthCheckupListTs on cup.CheckupId equals clt.CheckupId
                                   join crt in db.AaiHealthCheckupResultTs on clt.CheckupListId equals crt.CheckupListId
                                   join clm in db.AaiHealthChecklistMs on clt.ChecklistId equals clm.ChecklistId
                                   join cld in db.AaiHealthChecklistDs on crt.ChecklistDtId equals cld.ChecklistDtId
                                   where clt.IsChecked == true
                                      && cld.IsOption == false
                                      && clt.CheckupId == x.CheckupId
                                      && ((clm.IsSetRef == false && crt.IsNormal != null) || (clm.IsSetRef == true && crt.ResultCheckValue != null && crt.IsNormal != null))
                                   select clt.CheckupListId).Distinct().Count(),
                    CountPrivileges = x.AaiHealthCheckupListTs.Where(y => y.CheckupId == x.CheckupId).Count()
                }).ToList();
                return res;
            }catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<AdminHos>> GetAllRoleAdminByHosCode(string hoscode)
        {
            try
            {
                var usernames = Idpdb.HospitalUserMs.Where(x => x.MedicalCode == hoscode).Select(x => x.UserName).ToList();
                var res1 = await Idpdb.RoleUserMappings
                    .Where(x => x.RoleGroupId == 76 && usernames.Contains(x.UserName))
                    .Select(x => new AdminHos
                    {
                        username = x.UserName
                    })
                    .ToListAsync();
                return res1;
            }catch (Exception ex) {
                return null;
            }
        }
    }
}
