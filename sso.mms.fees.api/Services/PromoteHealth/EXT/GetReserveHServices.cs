using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using OneOf.Types;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class GetReserveHServices : IGetReserveHExtServices
    {
        private readonly PromoteHealthContext db;

        public GetReserveHServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }

        public async Task<ManageBookHealthCheckupView> GetReserveHById(int reserveId)
        {
            try
            {
                ManageBookHealthCheckupView response = new ManageBookHealthCheckupView();
                var result = db.AaiHealthReserveHs.Where(x => x.ReserveId == reserveId).Select(y => new ReserveH
                {
                    ReserveId = y.ReserveId,
                    HospitalCode = y.HospitalCode,
                    CompanyName = y.CompanyName,
                    CompanyBranch = y.CompanyBranch,
                    CompanyTaxNo = y.CompanyTaxNo,
                    CompanyAccNo = y.CompanyAccNo,
                    CompanyAddr = y.CompanyAddr,
                    HrName = y.HrName,
                    HrEmail = y.HrEmail,
                    HrPhone = y.HrPhone,
                    ReserveStartDate = y.ReserveStartDate,
                    ReserveEndDate = y.ReserveEndDate,
                    DocStatus = y.DocStatus,
                    CreateBy = y.CreateBy,
                    UpdateBy = y.UpdateBy,
                    CreateDate = y.CreateDate,
                    UpdateDate = y.UpdateDate

                }).FirstOrDefault();
                

                var res = db.AaiHealthCheckupHs.Where(x => x.ReserveId == reserveId).ToList().Select(y => new CheckPermissionCheckListView
                {
                    CheckupId = y.CheckupId,
                    DeleteStatus = y.DeleteStatus,
                    PatientName = y.PatientName,
                    PersonalId = y.PersonalId,
                    Remark = y.Remark,
                    PatientSurname = y.PatientSurname,
                    PatientSex = y.PatientSex,
                    PatientTel = y.PatientTel,
                    ReadDate = y.ReadDate
                }).ToList();
                foreach (var data in res)
                {
                    var checklistM = db.AaiHealthChecklistMs.Where(x => x.ChecklistStatus == "A").ToList().Select(y => new ChecklistM
                    {
                        No  = y.ChecklistCode == "ChestX-ray" ? 14 : y.ChecklistCode == "FOBT" ? 13 : y.ChecklistCode == "Via" ? 12 : y.ChecklistCode == "PAPSmear" ? 11 : y.ChecklistCode == "HBaAg" ? 10 : y.ChecklistCode == "LP" ? 9 : y.ChecklistCode == "CR" ? 8 : y.ChecklistCode == "FBS" ? 7 : y.ChecklistCode == "UA" ? 6 : y.ChecklistCode == "CBC" ? 5 : y.ChecklistCode == "Snelleneye" ? 3 : y.ChecklistCode == "Eye1" ? 4 : y.ChecklistCode == "Breast" ? 2 : y.ChecklistCode == "Hearing" ? 1 : 0,
                        ChecklistId = y.ChecklistId,
                        ChecklistName = y.ChecklistName,
                        ChecklistPrice = y.ChecklistPrice,
                        ChecklistCode = y.ChecklistCode,
                        ChecklistShortname = y.ChecklistShortname,
                        statuscheck = false

                    }).ToList();
                    var checklistT = db.ViewAaiHealthCheckupListTs.Where(x => x.CheckupId == data.CheckupId && x.ReserveId == reserveId && x.DeleteStatus == "I").ToList();
                    for (var i = 0; i < checklistT.Count(); i++)
                    {
                        for (var j = 0; j < checklistM.Count(); j++)
                        {
                            if (checklistM[j].ChecklistCode == checklistT[i].ChecklistCode)
                            {
                                checklistM[j].statuscheck = true;
                            }
                        }
                    }
                    data.checklistMs = checklistM.OrderBy(x => x.No).ToList();
                }
               

                response.dataCompany = result;
                response.person = res;

                foreach (var person in response.person)
                {
                    person.ChecklistPriceAll = person.checklistMs.Where(x => x.statuscheck == true).Sum(x => x.ChecklistPrice);
                    person.Allcount = person.checklistMs.Count(x => x.statuscheck == true);
                }
                


                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<GetReServeHView>> GetAll(string hoscode)
        {
            try
            {
                var res = db.AaiHealthReserveHs.Where(x =>x.HospitalCode == hoscode).OrderByDescending(y => y.ReserveId).ToList().Select(x => new GetReServeHView
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
                    PersonCount = db.AaiHealthCheckupHs.Where(y => y.ReserveId == x.ReserveId).Count(),

                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<List<GetReServeHView>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
