using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using sso.mms.helper.Services;
using sso.mms.helper.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Data.SqlClient.Server;
using AntDesign;
using sso.mms.helper;
using sso.mms.fees.api.Configs;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class ManageWithdrawalServices : IManageWithdrawalExtServices
    {
        private readonly PromoteHealthContext db;
        private readonly IUploadFileService uploadFileService;
        public ManageWithdrawalServices(PromoteHealthContext DbContext, IUploadFileService uploadFileService)
        {
            db = DbContext;
            this.uploadFileService = uploadFileService;
        }

        public async Task<List<WithdrawalView>> GetWithdrawalByHoscode(string hoscode)
        {
            try
            {
                var result = db.AaiHealthWithdrawalHs.Where(x => x.HospitalCode == hoscode).Select(y => new WithdrawalView
                {
                    WithdrawalNo = y.WithdrawalNo,
                    HospitalCode = y.HospitalCode,
                    SumChecklistPrice = (
                            from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == hoscode && wt.WithdrawalNo == y.WithdrawalNo
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                            select grouped.Sum(x => x.cm.ChecklistPrice)
                        ).Sum(),
                    WithdrawalDoc = y.WithdrawalDoc,
                }).GroupBy(z => z.WithdrawalNo) // Group by WithdrawalNo
                .Select(group => group.First()) // Select the first item from each group (distinct by WithdrawalNo)
                .ToList();



                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewAaiHealthCheckupH>> GetCheckupHInWithdrawal(string hoscode, string withdrawalNo)
        {
            try
            {

                var query = from wt in db.AaiHealthWithdrawalTs
                            join lt in db.AaiHealthCheckupListTs on wt.CheckupListId equals lt.CheckupListId
                            join ch in db.AaiHealthCheckupHs on lt.CheckupId equals ch.CheckupId
                            join cm in db.AaiHealthChecklistMs on lt.ChecklistId equals cm.ChecklistId
                            where wt.HospitalCode == hoscode && wt.WithdrawalNo == withdrawalNo
                            group new { ch, wt, cm } by new { ch.PersonalId, ch.PatientName, ch.PatientSurname, ch.CheckupId, ch.HospitalCode } into grouped
                            select new ViewAaiHealthCheckupH
                            {
                                PersonalId = grouped.Key.PersonalId,
                                PatientName = grouped.Key.PatientName,
                                PatientSurname = grouped.Key.PatientSurname,
                                CountWithdraw = grouped.Count(),
                                TotalPrice = grouped.Sum(x => x.cm.ChecklistPrice),
                                //Allprivilege = db.AaiHealthCheckupListTs.Count(cl => cl.CheckupId == grouped.Key.CheckupId),
                                CheckupId = grouped.Key.CheckupId,

                            };

                var result = query.ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<string> EditWithdrawalDoc(WithdrawalDocView dataDoc)
        {
            try
            {

                string Uploadurl;

                Uploadurl = $"{ConfigurationCores.FeesApiAddress}/Files/Module8/RequestForm/";

                List<IFormFile> files = new List<IFormFile>();

                if (dataDoc.File != null)
                {
                    files.Add(dataDoc.File);
                }

                //var uploadFileService = new UploadFileService(webHostEnvironment, configuration, httpClient);
                (string errorMessage, string imageName) = await uploadFileService.UploadImage(files, "Module8/RequestForm");
                var result = db.AaiHealthWithdrawalHs.Where(x => x.WithdrawalNo == dataDoc.WithdrawalNo && x.HospitalCode == dataDoc.HospitalCode).ToList();
                foreach (var item in result)
                {
                    item.WithdrawalDoc = Uploadurl + imageName;
                    db.Update(item);
                }
                db.SaveChanges();

                return "succees";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        //public async Task<ResponseUpload> UploadFile(IBrowserFile file)
        //{
        //    try
        //    {
        //        using (var formData = new MultipartFormDataContent())
        //        {
        //            using (var fileStream = file.OpenReadStream(maxAllowedSize: 10240000))
        //            {
        //                var fileContent = new StreamContent(fileStream);
        //                formData.Add(fileContent, "upload", file.Name);
        //                httpClient.DefaultRequestHeaders.Accept.Clear();
        //                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        //                var response = await httpClient.PostAsync($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/UploadFile", formData);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var result = response.Content.ReadAsStringAsync().Result;

        //                    var responseUpload = JsonConvert.DeserializeObject<ResponseUpload>(result);

        //                    var success = new ResponseUpload
        //                    {
        //                        FileName = responseUpload.FileName,
        //                        Path_Url = responseUpload.Path_Url,
        //                    };

        //                    return success;
        //                }
        //                else
        //                {
        //                    var fail = new ResponseUpload
        //                    {
        //                        FileName = "",
        //                        Path_Url = "",
        //                    };
        //                    return fail;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var fail = new ResponseUpload
        //        {
        //            Error = ex.Message,
        //            FileName = "",
        //            Path_Url = "",
        //        };
        //        return fail;
        //    }

        //}
    }
}
