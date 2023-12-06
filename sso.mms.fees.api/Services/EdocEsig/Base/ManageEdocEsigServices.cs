
using Microsoft.AspNetCore.Hosting;
using sso.mms.fees.api.Interface.EdocEsig.Base;
using sso.mms.fees.api.ViewModels.Responses;
using System.Text;
using System.Web;
using sso.mms.fees.api.ViewModels.EdocEsig;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;

namespace sso.mms.fees.api.Services.EdocEsig.Base
{
    
    public class ManageEdocEsigServices : IManageEdocEsigServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ManageEdocEsigServices(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<Response<ResSendSign>> SendSign(SendSignView data) 
        {
            Response<ResSendSign> res = new Response<ResSendSign>();
            try 
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = $"{ConfigureCore.baseEdoc}bdoc/send-sign";
                    string apiKey = ConfigureCore.apiKeyEdoc;
                    httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

                    var requestBody = new SendSignView
                    {
                        userId = data.userId,
                        documentRef = data.documentRef,
                        subject = data.subject,
                        body = data.body,
                        flowType = data.flowType,
                        dueDate = data.dueDate,
                        priority = data.priority,
                        successCallback = data.successCallback,
                        failCallback = data.failCallback,
                        sendTo = data.sendTo
                    };
                    string json = JsonConvert.SerializeObject(requestBody);
                    Console.WriteLine(json);

                    // Convert the JSON string to a StringContent
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<ResSendSign>(responseBody);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(responseBody);
                        res.Status = 1;
                        res.Message = "Send document successfully.";
                        res.Result = model;
                        return res;
                    }
                    else
                    {
                        Console.WriteLine("Send document failed with status code: " + response.StatusCode);
                        res.Status = 0;
                        res.Message = "Send document failed with status code: " + response.StatusCode;
                        res.Result = model;
                        return res;
                    }

                }



            }
            catch (Exception ex)
            {
                res.Status = 0;
                res.Message = ex.Message;
                res.Result = null;
                return res;
            }
        }


        public async Task<Response<ResCreateDocument>> CreateDocument(CreateDocument data)
        {
            Response<ResCreateDocument> res = new Response<ResCreateDocument>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = $"{ConfigureCore.baseEdoc}bfile/create";
                    string apiKey = ConfigureCore.apiKeyEdoc;
                    string filePath = $"{webHostEnvironment.WebRootPath}{data.pathFile}{data.filename}";
                    string userId = data.identification_number;
                    string docItemTitle = data.doc_title;
                    var fileContent = new StreamContent(File.OpenRead(filePath));
                    fileContent.Headers.Add("Content-Type", "application/pdf");

                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent(userId), "USER_ID");
                    form.Add(new StringContent(docItemTitle), "DOC_ITEM_TITLE");
                    form.Add(fileContent, "UPLOAD_DOCUMENT", data.filename);

                    httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

                    HttpResponseMessage response = await httpClient.PostAsync(url, form);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<ResCreateDocument>(responseBody);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(responseBody);
                        res.Status = 1;
                        res.Message = "Document created successfully.";
                        res.Result = model;
                        return res;
                    }
                    else
                    {
                        Console.WriteLine("Request failed with status code: " + response.StatusCode);
                        res.Status = 0;
                        res.Message = "Request failed with status code: " + response.StatusCode;
                        res.Result = model;
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Status = 0;
                res.Message = ex.Message;
                res.Result = null;
                return res;
            }
        }

        public async Task<Response<string>> GenReportFromDB(string apiUrl, string pathFile)
        {
            Response<string> res = new Response<string>();

            // pathFile ex.  /Files/Module8/Edoc/
            apiUrl = HttpUtility.UrlDecode(apiUrl);
            var values = apiUrl.Split('=')[1];
            var fileName = values.Split('&')[0];
            DateTime date = DateTime.Now;
            // Format the date and time as "yyyyMMddH24mmsstt"
            string formattedDateTime = date.ToString("yyyyMMddHHmmssff");


            string pdfFilePath = $"{webHostEnvironment.WebRootPath}{pathFile}{fileName}_{formattedDateTime}.pdf";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string jsonContent = "{}";
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] binaryData = await response.Content.ReadAsByteArrayAsync();

                        using (FileStream fileStream = new FileStream(pdfFilePath, FileMode.Create))
                        {
                            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                            {
                                // Write the binary data to the PDF file
                                binaryWriter.Write(binaryData);
                            }
                        }
                        res.Status = 1;
                        res.Message = "PDF file created successfully.";
                        res.Result = $"{fileName}_{formattedDateTime}.pdf";
                        return res;
                        //Console.WriteLine("PDF file created successfully.");
                    }
                    else
                    {
                        res.Status = 0;
                        res.Message = "Failed to retrieve data from the API.";
                        res.Result = null;
                        Console.WriteLine("Failed to retrieve data from the API.");
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    res.Status = 0;
                    res.Message = ex.Message;
                    res.Result = null;
                    Console.WriteLine($"Error: {ex.Message}");
                    return res;
                }
            }
        }

    }
}
