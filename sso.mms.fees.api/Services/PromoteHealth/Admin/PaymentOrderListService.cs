using sso.mms.fees.api.Interface.PromoteHealth.Admin;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.ViewModels.Responses;
using sso.mms.helper.Configs;
using static System.Net.WebRequestMethods;
using sso.mms.helper.Data;
using sso.mms.fees.api.ViewModels.EdocEsig;
using Newtonsoft.Json;
using AntDesign.Core.Extensions;

namespace sso.mms.fees.api.Services.PromoteHealth.Admin
{
  
        public class PaymentOrderListService : IPaymentOrderListService
        {
        private readonly IdpDbContext Idpdb;
        private readonly PromoteHealthContext db;
            private readonly HttpClient httpClient;
        public PaymentOrderListService(PromoteHealthContext DbContext, HttpClient httpClient, IdpDbContext DbIdpContext)
        {
            this.db = DbContext;
            this.Idpdb = DbIdpContext;
            this.httpClient = httpClient;
        }


        public async Task<string> Save(SaveOrderT data)
        {
            try
            {
                string PayOrderId = "";
                var IdentificationNumber = Idpdb.SsoUserMs.Where(x => x.UserName == data.usernameupdate).FirstOrDefault().IdentificationNumber;
                string checkupidtosend = "";
                foreach (var wd in data.person) 
                {
                    checkupidtosend +=  wd.CheckupId   + ",";
                    // get withdalval
                    List<AaiHealthWithdrawalT> aaiHealthWithdrawalT = new List<AaiHealthWithdrawalT>();
                    var query = from wt in db.AaiHealthWithdrawalTs
                                           where db.AaiHealthCheckupListTs
                                                     .Where(lt => lt.CheckupId == wd.CheckupId)
                                                     .Select(lt => lt.CheckupListId)
                                                     .Contains(wt.CheckupListId)
                                           select wt;
                    aaiHealthWithdrawalT = query.ToList();

                    // update withdalval
                    foreach (var upwd in aaiHealthWithdrawalT) 
                    {
                        upwd.UpdateBy = data.usernameupdate;
                        upwd.UpdateDate = DateTime.Now;
                        // bypass edoc esig
                        upwd.Status = "W";
                        db.Update(upwd);
                    }

                    //update withdalvalH
                    var aaiHealthWithdrawalHraw = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == wd.CheckupId && x.HospitalCode == wd.Hospitalcode && x.WithdrawalNo == wd.withdrawNo).FirstOrDefault();
                    aaiHealthWithdrawalHraw.Status = "W";
                    aaiHealthWithdrawalHraw.UpdateDate = DateTime.Now;
                    aaiHealthWithdrawalHraw.UpdateBy = data.usernameupdate;
                    db.Update(aaiHealthWithdrawalHraw);


                    data.HospByWitdraw.Where(x => x.HospCode == wd.Hospitalcode).FirstOrDefault().ischeck = true;
                }
                db.SaveChanges();

                // get PayOrderSetNo
                var countInjectwdNo = db.AaiHealthPayOrderTs.ToList();
                var countInjectwdNoForKeep = 0;
                if (countInjectwdNo.Count() > 0)
                {
                    countInjectwdNoForKeep = Convert.ToInt32(countInjectwdNo.Where(x => x.PayOrderSetNo.Substring(5, 2) == DateTime.Now.Month.ToString().PadLeft(2, '0')).OrderBy(x => x.PayOrderId).LastOrDefault().PayOrderSetNo.ToString().Substring(7,3));
                }
                // insert orderT
                foreach (var item in data.HospByWitdraw)
                {
                    if (item.ischeck)
                    {
                        AaiHealthPayOrderT aaiHealthPayOrderT = new AaiHealthPayOrderT();
                        aaiHealthPayOrderT.WithdrawalNo = data.InjectwdNo;
                        aaiHealthPayOrderT.HospitalCode = item.HospCode;
    
                        aaiHealthPayOrderT.PayOrderSetNo = $"108{(DateTime.Now.Year + 543).ToString().Substring(2)}{DateTime.Now.Month.ToString().PadLeft(2, '0')}{(countInjectwdNoForKeep + 1).ToString().PadLeft(3, '0')}";
                        var countPayOrderNo = db.AaiHealthPayOrderTs.Where(x => x.PayOrderSetNo == aaiHealthPayOrderT.PayOrderSetNo).Count();
                        aaiHealthPayOrderT.PayOrderNo = $"{aaiHealthPayOrderT.PayOrderSetNo}-{(countPayOrderNo + 1).ToString().PadLeft(6, '0')}";
                        aaiHealthPayOrderT.PayOrderStatus = "A";
                        aaiHealthPayOrderT.PayOrderRisk = false;
                        aaiHealthPayOrderT.CreateDate = DateTime.Now;
                        aaiHealthPayOrderT.UpdateDate = DateTime.Now;
                        aaiHealthPayOrderT.CreateBy = data.usernameupdate;
                        aaiHealthPayOrderT.UpdateBy = data.usernameupdate;
                        aaiHealthPayOrderT.PayAmount = data.person.Where(x => x.Hospitalcode == item.HospCode).Sum(x => x.Sumprice);
                        db.AaiHealthPayOrderTs.Add(aaiHealthPayOrderT);
                        db.SaveChanges();


                        // set PayOrderId in withdrawT
                        foreach (var wd in data.person)
                        {
                            // get withdalval
                            List<AaiHealthWithdrawalT> aaiHealthWithdrawalT = new List<AaiHealthWithdrawalT>();
                            var query = from wt in db.AaiHealthWithdrawalTs
                                        where db.AaiHealthCheckupListTs
                                                  .Where(lt => lt.CheckupId == wd.CheckupId)
                                                  .Select(lt => lt.CheckupListId)
                                                  .Contains(wt.CheckupListId)
                                        select wt;
                            aaiHealthWithdrawalT = query.ToList();

                            // update withdalval
                            foreach (var upwd in aaiHealthWithdrawalT)
                            {
                                upwd.PayOrderId = aaiHealthPayOrderT.PayOrderId;
                                db.Update(upwd);
                            }
                        }
                        db.SaveChanges();

                        PayOrderId += (Int32)aaiHealthPayOrderT.PayOrderId + ",";
                    }
                }
                // create pdf
                string path = $"{ConfigureCore.redirectRpt}/api/report%3FReportName=WithDrawalH%26id={checkupidtosend}";
                var resapiedoc = await httpClient.GetFromJsonAsync<Response<string>>($"{ConfigureCore.FeesApiAddress}/api/EdocEsig/base/ManageEdocEsig/GenReportFromDB?apiUrl={path}&pathFile=/Files/Module8/Edoc/");

                if (resapiedoc.Status == 1)
                {
                    CreateDocument dataCreateDocument = new CreateDocument();
                    dataCreateDocument.pathFile = "/Files/Module8/Edoc/";
                    dataCreateDocument.filename = resapiedoc.Result;
                    dataCreateDocument.doc_title = $"ใบขอเบิกค่าบริการส่งเสริมสุขภาพป้องกันโรค-งวด{data.InjectwdNo}";
                    dataCreateDocument.identification_number = IdentificationNumber;
                    
                    // create document
                    var resapicreatedocument = await httpClient.PostAsJsonAsync<CreateDocument>($"{ConfigureCore.FeesApiAddress}/api/EdocEsig/base/ManageEdocEsig/CreateDocument", dataCreateDocument);
                    if (resapicreatedocument.IsSuccessStatusCode)
                    {
                        var response1 = await resapicreatedocument.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<Response<ResCreateDocument>>(response1);

                        // send sign
                        List<string> sendTo = new List<string>();
                        sendTo.Add("9229860177009");

                        SendSignView dataSendSign = new SendSignView();
                        dataSendSign.userId = IdentificationNumber;
                        dataSendSign.documentRef = model.Result.Value;
                        dataSendSign.subject = "";
                        dataSendSign.body = "";
                        dataSendSign.flowType = 1;
                        dataSendSign.dueDate = "";
                        dataSendSign.priority = 1;
                        dataSendSign.successCallback = $"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/ManageCallBack/CallBackPromoteHealth?Fail=False&SignBy={data.usernameupdate}&PayOrderId={PayOrderId}";
                        dataSendSign.failCallback = $"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/admin/ManageCallBack/CallBackPromoteHealth?Fail=True";
                        dataSendSign.sendTo = sendTo;

                        var ressendsign = await httpClient.PostAsJsonAsync<SendSignView>($"{ConfigureCore.FeesApiAddress}/api/EdocEsig/base/ManageEdocEsig/SendSign", dataSendSign);
                        Console.WriteLine(model);
                        return "success";
                    }
                    else
                    {
                        return "Not success";
                    }
                }
                else {
                    return "Not success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<GetPaymentOrderList> GetByWithdrawalNo(string withdrawalNo)
            {
                try
                {
                    var resultpersoncount = db.AaiHealthCheckupListTs.Join(
                    db.AaiHealthWithdrawalTs,
                    checkupList => checkupList.CheckupListId,
                    withdrawal => withdrawal.CheckupListId,
                    (checkupList, withdrawal) => new { CheckupList = checkupList, Withdrawal = withdrawal }
                    )
                    .Where(joinResult => joinResult.Withdrawal.WithdrawalNo == withdrawalNo && joinResult.Withdrawal.Status == "P")
                    .Select(joinResult => joinResult.CheckupList.CheckupId)
                    .Distinct()
                    .Count();

                    var result = db.AaiHealthWithdrawalTs.Where(w => w.WithdrawalNo == withdrawalNo && w.Status == "P").GroupBy(g => g.HospitalCode).Select(s => new GetPaymentOrderList
                    {
                        HospitalCode = s.Key,
                    }).ToList();
                    
                    GetPaymentOrderList data = new GetPaymentOrderList()
                    {
                        HospitalCount = result.Count(),
                        PersonCount = resultpersoncount,

                    };
                return data;
                }
                catch (Exception ex)
                {
                    return null;
                }
               
            
            }

        public async Task<List<GetPaymentOrderList>> GetHospByWitdraw(string withdrawalNo)
        {
            try
            {

                var result = db.AaiHealthWithdrawalTs
                    .Where(w => w.WithdrawalNo == withdrawalNo)
                    .GroupBy(g => g.HospitalCode)
                    .Select(s => new GetPaymentOrderList
                {
                    HospCode = s.Key,
                    HospName = db.HosHospitals.FirstOrDefault(w => w.HospCode9.ToString() == s.Key).HospName,
                    HospDisplayName = db.HosHospitals.FirstOrDefault(w => w.HospCode9.ToString() == s.Key).HospDisplayName,
                    isRequestForm = db.AaiHealthWithdrawalHs.Where(w => db.AaiHealthCheckupListTs
                    .Where(lt => lt.CheckupListId == db.AaiHealthWithdrawalTs.Where(x => x.HospitalCode == s.Key && x.WithdrawalNo == withdrawalNo).FirstOrDefault().CheckupListId)
                    .Select(lt => lt.CheckupId)
                    .Contains(w.CheckupId)).FirstOrDefault().WithdrawalDoc == null ? false : true, 
                    WithdrawalDoc = db.AaiHealthWithdrawalHs.Where(w => db.AaiHealthCheckupListTs
                    .Where(lt => lt.CheckupListId == db.AaiHealthWithdrawalTs.Where(x => x.HospitalCode == s.Key && x.WithdrawalNo == withdrawalNo).FirstOrDefault().CheckupListId)
                    .Select(lt => lt.CheckupId)
                    .Contains(w.CheckupId)).FirstOrDefault().WithdrawalDoc,
                    PersonCount = (
                            from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == s.Key && wt.WithdrawalNo == withdrawalNo && wt.Status == "P"
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname} into grouped
                            select grouped.Key
                    ).Count(),
                    PersonCountAll = (
                            from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == s.Key && wt.WithdrawalNo == withdrawalNo
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname} into grouped
                            select grouped.Key
                    ).Count(),
                    SumPrice = (
                            from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == s.Key && wt.WithdrawalNo == withdrawalNo && wt.Status == "P"
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                            select grouped.Sum(x => x.cm.ChecklistPrice)
                        ).Sum(),
                    SumPriceAll = (
                            from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == s.Key && wt.WithdrawalNo == withdrawalNo
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                            select grouped.Sum(x => x.cm.ChecklistPrice)
                        ).Sum(),
                    isShowAi = db.AaiHealthWithdrawalHs.Where(x => x.HospitalCode == s.Key && x.WithdrawalNo == withdrawalNo && x.SuggestionStatus != null).Count() > 0 ? true : false
                    }).ToList();
                result.RemoveAll(x => x.SumPrice == 0);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GetPaymentOrderList>> GetPaymentOList()
                {
                    try
                    {
                        var data = db.AaiHealthWithdrawalTs.GroupBy(x => x.WithdrawalNo );
                        var result = data.Select(x => new GetPaymentOrderList
                        {
                            WithdrawalNo = x.Key,
                            SumPrice = db.AaiHealthWithdrawalTs
                            .Where(wt => wt.WithdrawalNo == x.Key && wt.Status == "P")
                            .Join(db.AaiHealthCheckupListTs,
                                  wt => wt.CheckupListId,
                                  lt => lt.CheckupListId,
                                  (wt, lt) => new { wt, lt })
                            .Join(db.AaiHealthChecklistMs,
                                  wtlt => wtlt.lt.ChecklistId,
                                  cm => cm.ChecklistId,
                                  (wtlt, cm) => new { wtlt, cm })
                            .Sum(result => result.cm.ChecklistPrice) == null 
                            ? 0 : db.AaiHealthWithdrawalTs
                            .Where(wt => wt.WithdrawalNo == x.Key && wt.Status == "P")
                            .Join(db.AaiHealthCheckupListTs,
                                  wt => wt.CheckupListId,
                                  lt => lt.CheckupListId,
                                  (wt, lt) => new { wt, lt })
                            .Join(db.AaiHealthChecklistMs,
                                  wtlt => wtlt.lt.ChecklistId,
                                  cm => cm.ChecklistId,
                                  (wtlt, cm) => new { wtlt, cm })
                            .Sum(result => result.cm.ChecklistPrice),
                            HospitalCount = x.Count()
                        }).ToList();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }

        public async Task<List<AaiHealthCheckupHViewModel>> GetPersonPayorderSetNoBy(string paayordersetno, string? hoscode) 
        {
            try
            {
                if (hoscode == null)
                {
                var query = from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            join po in db.AaiHealthPayOrderTs on wt.PayOrderId equals po.PayOrderId
                            where po.PayOrderSetNo == paayordersetno
                            group new { wt, lt, ch, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                            select new AaiHealthCheckupHViewModel
                            {
                                PersonalId = grouped.Key.PersonalId,
                                PatientName = grouped.Key.PatientName,
                                PatientSurname = grouped.Key.PatientSurname,
                                Useprivilege = grouped.Count(),
                                Sumprice = grouped.Sum(x => x.cm.ChecklistPrice),
                                Allprivilege = db.AaiHealthCheckupListTs.Count(cl => cl.CheckupId == grouped.Key.CheckupId),
                                CheckupId = grouped.Key.CheckupId,
                                Hospitalcode = grouped.Key.HospitalCode,
                                isShowAi = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionStatus != null ? true : false,
                                withdrawHId = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().WithdrawalHId,
                                statusAi = (decimal)db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionStatus,
                                desAi = db.AaiHealthAiDescriptionMs.Where(x => x.Id == Convert.ToDecimal(db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionDesc)).FirstOrDefault().Message,
                                isShowRequest = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().WithdrawalDoc == null ? false : true,
                                HospitalName = db.HosHospitals.FirstOrDefault(w => w.HospCode9.ToString() == grouped.Key.HospitalCode).HospName
                            };

                    var result = query.ToList();
                    return result;
                }
                else {
                    var query = from wt in db.AaiHealthWithdrawalTs
                                join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                                join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                                join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                                join po in db.AaiHealthPayOrderTs on wt.PayOrderId equals po.PayOrderId
                                where po.PayOrderSetNo == paayordersetno && wt.HospitalCode == hoscode
                                group new { wt, lt, ch, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                                select new AaiHealthCheckupHViewModel
                                {
                                    PersonalId = grouped.Key.PersonalId,
                                    PatientName = grouped.Key.PatientName,
                                    PatientSurname = grouped.Key.PatientSurname,
                                    Useprivilege = grouped.Count(),
                                    Sumprice = grouped.Sum(x => x.cm.ChecklistPrice),
                                    Allprivilege = db.AaiHealthCheckupListTs.Count(cl => cl.CheckupId == grouped.Key.CheckupId),
                                    CheckupId = grouped.Key.CheckupId,
                                    Hospitalcode = grouped.Key.HospitalCode,
                                    isShowAi = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionStatus != null ? true : false,
                                    withdrawHId = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().WithdrawalHId,
                                    statusAi = (decimal)db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionStatus,
                                    desAi = db.AaiHealthAiDescriptionMs.Where(x => x.Id == Convert.ToDecimal(db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionDesc)).FirstOrDefault().Message,
                                    isShowRequest = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().WithdrawalDoc == null ? false : true,
                                    HospitalName = db.HosHospitals.FirstOrDefault(w => w.HospCode9.ToString() == grouped.Key.HospitalCode).HospName
                                };

                    var result = query.ToList();
                    return result;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<List<AaiHealthCheckupHViewModel>> GetPerson(string withdrawalNo,string hospCode)
        {
            try
            {
                var query = from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == hospCode && wt.WithdrawalNo == withdrawalNo && wt.Status == "P"
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                            select new AaiHealthCheckupHViewModel
                            {
                                PersonalId = grouped.Key.PersonalId,
                                PatientName = grouped.Key.PatientName,
                                PatientSurname = grouped.Key.PatientSurname,
                                Useprivilege = grouped.Count(),
                                Sumprice = grouped.Sum(x => x.cm.ChecklistPrice),
                                Allprivilege = db.AaiHealthCheckupListTs.Count(cl => cl.CheckupId == grouped.Key.CheckupId),
                                CheckupId = grouped.Key.CheckupId,
                                Hospitalcode = grouped.Key.HospitalCode,
                                isShowAi = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionStatus != null ? true : false,
                                withdrawHId = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().WithdrawalHId,
                                statusAi = (decimal)db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionStatus,
                                desAi = db.AaiHealthAiDescriptionMs.Where(x => x.Id == Convert.ToDecimal(db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().SuggestionDesc)).FirstOrDefault().Message,
                                isShowRequest = db.AaiHealthWithdrawalHs.Where(x => x.CheckupId == grouped.Key.CheckupId).FirstOrDefault().WithdrawalDoc == null ? false : true,
                                withdrawNo = withdrawalNo

                            };

                var result = query.ToList();


                return result;
            }
            catch (Exception ex)
            {
                return  null;
            }
        }

        public async Task<List<GetPayOrderListT>> GetPayOrder()
        {
            try {
                var result = db.AaiHealthPayOrderTs
                .Where(item => item.PayOrderStatus == "B" || item.PayOrderStatus == "A")
                .GroupBy(item => new
                {
                    item.PayOrderSetNo,
                    item.WithdrawalNo,
                    item.PayOrderStatus,
                    APPROVE_DATE = ((DateTime)item.ApproveDate).Date
                })
                .Select(group => new GetPayOrderListT
                {
                    PayOrderSetNo = group.Key.PayOrderSetNo,
                    CountHos = group.Count(),
                    WithdrawalNo = group.Key.WithdrawalNo,
                    Allprice = (decimal)group.Sum(item => item.PayAmount),
                    PayOrderStatus = group.Key.PayOrderStatus,
                    ApproveDate = group.Key.APPROVE_DATE
                }).ToList();

                return result;
            }
            catch {
                return null;
            }
            
        }


        public async Task<List<GetPayOrderListT>> GetPayOrderHis()
        {
            try
            {
                var result = db.AaiHealthPayOrderTs
                .Where(item => item.PayOrderStatus != "B" && item.PayOrderStatus != "A")
                .GroupBy(item => new
                {
                    item.PayOrderSetNo,
                    item.WithdrawalNo,
                    item.PayOrderStatus,
                    APPROVE_DATE = ((DateTime)item.ApproveDate).Date
                })
                .Select(group => new GetPayOrderListT
                {
                    PayOrderSetNo = group.Key.PayOrderSetNo,
                    CountHos = group.Count(),
                    WithdrawalNo = group.Key.WithdrawalNo,
                    Allprice = (decimal)group.Sum(item => item.PayAmount),
                    PayOrderStatus = group.Key.PayOrderStatus,
                    ApproveDate = group.Key.APPROVE_DATE
                }).ToList();

                return result;
            }
            catch
            {
                return null;
            }

        }


        public async Task<string> UpdatePayOrder(List<GetPayOrderListT> data)
        {
            try 
            {
                foreach (var item in data.Where(x => x.ischeck))
                {
                    List<AaiHealthPayOrderT> res = db.AaiHealthPayOrderTs.Where(x => x.PayOrderSetNo == item.PayOrderSetNo).ToList();

                    foreach (var item1 in res)
                    {
                        AaiHealthPayOrderT res1 = db.AaiHealthPayOrderTs.FirstOrDefault(x => x.PayOrderId == item1.PayOrderId);
                        res1.UpdateBy = item.updateBy;
                        // by pass 850 api 
                        res1.PayOrderStatus = "D";
                        res1.ApproveBy = item.updateBy;
                        res1.ApproveDate = DateTime.Now;
                        db.Update(res1);
                    }


                }
                db.SaveChanges();
                return "success";
            }
            catch (Exception e) 
            {
                return e.Message;
            }
        }
    }
    
}