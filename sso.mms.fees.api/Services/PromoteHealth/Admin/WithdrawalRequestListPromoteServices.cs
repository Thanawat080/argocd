using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Entities.PromoteHealth;
using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Text.RegularExpressions;

namespace sso.mms.fees.api.Services.PromoteHealth.Admin
{
    public class WithdrawalRequestListPromoteServices : IWithdrawalRequestListPromoteAdminServices
    {
        private readonly PromoteHealthContext db;

        public WithdrawalRequestListPromoteServices(PromoteHealthContext DbContext)
        {
            this.db = DbContext;
        }
        

         public async Task<HospitalAi> GetAllHosForAI(string withdrawNo)
        {
            HospitalAi res = new HospitalAi();
            res.AllHospital = db.AaiHealthWithdrawalHs.Where(x => x.WithdrawalNo == withdrawNo).GroupBy(x => x.HospitalCode).Count();
            res.Allperson = db.AaiHealthWithdrawalHs.Where(x => x.WithdrawalNo == withdrawNo).Count();
            var query = from wh in db.AaiHealthWithdrawalHs
                        join hh in db.HosHospitals on wh.HospitalCode equals hh.HospCode9 into hospitalGroup
                        from hh in hospitalGroup.DefaultIfEmpty()
                        where wh.WithdrawalNo == withdrawNo
                        group new { wh.HospitalCode, hh.HospName } by new { wh.HospitalCode, hh.HospName } into grouped
                        select new HospitalAiList
                        {
                            Hoscode = grouped.Key.HospitalCode,
                            Hosname = grouped.Key.HospName,
                            AllChecklist = (from wh in db.AaiHealthWithdrawalHs
                                            join lt in db.AaiHealthCheckupListTs on wh.CheckupId equals lt.CheckupId
                                            join wt in db.AaiHealthWithdrawalTs on lt.CheckupListId equals wt.CheckupListId
                                            where wh.HospitalCode == grouped.Key.HospitalCode && wh.WithdrawalNo == withdrawNo
                                            select wt.CheckupListId).Count(),
                            Allprice = (
                                        from wh in db.AaiHealthWithdrawalHs
                                        join lt in db.ViewAaiHealthCheckupListTs on wh.CheckupId equals lt.CheckupId
                                        join wt in db.AaiHealthWithdrawalTs on lt.CheckupListId equals wt.CheckupListId
                                        join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                                        where wh.HospitalCode == grouped.Key.HospitalCode && wh.WithdrawalNo == withdrawNo
                                        group cm.ChecklistPrice by new { wt.CheckupListId, cm.ChecklistPrice } into grouped1
                                        where grouped1.Count() > 0
                                        select grouped1.Sum()
                                    ).FirstOrDefault()

                        };
            res.hospitalAiList = query.ToList();
            return res;

        }
        public async Task<List<AaiHealthWithdrawalHViewForAi>> GetAllForAI()
        {
            var result = from withdrawal in db.AaiHealthWithdrawalHs
                         group withdrawal by new
                         {
                             withdrawal.WithdrawalNo,
                             CREATE_DATE = ((DateTime)withdrawal.CreateDate).Date,
                             UPDATE_DATE = ((DateTime)withdrawal.UpdateDate).Date,
                             //withdrawal.SuggestionStatus
                         } into groupedWithdrawals
                         select new AaiHealthWithdrawalHViewForAi
                         {
                             WithdrawalNo = groupedWithdrawals.Key.WithdrawalNo,
                             CreateDate = groupedWithdrawals.Key.CREATE_DATE,
                             UpdateDate = groupedWithdrawals.Key.UPDATE_DATE,
                             SuggestionStatus = groupedWithdrawals.Count(h2 => h2.WithdrawalNo == groupedWithdrawals.Key.WithdrawalNo && h2.SuggestionStatus != null)
                         };
            return result.ToList();
        }

        public async Task<AaiHealthCheckupH> GetByCheckId(int CheckupId)
        {

            var result = await db.AaiHealthCheckupHs
    .Include(x => x.AaiHealthCheckupListTs)
        .ThenInclude(y => y.Checklist)
    .Include(a => a.AaiHealthCheckupListTs)
        .ThenInclude(y => y.AaiHealthCheckupResultTs)
            .ThenInclude(z => z.ChecklistDt)
    .Include(a => a.AaiHealthCheckupListTs)
        .ThenInclude(x => x.SetRefDoctor)
    .Where(x => x.CheckupId == CheckupId)
    .Select(x => new AaiHealthCheckupH
    {
        CheckupId = x.CheckupId,
        CreateDate = x.CreateDate,
        UpdateDate = x.UpdateDate,
        UpdateBy = x.UpdateBy,
        PersonalId = x.PersonalId,
        PatientName = x.PatientName,
        CheckupDate = x.CheckupDate,
        PatientWeight = x.PatientWeight,
        PatientHeight = x.PatientHeight,
        PatientPressure = x.PatientPressure,
        IsUd = x.IsUd,
        AaiHealthCheckupListTs = x.AaiHealthCheckupListTs.Select(y => new AaiHealthCheckupListT
        {
            CheckupListId = y.CheckupListId,
            CheckupId=y.CheckupId,
            ChecklistId = y.ChecklistId,
            Checklist = y.Checklist,
            SetRefDoctor = y.SetRefDoctor,
            AaiHealthCheckupResultTs = y.AaiHealthCheckupResultTs.Select(z=> new AaiHealthCheckupResultT
            {
                CheckupResultId= z.CheckupResultId,
                CheckupListId = z.CheckupListId,
                ChecklistDtId = z.ChecklistDtId,
                ResultCheckValue =z.ResultCheckValue,
                RefValue = z.RefValue,
                IsNormal = z.IsNormal,
                Suggession = z.Suggession,
                ChecklistDt =z.ChecklistDt
            }).ToList()



            // Add other properties as needed
        }).ToList()
    })
    .FirstOrDefaultAsync();

            return result;




        }

        public async Task<PersonForAi> GetAllPersonForAI(string withdrawNo, string hosCode)
        {
            PersonForAi res = new PersonForAi();
            res.AllpersonByHos = db.AaiHealthWithdrawalHs.Where(x => x.HospitalCode == hosCode && x.WithdrawalNo == withdrawNo).Count();
            var query = from wh in db.AaiHealthWithdrawalHs
                        join ch in db.AaiHealthCheckupHs on wh.CheckupId equals ch.CheckupId
                        where wh.HospitalCode == hosCode && wh.WithdrawalNo == withdrawNo
                        select new PersonAiList
                        {
                            Fullname = $"{ch.PatientName} {ch.PatientSurname}",
                            Personalid = ch.PersonalId,
                            CheckIndate = (DateTime)ch.ReadDate,
                            ProActive = wh.Proactive,
                            OutlierType = (decimal)wh.SuggestionStatus,
                            Message = db.AaiHealthAiDescriptionMs.Where(x => Convert.ToDecimal(x.Id) == Convert.ToDecimal(wh.SuggestionDesc)).FirstOrDefault().Message
                        };

            res.personAiList = query.ToList();
            return res;
        }
    }
}
