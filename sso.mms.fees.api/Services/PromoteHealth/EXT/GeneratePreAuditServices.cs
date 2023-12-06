using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.Interface.PromoteHealth.EXT;
using System.Text;
using sso.mms.fees.api.Configs;
using System;
using sso.mms.helper.Configs;
using Newtonsoft.Json;
using System.Security.Policy;
using sso.mms.fees.api.ViewModels.Responses;
using System.Reflection.Metadata;

namespace sso.mms.fees.api.Services.PromoteHealth.EXT
{
    public class GeneratePreAuditServices : IGeneratePreAuditServices
    {
        private readonly PromoteHealthContext db;
        public string? normalPath = ConfigurationCores.NormalPath;
        //public string? proactivePath = ConfigurationCores.ProActiveInputPath;
        private readonly HttpClient httpClient;
        public GeneratePreAuditServices(PromoteHealthContext DbContext, HttpClient httpClient)
        {
            db = DbContext;
            this.httpClient = httpClient;
        }
        public async Task<bool> GenerateFile()
        {
            try
            {
                Console.WriteLine("GeneratedFile");
                int currentYear = DateTime.Now.Year;
                int previousMonth = DateTime.Now.Month - 1;
                int previousYear = currentYear;
                int? jobId = null;
                // Handle January
                if (previousMonth == 0)
                {
                    previousMonth = 12; // December
                    previousYear = previousYear - 1;
                }
                // Get All ViewExportListT
                var withDrawalNo = (previousYear + 543) + "/" + previousMonth.ToString().PadLeft(2, '0');
                var data = db.ViewExportListTs.ToList();
                var withdrawalH = data.GroupBy(item => item.CheckupId).Select(item => new AaiHealthWithdrawalH
                {
                    WithdrawalNo = withDrawalNo,
                    HospitalCode = item.Select(item => item.MainHos).FirstOrDefault(),
                    CheckupId = item.Select(item => item.CheckupId).FirstOrDefault(),
                    Status = "P",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = "MMSSYS",
                    UpdateBy = "MMSSYS",
                    Proactive = item.Select(item => item.Proactive).FirstOrDefault().ToString(),
                    TreatmentDate = item.Select(item => item.TreatmentDate).FirstOrDefault(),
                    PidManual = Convert.ToBoolean(item.Select(item => item.PidManual).FirstOrDefault())
                }).ToList();
                db.AaiHealthWithdrawalHs.AddRange(withdrawalH);
                

                //Convert ViewExportListT to AAIHEALTHWITHDRAWALT For Insert
                var listData = data.Select(item => new AaiHealthWithdrawalT
                {
                    WithdrawalNo = withDrawalNo,
                    HospitalCode = item.MainHos,
                    CheckupListId = (decimal)item.CheckupListId,
                    Status = "P",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = "MMSSYS",
                    UpdateBy = "MMSSYS",
                }).ToList();
                // update checkuplistT isexport == true
                var listTIdList = data.Select(item => item.CheckupListId).ToList();
                var listT = db.AaiHealthCheckupListTs.Where(item => listTIdList.Contains(item.CheckupListId));
                foreach (var item in listT)
                {
                    item.IsExport = true;
                }
                db.AaiHealthWithdrawalTs.AddRange(listData);
                db.SaveChanges();
                Console.WriteLine(withdrawalH);

                //build csv
                StringBuilder csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("PETITION_ID,TREATMENT_DATE,MAIN_HOS,PROACTIVE,PID_MANUAL");
                foreach (var item in withdrawalH)
                {
                    csvBuilder.AppendLine($"{item.WithdrawalHId},{item.TreatmentDate?.ToString("MM/dd/yyyy")},{item.HospitalCode.Trim().PadLeft(9, '0')},{(Convert.ToInt32(item.Proactive) > 0 ? "TRUE" : "FALSE")},{(item.PidManual == true ? "TRUE" : "FALSE")}");
                }
                string csvContent = csvBuilder.ToString();
                string fileName = $"pps_transaction_{previousYear}_{previousMonth.ToString().PadLeft(2, '0')}.csv";
                string filePath = Path.Combine(normalPath, fileName);

                //save csv file to directory
                File.WriteAllText(filePath, csvContent);
                Console.WriteLine($"CSV data saved to {filePath}");


                //ai api
                string jsonBody = $"{{\"filename\": \"{fileName}\", \"records\": {withdrawalH.Count}}}";
                //string jsonBody = $"{{\"filename\": \"pps_transaction_2023_07.csv\", \"records\": 205}}";
                Console.WriteLine("BODY TO AI ++++++");
                Console.WriteLine(jsonBody);
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                Console.WriteLine(content);
                HttpResponseMessage response = await httpClient.PostAsync("inference", content);
                if (response.IsSuccessStatusCode)
                {

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseContent}");


                    var responseData = JsonConvert.DeserializeObject<AiResponse>(responseContent);
                    jobId = responseData.jobId;
                    foreach (var item in withdrawalH)
                    {
                        item.JobId = jobId;
                    }
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
                Console.WriteLine("ai generate successfully!");


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> PingAi(PingModels data)
        {

            foreach (var item in data.filepath)
            {
                List<AiOutputCsv> values = File.ReadAllLines("/app" + item)
                                           .Skip(1)
                                           .Select(v =>
                                           {
                                               string[] values = v.Split(',');
                                               return new AiOutputCsv
                                               {
                                                   petition_id = values[0],
                                                   main_hos = values[1],
                                                   treatment_date = values[2],
                                                   proactive = values[3],
                                                   pid_manual = values[4],
                                                   end_week = values[5],
                                                   outlier_tyoes = values[6],
                                                   error_message = values[7]
                                               };
                                           })
                                           .ToList();

                foreach (var value in values)
                {
                    var entity = db.AaiHealthWithdrawalHs.FirstOrDefault(e => e.WithdrawalHId == decimal.Parse(value.petition_id));
                    if (entity != null)
                    {
                        entity.SuggestionStatus = decimal.Parse(value.outlier_tyoes);
                        if (value.error_message != null)
                        {
                            entity.SuggestionDesc = value.error_message;
                        }
                    }
                }
                db.SaveChanges();

            }
            return true;
        }

        public async Task<bool> ChekcStatus(int jobId)
        {


            HttpResponseMessage response = await httpClient.GetAsync($"status/{jobId}");
            if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseContent}");


                var responseData = JsonConvert.DeserializeObject<AiStatusResponse>(responseContent);

                if (responseData.status == "done")
                {
                    HttpResponseMessage responseOuput = await httpClient.GetAsync($"output/{jobId}");
                    if (responseOuput.IsSuccessStatusCode)
                    {

                        string responseOutputContent = await responseOuput.Content.ReadAsStringAsync();
                        Console.WriteLine($"ResponseOutput: {responseOutputContent}");


                        var responseOutputData = JsonConvert.DeserializeObject<AiOutputResponse>(responseOutputContent);

                        foreach (var item in responseOutputData.filename)
                        {
                            List<AiOutputCsv> values = File.ReadAllLines("/app" + item)
                                                       .Skip(1)
                                                       .Select(v =>
                                                       {
                                                           string[] values = v.Split(',');
                                                           return new AiOutputCsv
                                                           {
                                                               petition_id = values[0],
                                                               main_hos = values[1],
                                                               treatment_date = values[2],
                                                               proactive = values[3],
                                                               pid_manual = values[4],
                                                               end_week = values[5],
                                                               outlier_tyoes = values[6],
                                                               error_message = values[7]
                                                           };
                                                       })
                                                       .ToList();

                            foreach (var value in values)
                            {
                                var entity = db.AaiHealthWithdrawalHs.FirstOrDefault(e => e.WithdrawalHId == decimal.Parse(value.petition_id));
                                if (entity != null)
                                {
                                    entity.SuggestionStatus = decimal.Parse(value.outlier_tyoes);
                                    if (value.error_message != null)
                                    {
                                        entity.SuggestionDesc = value.error_message;
                                    }
                                }
                            }
                            db.SaveChanges();
                            
                        }
                        return true;
                    }
                    else 
                    { 
                        return false; 
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
        }
    }
}
