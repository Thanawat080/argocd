using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using sso.mms.batch.ViewModels;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.OracleIdpModels;
using sso.mms.helper.ViewModels;
using System;
using System.Net.Mail;
using System.Drawing.Drawing2D;
using System.Net.Http.Headers;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using sso.mms.helper.Services;
using System.Net.Http;
using Newtonsoft.Json;
using sso.mms.helper.PortalModel;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using System.Linq;
using Microsoft.OpenApi.Any;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Collections;
using System.Text.Json;

namespace sso.mms.batch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchSyncController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly PromoteHealthContext oracleDbContext;
        private readonly IdpDbContext IdpDbContext;
        private readonly JsonSerializerOptions _options;
        private readonly PortalDbContext PortalDbContext;
        public BatchSyncController(HttpClient httpClient, PromoteHealthContext oracleDbContext, IdpDbContext IdpDbContext, PortalDbContext PortalDbContext)
        {
            this.oracleDbContext = oracleDbContext;
            this.IdpDbContext = IdpDbContext;
            this.httpClient = httpClient;
            this.httpClient.Timeout = TimeSpan.FromSeconds(90000);
            this.PortalDbContext = PortalDbContext;
        }
        [HttpGet("Helloworld")]
        public async Task<string> Helloworld()
        {
            return "Helloworld";
        }

        [NonAction]
        public static async Task<ResponseModel> SSOSendEmail(string subject, string? otpCode, string toEmail, string? username, string? password)
        {


            var fromEmail = "mmsadmin@sso.go.th";
            var fromName = "SSO MMS Servcie";
            var fromAddress = new MailAddress(fromEmail, fromName);
            var toAddress = new MailAddress(toEmail);
            var smtp = new SmtpClient
            {
                Host = "smtp.sso.go.th",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            if (otpCode == null)
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = $"Username : {username} \nPassword : {password}"
                })
                {
                    try
                    {
                        smtp.Send(message);
                        return new ResponseModel { issucessStatus = true, statusCode = "200", statusMessage = "Email sent successfully." };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel { issucessStatus = false, statusCode = "200", statusMessage = ex.Message.ToString() };
                    }
                }
            }
            else
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = $"otp ของคุณ : {otpCode}"
                })
                {
                    try
                    {
                        smtp.Send(message);
                        return new ResponseModel { issucessStatus = true, statusCode = "200", statusMessage = "Email sent successfully." };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel { issucessStatus = false, statusCode = "400", statusMessage = ex.Message.ToString() };
                    }
                }

            }
        }

        [HttpGet("Checksendemail")]
        public async Task<string> Checksendemail()
        {
            var res = await SSOSendEmail("Test Send Email", "1111", "thanawatearth1234@gmail.com", null, null);
            if ((bool)res.issucessStatus)
            {

                return "success";
            }
            else
            {
                return "fail";
            }
        }


        [NonAction]
        public async Task<ResponseModel> CheckDopaProd(CheckDopaModel dopaData)
        {
            var response = new ResponseModel();
            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                string dopaUrl1 = String.Format($"{ConfigureCore.baseDopavalidateurl}?ConsumerSecret=n1MPfBuwyxS&AgentID={dopaData.UID13}");
                var client = new HttpClient(clientHandler);
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(dopaUrl1),
                    Headers =
                    {
                        {"Consumer-Key", "adb283e5-e838-411c-bf70-b64ba2cb7b31"}
                    },
                };
                var validate = client.SendAsync(httpRequestMessage).Result;

                if (validate.IsSuccessStatusCode)
                {
                    var json_result = validate.Content.ReadAsStringAsync().Result;
                    ResponseDopaAuth result = JsonConvert.DeserializeObject<ResponseDopaAuth>(json_result);
                    var token = result.Result;
                    var client2 = new HttpClient(clientHandler);
                    var verify = String.Format($"{ConfigureCore.baseDopaverification}?CitizenID={dopaData.UID13}&FirstName={dopaData.Fname}&LastName={dopaData.Lname}&BEBirthDate={dopaData.BOD}&LaserCode={dopaData.LAZER_CODE}");
                    var httpRequestMessage2 = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(verify),
                        Headers =
                        {
                            {"Consumer-Key", "adb283e5-e838-411c-bf70-b64ba2cb7b31"},
                            {"Token", token }
                        },
                    };
                    var response2 = client2.SendAsync(httpRequestMessage2).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        var json_result2 = response2.Content.ReadAsStringAsync().Result;
                        //string data = await content.ReadAsStringAsync().ConfigureAwait(false);
                        ResponseDopaAuth result2 = JsonConvert.DeserializeObject<ResponseDopaAuth>(json_result2);
                        response = new ResponseModel
                        {
                            issucessStatus = true,
                            statusCode = "200",
                            statusMessage = result2.Result
                        };
                        return response;
                    }
                    else
                    {
                        response = new ResponseModel
                        {
                            issucessStatus = false,
                            statusCode = "400",
                            statusMessage = "check dopa fail"
                        };
                        return response;
                    };
                }
                else
                {
                    response = new ResponseModel
                    {
                        issucessStatus = false,
                        statusCode = "400",
                        statusMessage = "check cid fail"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = ex.Message
                };
                return response;
            }
        }

        [HttpGet("BatchCheckDopa")]
        public async Task<ResponseModel> BatchCheckDopa()
        {
            var dopaData = new CheckDopaModel
            {
                UID13 = "1103703085646",
                BOD = "25430822",
                LAZER_CODE = "ME1128566700",
                Fname = "ธนวัฒน์",
                Lname = "เนื่องรัศมี"
            };
            ResponseModel res = await CheckDopaProd(dopaData);
            return res;

        }


        [HttpGet("syncAllSSOUserTOPG")]
        public async Task<string> syncAllSSOUserTOPG() 
        {
            try 
            {
                var ssouserall = IdpDbContext.SsoUserMs.ToList();
                var data = new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", $"client-sso-mms-admin"},
                    {"client_secret", ConfigureCore.secretKeyRealmGroupAdmin}
                };

                var content = new FormUrlEncodedContent(data);
                var response = await httpClient.PostAsync($"{ConfigureCore.baseAddressIdpKeycloak}realms/sso-mms-admin/protocol/openid-connect/token", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var reskeycloak = JsonConvert.DeserializeObject<KeyCloakModels>(responseContent)!;

                Console.WriteLine($"reskeycloak {reskeycloak}");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reskeycloak.access_token);
                var responseuser = await httpClient.GetAsync($"{ConfigureCore.baseAddressIdpKeycloak}admin/realms/sso-mms-admin/users?max=-1");

                if (responseuser.IsSuccessStatusCode)
                {
                    var responseBody = await responseuser.Content.ReadAsStringAsync();

                    List<SsoUserModel> itemList = JsonConvert.DeserializeObject<List<SsoUserModel>>(responseBody);

                    //Console.WriteLine($"itemList {itemList}");
                    foreach (var item in itemList)
                    {
                        //Console.WriteLine(item);
                        Console.WriteLine(item.username);
                        //string item_username = Convert.ToString(item.username);
                        SsoUserM olditem = new SsoUserM();
                        olditem = ssouserall.Where(q => q.UserName.Contains(Convert.ToString(item.username)) && q.IsStatus == 1).FirstOrDefault();


                        if (olditem is null)
                        {
                            Console.WriteLine("insert");
                            IdpDbContext.SsoUserMs.Add(new SsoUserM
                            {
                                UserName = Convert.ToString(item.username),
                                FirstName = item.firstName is not null ? Convert.ToString(item.firstName).Split(' ')[0] : null,
                                LastName = item.lastName is not null ? Convert.ToString(item.lastName) : null,
                                Email = item.email is not null ? Convert.ToString(item.email) : null,
                                SsoBranchCode = item.attributes.SSObranchCode is not null ? Convert.ToString(item.attributes.SSObranchCode[0]) : null,
                                IdentificationNumber = item.attributes.identification_number is not null ? Convert.ToString(item.attributes.identification_number[0]) : null,
                                IsStatus = 1,
                                UpdateBy = "MMS-ADMIN",
                                CreateBy = "MMS-ADMIN",
                                SsoPersonFieldPosition = item.attributes.ssopersonfieldposition is not null ? item.attributes.ssopersonfieldposition[0] : null,
                                WorkingdeptDescription = item.attributes.workingdeptdescription is not null ? item.attributes.workingdeptdescription[0] : null

                            });
                        }
                        else 
                        {
                            Console.WriteLine("update");
                            olditem.UserName = Convert.ToString(item.username);
                            olditem.FirstName = item.firstName is not null ? Convert.ToString(item.firstName).Split(' ')[0] : null;
                            olditem.LastName = item.lastName is not null ? Convert.ToString(item.lastName) : null;
                            olditem.Email = item.email is not null ? Convert.ToString(item.email) : null;
                            olditem.SsoBranchCode = item.attributes.SSObranchCode is not null ? Convert.ToString(item.attributes.SSObranchCode[0]) : null;
                            olditem.IdentificationNumber = item.attributes.identification_number is not null ? Convert.ToString(item.attributes.identification_number[0]) : null;
                            olditem.UpdateBy = "MMS-ADMIN-UPDATE";
                            olditem.CreateBy = "MMS-ADMIN";
                            olditem.SsoPersonFieldPosition = item.attributes.ssopersonfieldposition is not null ? item.attributes.ssopersonfieldposition[0] : null;
                            olditem.WorkingdeptDescription = item.attributes.workingdeptdescription is not null ? item.attributes.workingdeptdescription[0] : null;
                            IdpDbContext.Update(olditem);
                        }
                    }
                    IdpDbContext.SaveChanges();
                }


                return "success";
            
            }catch  (Exception ex) {
                return ex.Message;
            }
        }


        [HttpGet("syncSsoUser")]
        public async Task<string> syncSsoUser()
        {
            try
            {
                var setA = oracleDbContext.UserMSsos.ToList().Select(x => new BatchSyncModels {
                    Id = x.Id,
                    PrefixMCode = x.PrefixMCode,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    SsoBranchCode = x.SsoBranchCode,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ImagePath = x.ImagePath,
                    ImageName = x.ImageName,
                    GroupId = x.GroupId,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    SsoDeptId = x.SsoDeptId,
                    SsoPositionId = x.SsoPositionId,
                    UserName = x.UserName,
                    UserType = x.UserType,
                    IdentificationNumber = x.IdentificationNumber,
                    IsActive = x.IsActive
                });
                var setB = IdpDbContext.SsoUserMs.ToList().Select(x => new BatchSyncModels
                {
                    Id = x.Id,
                    PrefixMCode = x.PrefixMCode,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    SsoBranchCode = x.SsoBranchCode,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ImagePath = x.ImagePath,
                    ImageName = x.ImageName,
                    GroupId = x.GroupId,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    SsoDeptId = x.SsoDeptId,
                    SsoPositionId = x.SsoPositionId,
                    UserName = x.UserName,
                    UserType = x.UserType,
                    IdentificationNumber = x.IdentificationNumber,
                    IsActive = x.IsActive
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa != sb));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.UserName != fiu.UserName));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.UserName != sb.UserName));
                var forDelete = setA.Where(sa => setB.All(sb => sa.UserName != sb.UserName));

                var allusermsso = oracleDbContext.UserMSsos.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    UserMSso usermsso = new UserMSso();
                    usermsso = allusermsso.Where(y => y.UserName == x.UserName).FirstOrDefault();
                    oracleDbContext.UserMSsos.Remove(usermsso);
                    
                }
                oracleDbContext.SaveChanges();

                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var item1 in forInsert) 
                {
                    UserMSso usermsso = new UserMSso();
                    usermsso.Id = item1.Id;
                    usermsso.PrefixMCode = item1.PrefixMCode;
                    usermsso.FirstName = item1.FirstName;
                    usermsso.MiddleName = item1.MiddleName;
                    usermsso.LastName = item1.LastName;
                    usermsso.SsoBranchCode = item1.SsoBranchCode;
                    usermsso.Email = item1.Email;
                    usermsso.Mobile = item1.Mobile;
                    usermsso.ImagePath = item1.ImagePath;
                    usermsso.ImageName = item1.ImageName;
                    usermsso.GroupId = item1.GroupId;
                    usermsso.IsStatus = item1.IsStatus;
                    usermsso.CreateDate = item1.CreateDate;
                    usermsso.CreateBy = item1.CreateBy;
                    usermsso.UpdateDate = item1.UpdateDate;
                    usermsso.UpdateBy = item1.UpdateBy;
                    usermsso.SsoDeptId = item1.SsoDeptId;
                    usermsso.SsoPositionId = item1.SsoPositionId;
                    usermsso.UserName = item1.UserName;
                    usermsso.UserType = item1.UserType;
                    usermsso.IdentificationNumber = item1.IdentificationNumber;
                    usermsso.IsActive = item1.IsActive;
                    await oracleDbContext.UserMSsos.AddAsync(usermsso);
                    
                }
                await oracleDbContext.SaveChangesAsync();

                if (setA.Count() == 0)
                {
                    return "success";
                }
                //update 
                foreach (var item1 in Forupdate)
                {
                    UserMSso usermsso = new UserMSso();
                    usermsso = allusermsso.Where(x => x.UserName == item1.UserName).FirstOrDefault();
                    usermsso.PrefixMCode = item1.PrefixMCode;
                    usermsso.FirstName = item1.FirstName;
                    usermsso.MiddleName = item1.MiddleName;
                    usermsso.LastName = item1.LastName;
                    usermsso.SsoBranchCode = item1.SsoBranchCode;
                    usermsso.Email = item1.Email;
                    usermsso.Mobile = item1.Mobile;
                    usermsso.ImagePath = item1.ImagePath;
                    usermsso.ImageName = item1.ImageName;
                    usermsso.GroupId = item1.GroupId;
                    usermsso.IsStatus = item1.IsStatus;
                    usermsso.CreateDate = item1.CreateDate;
                    usermsso.CreateBy = item1.CreateBy;
                    usermsso.UpdateDate = item1.UpdateDate;
                    usermsso.UpdateBy = item1.UpdateBy;
                    usermsso.SsoDeptId = item1.SsoDeptId;
                    usermsso.SsoPositionId = item1.SsoPositionId;
                    usermsso.UserType = item1.UserType;
                    usermsso.IdentificationNumber = item1.IdentificationNumber;
                    usermsso.IsActive = item1.IsActive;
                    oracleDbContext.Update(usermsso);
                    
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception e){
                return e.Message;
            }
            
        }


        [HttpGet("syncHosUser")]
        public async Task<string> syncHosUser()
        {
            try
            {
                var setA = oracleDbContext.UserMHospitals.ToList().Select(x => new BatchSyncHosModels {
                    Id = x.Id,
                    PrefixMCode = x.PrefixMCode,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    SsoBranchCode = x.SsoBranchCode,
                    MedicalName = x.MedicalName,
                    MedicalCode = x.MedicalCode,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ImagePath = x.ImagePath,
                    ImageName = x.ImageName,
                    GroupId = x.GroupId,
                    Address = x.Address,
                    Moo = x.Moo,
                    DistrictCode = x.DistrictCode,
                    SubdistrictCode = x.SubdistrictCode,
                    ProvinceCode = x.ProvinceCode,
                    ZipCode = x.ZipCode,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    IsActive = x.IsActive,
                    HospitalMId = x.HospitalMId,
                    UserName = x.UserName,
                    IdenficationNumber = x.IdenficationNumber,
                    IsCheckDopa = x.IsCheckDopa,
                    PositionName = x.PositionName,
                    HospitalMCode = x.HospitalMCode
                });

                var setB = IdpDbContext.HospitalUserMs.ToList().Select(x => new BatchSyncHosModels {
                    Id = x.Id,
                    PrefixMCode = x.PrefixMCode,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    SsoBranchCode = x.SsoBranchCode,
                    MedicalName = x.MedicalName,
                    MedicalCode = x.MedicalCode,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ImagePath = x.ImagePath,
                    ImageName = x.ImageName,
                    GroupId = x.GroupId,
                    Address = x.Address,
                    Moo = x.Moo,
                    DistrictCode = x.DistrictCode,
                    SubdistrictCode = x.SubdistrictCode,
                    ProvinceCode = x.ProvinceCode,
                    ZipCode = x.ZipCode,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    IsActive = x.IsActive,
                    HospitalMId = x.HospitalMId,
                    UserName = x.UserName,
                    IdenficationNumber = x.IdenficationNumber,
                    IsCheckDopa = x.IsCheckDopa,
                    PositionName = x.PositionName
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa != sb));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.UserName != fiu.UserName));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.UserName != sb.UserName));
                var forDelete = setA.Where(sa => setB.All(sb => sa.UserName != sb.UserName));

                var allusermhos = oracleDbContext.UserMHospitals.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    UserMHospital usermhos = new UserMHospital();
                    usermhos = allusermhos.Where(y => y.UserName == x.UserName).FirstOrDefault();
                    oracleDbContext.UserMHospitals.Remove(usermhos);
                   
                }
                oracleDbContext.SaveChanges();

                if (setA.Count() == 0) {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var item in forInsert)
                {
                    UserMHospital usermhos = new UserMHospital();
                    usermhos.Id = item.Id;
                    usermhos.PrefixMCode = item.PrefixMCode;
                    usermhos.FirstName = item.FirstName;
                    usermhos.MiddleName = item.MiddleName;
                    usermhos.LastName = item.LastName;
                    usermhos.SsoBranchCode = item.SsoBranchCode;
                    usermhos.MedicalName = item.MedicalName;
                    usermhos.MedicalCode = item.MedicalCode;
                    usermhos.Email = item.Email;
                    usermhos.Mobile = item.Mobile;
                    usermhos.ImagePath = item.ImagePath;
                    usermhos.ImageName = item.ImageName;
                    usermhos.GroupId = item.GroupId;
                    usermhos.Address = item.Address;
                    usermhos.Moo = item.Moo;
                    usermhos.DistrictCode = item.DistrictCode;
                    usermhos.SubdistrictCode = item.SubdistrictCode;
                    usermhos.ProvinceCode = item.ProvinceCode;
                    usermhos.ZipCode = item.ZipCode;
                    usermhos.IsStatus = item.IsStatus;
                    usermhos.CreateDate = item.CreateDate;
                    usermhos.CreateBy = item.CreateBy;
                    usermhos.UpdateDate = item.UpdateDate;
                    usermhos.UpdateBy = item.UpdateBy;
                    usermhos.IsActive = item.IsActive;
                    usermhos.HospitalMId = item.HospitalMId;
                    usermhos.UserName = item.UserName;
                    usermhos.IdenficationNumber = item.IdenficationNumber;
                    usermhos.IsCheckDopa = item.IsCheckDopa;
                    usermhos.PositionName = item.PositionName;
                    usermhos.HospitalMCode = item.MedicalCode;
                    await oracleDbContext.UserMHospitals.AddAsync(usermhos);
                   
                }
                await oracleDbContext.SaveChangesAsync();
                if (setA.Count() == 0) 
                {
                    return "success";
                }
                //update 
                foreach (var item in Forupdate)
                {
                    UserMHospital usermhos = new UserMHospital();
                    usermhos = allusermhos.Where(x => x.UserName == item.UserName).FirstOrDefault();
                    usermhos.PrefixMCode = item.PrefixMCode;
                    usermhos.FirstName = item.FirstName;
                    usermhos.MiddleName = item.MiddleName;
                    usermhos.LastName = item.LastName;
                    usermhos.SsoBranchCode = item.SsoBranchCode;
                    usermhos.MedicalName = item.MedicalName;
                    usermhos.MedicalCode = item.MedicalCode;
                    usermhos.Email = item.Email;
                    usermhos.Mobile = item.Mobile;
                    usermhos.ImagePath = item.ImagePath;
                    usermhos.ImageName = item.ImageName;
                    usermhos.GroupId = item.GroupId;
                    usermhos.Address = item.Address;
                    usermhos.Moo = item.Moo;
                    usermhos.DistrictCode = item.DistrictCode;
                    usermhos.SubdistrictCode = item.SubdistrictCode;
                    usermhos.ProvinceCode = item.ProvinceCode;
                    usermhos.ZipCode = item.ZipCode;
                    usermhos.IsStatus = item.IsStatus;
                    usermhos.CreateDate = item.CreateDate;
                    usermhos.CreateBy = item.CreateBy;
                    usermhos.UpdateDate = item.UpdateDate;
                    usermhos.UpdateBy = item.UpdateBy;
                    usermhos.IsActive = item.IsActive;
                    usermhos.HospitalMId = item.HospitalMId;
                    usermhos.IdenficationNumber = item.IdenficationNumber;
                    usermhos.IsCheckDopa = item.IsCheckDopa;
                    usermhos.PositionName = item.PositionName;
                    if (usermhos.HospitalMCode == null) {
                        usermhos.HospitalMCode = item.MedicalCode;
                    }
                    oracleDbContext.Update(usermhos);
                   
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }


        [HttpGet("syncAuditUser")]
        public async Task<string> syncAuditUser()
        {
            try
            {

                var setA = oracleDbContext.UserMAuditors.ToList().Select(x => new BatchSyncAuditModels {
                    Id = x.Id,
                    PrefixMCode = x.PrefixMCode,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    IdenficationNumber = x.IdenficationNumber,
                    CertNo = x.CertNo,
                    StartDate = x.StartDate,
                    ExpireDate = x.ExpireDate,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ImagePath = x.ImagePath,
                    ImageName = x.ImageName,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    UserName = x.UserName,
                    IsActive = x.IsActive,
                    Birthdate = x.Birthdate,
                    Position = x.Position
                });
                var setB = IdpDbContext.AuditorUserMs.ToList().Select(x => new BatchSyncAuditModels
                {
                    Id = x.Id,
                    PrefixMCode = x.PrefixMCode,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    IdenficationNumber = x.IdenficationNumber,
                    CertNo = x.CertNo,
                    StartDate = x.StartDate,
                    ExpireDate = x.ExpireDate,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    ImagePath = x.ImagePath,
                    ImageName = x.ImageName,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    UserName = x.UserName,
                    IsActive = x.IsActive,
                    Birthdate = x.Birthdate,
                    Position = x.Position
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa != sb));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.UserName != fiu.UserName));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.UserName != sb.UserName));
                var forDelete = setA.Where(sa => setB.All(sb => sa.UserName != sb.UserName));

                var allusermaudit = oracleDbContext.UserMAuditors.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    UserMAuditor usermaudit = new UserMAuditor();
                    usermaudit = allusermaudit.Where(y => y.UserName == x.UserName).FirstOrDefault();
                    oracleDbContext.UserMAuditors.Remove(usermaudit);
                    
                }
                oracleDbContext.SaveChanges();

                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var item in forInsert)
                {
                    UserMAuditor usermaudit = new UserMAuditor();
                    usermaudit.Id = item.Id;
                    usermaudit.PrefixMCode = item.PrefixMCode;
                    usermaudit.FirstName = item.FirstName;
                    usermaudit.MiddleName = item.MiddleName;
                    usermaudit.LastName = item.LastName;
                    usermaudit.IdenficationNumber = item.IdenficationNumber;
                    usermaudit.CertNo = item.CertNo;
                    usermaudit.StartDate = item.StartDate;
                    usermaudit.ExpireDate = item.ExpireDate;
                    usermaudit.Email = item.Email;
                    usermaudit.Mobile = item.Mobile;
                    usermaudit.ImagePath = item.ImagePath;
                    usermaudit.ImageName = item.ImageName;
                    usermaudit.IsStatus = item.IsStatus;
                    usermaudit.CreateDate = item.CreateDate;
                    usermaudit.CreateBy = item.CreateBy;
                    usermaudit.UpdateDate = item.UpdateDate;
                    usermaudit.UpdateBy = item.UpdateBy;
                    usermaudit.UserName = item.UserName;
                    usermaudit.IsActive = item.IsActive;
                    usermaudit.Birthdate = item.Birthdate;
                    usermaudit.Position = item.Position;
                    await oracleDbContext.UserMAuditors.AddAsync(usermaudit);
                    
                }
                await oracleDbContext.SaveChangesAsync();

                if (setA.Count() == 0)
                {
                    return "success";
                }
                //update 
                foreach (var item in Forupdate)
                {
                    UserMAuditor usermaudit = new UserMAuditor();
                    usermaudit = allusermaudit.Where(x => x.UserName == item.UserName).FirstOrDefault();
                    usermaudit.PrefixMCode = item.PrefixMCode;
                    usermaudit.FirstName = item.FirstName;
                    usermaudit.MiddleName = item.MiddleName;
                    usermaudit.LastName = item.LastName;
                    usermaudit.IdenficationNumber = item.IdenficationNumber;
                    usermaudit.CertNo = item.CertNo;
                    usermaudit.StartDate = item.StartDate;
                    usermaudit.ExpireDate = item.ExpireDate;
                    usermaudit.Email = item.Email;
                    usermaudit.Mobile = item.Mobile;
                    usermaudit.ImagePath = item.ImagePath;
                    usermaudit.ImageName = item.ImageName;
                    usermaudit.IsStatus = item.IsStatus;
                    usermaudit.CreateDate = item.CreateDate;
                    usermaudit.CreateBy = item.CreateBy;
                    usermaudit.UpdateDate = item.UpdateDate;
                    usermaudit.UpdateBy = item.UpdateBy;
                    usermaudit.IsActive = item.IsActive;
                    usermaudit.Birthdate = item.Birthdate;
                    usermaudit.Position = item.Position;
                    oracleDbContext.Update(usermaudit);
                    
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("syncMRoleGroup")]
        public async Task<string> syncMRoleGroup()
        {
            try
            {
                var setA = oracleDbContext.AutMRoleGroups.ToList().Select(x => new BatchSyncMRoleGroupModels
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    RoleCode = x.RoleCode,
                    RoleDesc = x.RoleDesc,
                    IsActive = x.IsActive,
                    UserGroup = x.UserGroup
                });
                var setB = IdpDbContext.RoleGroupMs.ToList().Select(x => new BatchSyncMRoleGroupModels
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    RoleCode = x.RoleCode,
                    RoleDesc = x.RoleDesc,
                    IsActive = x.IsActive,
                    UserGroup = x.UserGroup
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa.RoleCode != sb.RoleCode || sa.IsStatus != sb.IsStatus || sa.Name != sb.Name || sb.RoleDesc != sa.RoleDesc));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.RoleCode != fiu.RoleCode));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.RoleCode != sb.RoleCode));
                var forDelete = setA.Where(sa => setB.All(sb => sa.RoleCode != sb.RoleCode));
                var allrolegroup = oracleDbContext.AutMRoleGroups.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    AutMRoleGroup rolegroup = new AutMRoleGroup();
                    rolegroup = allrolegroup.Where(y => y.RoleCode == x.RoleCode).FirstOrDefault();
                    oracleDbContext.AutMRoleGroups.Remove(rolegroup);
                    
                }
                oracleDbContext.SaveChanges();

                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var x in forInsert)
                {
                    AutMRoleGroup rolegroup = new AutMRoleGroup();
                    rolegroup.Id = x.Id;
                    rolegroup.Name = x.Name;
                    rolegroup.IsStatus = x.IsStatus;
                    rolegroup.CreateDate = x.CreateDate;
                    rolegroup.CreateBy = x.CreateBy;
                    rolegroup.UpdateDate = x.UpdateDate;
                    rolegroup.UpdateBy = x.UpdateBy;
                    rolegroup.RoleCode = x.RoleCode;
                    rolegroup.RoleDesc = x.RoleDesc;
                    rolegroup.IsActive = x.IsActive;
                    rolegroup.UserGroup = x.UserGroup;
                    await oracleDbContext.AutMRoleGroups.AddAsync(rolegroup);
                    
                }
                await oracleDbContext.SaveChangesAsync();
                if (setA.Count() == 0)
                {
                    return "success";
                }
                //update 
                foreach (var x in Forupdate)
                {
                    AutMRoleGroup rolegroup = new AutMRoleGroup();
                    rolegroup = allrolegroup.Where(y => y.RoleCode == x.RoleCode).FirstOrDefault();
                    rolegroup.Name = x.Name;
                    rolegroup.IsStatus = x.IsStatus;
                    rolegroup.CreateDate = x.CreateDate;
                    rolegroup.CreateBy = x.CreateBy;
                    rolegroup.UpdateDate = x.UpdateDate;
                    rolegroup.UpdateBy = x.UpdateBy;
                    rolegroup.RoleDesc = x.RoleDesc;
                    rolegroup.IsActive = x.IsActive;
                    rolegroup.UserGroup = x.UserGroup;
                    oracleDbContext.Update(rolegroup);
                    
                }
                oracleDbContext.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("syncRoleUserMapping")]
        public async Task<string> syncRoleUserMapping()
        {
            try
            {
                var setA = oracleDbContext.AutRoleUserMappings.ToList().Select(x => new BatchSyncRoleUserMapping
                {
                    RoleGroupId = x.RoleGroupId,
                    UserName = x.UserName,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    UserType = x.UserType,
                    IsActive = x.IsActive
                });
                var setB = IdpDbContext.RoleUserMappings.ToList().Select(x => new BatchSyncRoleUserMapping
                {
                    RoleGroupId = x.RoleGroupId,
                    UserName = x.UserName,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    UserType = x.UserType,
                    IsActive = x.IsActive
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa.RoleGroupId != sb.RoleGroupId || sa.UserName != sb.UserName));
                var forDelete = setA.Where(sa => setB.All(sb => sb.RoleGroupId != sa.RoleGroupId || sb.UserName != sa.UserName));

                var allroleUserMapping = oracleDbContext.AutRoleUserMappings.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    AutRoleUserMapping roleUserMapping = new AutRoleUserMapping();
                    roleUserMapping = allroleUserMapping.Where(y => y.RoleGroupId == x.RoleGroupId && y.UserName == x.UserName).FirstOrDefault();
                    oracleDbContext.AutRoleUserMappings.Remove(roleUserMapping);
                }
                oracleDbContext.SaveChanges();

                // insert
                foreach (var x in forInsertAndUpdate)
                {
                    AutRoleUserMapping roleUserMapping = new AutRoleUserMapping();
                    roleUserMapping.RoleGroupId = x.RoleGroupId;
                    roleUserMapping.UserName = x.UserName;
                    roleUserMapping.IsStatus = x.IsStatus;
                    roleUserMapping.CreateDate = x.CreateDate;
                    roleUserMapping.CreateBy = x.CreateBy;
                    roleUserMapping.UpdateDate = x.UpdateDate;
                    roleUserMapping.UpdateBy = x.UpdateBy;
                    roleUserMapping.UserType = x.UserType;
                    roleUserMapping.IsActive = x.IsActive;
                    await oracleDbContext.AutRoleUserMappings.AddAsync(roleUserMapping);
                }
                await oracleDbContext.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [HttpGet("syncMPrefix")]
        public async Task<string> syncMPrefix()
        {
            try
            {
                var setA = oracleDbContext.UserPrefixMs.ToList().Select(x => new BatchSyncMPrefix
                {
                     Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateBy = x.CreateBy,
                    UpdateBy = x.UpdateBy
                });
                var setB = IdpDbContext.PrefixMs.ToList().Select(x => new BatchSyncMPrefix
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateBy = x.CreateBy,
                    UpdateBy = x.UpdateBy

                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa != sb));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.Code != fiu.Code));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.Code != sb.Code));
                var forDelete = setA.Where(sa => setB.All(sb => sa.Code != sb.Code));
                var allusermprefix = oracleDbContext.UserPrefixMs.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    UserPrefixM usermprefix= new UserPrefixM();
                    usermprefix = allusermprefix.Where(y => y.Code == x.Code).FirstOrDefault();
                    oracleDbContext.UserPrefixMs.Remove(usermprefix);
                }
                oracleDbContext.SaveChanges();
                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var item in forInsert)
                {
                    UserPrefixM usermprefix = new UserPrefixM();
                    usermprefix.Id = item.Id;
                    usermprefix.Code = item.Code;
                    usermprefix.Name = item.Name;
                    usermprefix.IsActive = item.IsActive;
                    usermprefix.IsStatus = item.IsStatus;
                    usermprefix.CreateBy = item.CreateBy;
                    usermprefix.UpdateBy = item.UpdateBy;
                    await oracleDbContext.UserPrefixMs.AddAsync(usermprefix);
                }
                await oracleDbContext.SaveChangesAsync();
                if (setA.Count() == 0)
                {
                    return "success";
                }
                //update 
                foreach (var item in Forupdate)
                {
                    UserPrefixM usermprefix = new UserPrefixM();
                    usermprefix = allusermprefix.Where(x => x.Code == item.Code).FirstOrDefault();
                    usermprefix.Name = item.Name;
                    usermprefix.IsActive = item.IsActive;
                    usermprefix.IsStatus = item.IsStatus;
                    usermprefix.CreateBy = item.CreateBy;
                    usermprefix.UpdateBy = item.UpdateBy;
                    oracleDbContext.Update(usermprefix);
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("syncMApp")]
        public async Task<string> syncMApp()
        {
            try
            {
                var setA = oracleDbContext.AutMApps.ToList().Select(x => new BatchSyncAutMApp
                {
                    Id = x.Id,
                    AppCode = x.AppCode,
                    Name = x.Name,
                    SystemDesc = x.SystemDesc,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    Url = x.Url
                });
                var setB = IdpDbContext.RoleAppMs.ToList().Select(x => new BatchSyncAutMApp
                {
                    Id = x.Id,
                    AppCode = x.AppCode,
                    Name = x.Name,
                    SystemDesc = x.SystemDesc,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy,
                    Url = x.Url
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa.AppCode != sb.AppCode || sa.Url != sb.Url || sa.IsStatus != sb.IsStatus || sa.Name != sb.Name));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.AppCode != fiu.AppCode));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.AppCode != sb.AppCode));
                var forDelete = setA.Where(sa => setB.All(sb => sa.AppCode != sb.AppCode));
                var allmapp = oracleDbContext.AutMApps.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    AutMApp mapp = new AutMApp();
                    mapp = allmapp.Where(y => y.AppCode == x.AppCode).FirstOrDefault();
                    oracleDbContext.AutMApps.Remove(mapp);
                }
                oracleDbContext.SaveChanges();
                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var x in forInsert)
                {
                    AutMApp mapp = new AutMApp();
                    mapp.Id = x.Id;
                    mapp.AppCode = x.AppCode;
                    mapp.Name = x.Name;
                    mapp.SystemDesc = x.SystemDesc;
                    mapp.IsActive = x.IsActive;
                    mapp.IsStatus = x.IsStatus;
                    mapp.CreateDate = x.CreateDate;
                    mapp.CreateBy = x.CreateBy;
                    mapp.UpdateDate = x.UpdateDate;
                    mapp.UpdateBy = x.UpdateBy;
                    mapp.Url = x.Url;
                    await oracleDbContext.AutMApps.AddAsync(mapp);
                }
                await oracleDbContext.SaveChangesAsync();
                if (setA.Count() == 0)
                {
                    return "success";
                }
                //update 
                foreach (var x in Forupdate)
                {
                    AutMApp mapp = new AutMApp();
                    mapp = allmapp.Where(y => x.AppCode == y.AppCode).FirstOrDefault();
                    mapp.Name = x.Name;
                    mapp.SystemDesc = x.SystemDesc;
                    mapp.IsActive = x.IsActive;
                    mapp.IsStatus = x.IsStatus;
                    mapp.CreateDate = x.CreateDate;
                    mapp.CreateBy = x.CreateBy;
                    mapp.UpdateDate = x.UpdateDate;
                    mapp.UpdateBy = x.UpdateBy;
                    mapp.Url = x.Url;
                    oracleDbContext.Update(mapp);
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("syncMMenu")]
        public async Task<string> syncMMenu()
        {
            try
            {
                var setA = oracleDbContext.AutMMenus.ToList().Select(x => new BatchSyncAutMMenu
                {
                    Id = x.Id,
                    MenuName = x.MenuName,
                    AppCode = x.AppCode,
                    MenuCode = x.MenuCode,
                    MenuDesc = x.MenuDesc,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy
                });
                var setB = IdpDbContext.RoleMenuMs.ToList().Select(x => new BatchSyncAutMMenu
                {
                    Id = x.Id,
                    MenuName = x.MenuName,
                    AppCode = x.AppCode,
                    MenuCode = x.MenuCode,
                    MenuDesc = x.MenuDesc,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa != sb));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.MenuCode != fiu.MenuCode));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => fi.MenuCode != sb.MenuCode));
                var forDelete = setA.Where(sa => setB.All(sb => sa.MenuCode != sb.MenuCode));

                var allmmenu = oracleDbContext.AutMMenus.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    AutMMenu mmenu = new AutMMenu();
                    mmenu = allmmenu.Where(y => y.MenuCode == x.MenuCode).FirstOrDefault();
                    oracleDbContext.AutMMenus.Remove(mmenu);
                }
                oracleDbContext.SaveChanges();
                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var x in forInsert)
                {
                    AutMMenu mmenu = new AutMMenu();
                    mmenu.Id = x.Id;
                    mmenu.MenuName = x.MenuName;
                    mmenu.AppCode = x.AppCode;
                    mmenu.MenuCode = x.MenuCode;
                    mmenu.MenuDesc = x.MenuDesc;
                    mmenu.IsActive = x.IsActive;
                    mmenu.IsStatus = x.IsStatus;
                    mmenu.CreateDate = x.CreateDate;
                    mmenu.CreateBy = x.CreateBy;
                    mmenu.UpdateDate = x.UpdateDate;
                    mmenu.UpdateBy = x.UpdateBy;
                    await oracleDbContext.AutMMenus.AddAsync(mmenu);
                }
                await oracleDbContext.SaveChangesAsync();
                if (setA.Count() == 0)
                {
                    return "success";
                }
                
                //update 
                foreach (var x in Forupdate)
                {
                    AutMMenu mmenu = new AutMMenu();
                    mmenu = allmmenu.Where(y => x.MenuCode == y.MenuCode).FirstOrDefault();
                    mmenu.MenuName = x.MenuName;
                    mmenu.AppCode = x.AppCode;
                    mmenu.MenuDesc = x.MenuDesc;
                    mmenu.IsActive = x.IsActive;
                    mmenu.IsStatus = x.IsStatus;
                    mmenu.CreateDate = x.CreateDate;
                    mmenu.CreateBy = x.CreateBy;
                    mmenu.UpdateDate = x.UpdateDate;
                    mmenu.UpdateBy = x.UpdateBy;
                    oracleDbContext.Update(mmenu);
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("syncRoleMenu")]
        public async Task<string> syncRoleMenu()
        {
            try
            {
                var setA = oracleDbContext.AutRoleMenus.ToList().Select(x => new BatchSyncAutRoleMenu
                {
                    Id = x.Id,
                    RoleGroupMId = x.RoleGroupMId,
                    RoleMenuMId = x.RoleMenuMId,
                    IsRoleRead = x.IsRoleRead,
                    IsRoleCreate = x.IsRoleCreate,
                    IsRoleUpdate = x.IsRoleUpdate,
                    IsRoleDelete = x.IsRoleDelete,
                    IsRolePrint = x.IsRolePrint,
                    IsRoleApprove = x.IsRoleApprove,
                    IsRoleCancel = x.IsRoleCancel,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy
                });
                var setB = IdpDbContext.RoleGroupListTs.ToList().Select(x => new BatchSyncAutRoleMenu
                {
                    Id = x.Id,
                    RoleGroupMId = x.RoleGroupMId,
                    RoleMenuMId = x.RoleMenuMId,
                    IsRoleRead = x.IsRoleRead,
                    IsRoleCreate = x.IsRoleCreate,
                    IsRoleUpdate = x.IsRoleUpdate,
                    IsRoleDelete = x.IsRoleDelete,
                    IsRolePrint = x.IsRolePrint,
                    IsRoleApprove = x.IsRoleApprove,
                    IsRoleCancel = x.IsRoleCancel,
                    IsActive = x.IsActive,
                    IsStatus = x.IsStatus,
                    CreateDate = x.CreateDate,
                    CreateBy = x.CreateBy,
                    UpdateDate = x.UpdateDate,
                    UpdateBy = x.UpdateBy
                });
                var forInsertAndUpdate = setB.Where(sb => setA.All(sa => sa.RoleMenuMId != sb.RoleMenuMId || sa.RoleGroupMId != sb.RoleGroupMId || sb.IsRoleCancel != sa.IsRoleCancel
                || sb.IsRoleRead != sa.IsRoleRead || sb.IsRoleCreate != sa.IsRoleCreate || sb.IsRoleUpdate != sa.IsRoleUpdate || sa.IsRoleDelete != sb.IsRoleDelete || sb.IsRolePrint != sa.IsRolePrint
                || sb.IsRoleApprove != sa.IsRoleApprove));
                var forInsert = forInsertAndUpdate.Where(fiu => setA.All(sa => sa.RoleMenuMId != fiu.RoleMenuMId || sa.RoleGroupMId != fiu.RoleGroupMId));
                var Forupdate = forInsertAndUpdate.Where(sb => forInsert.All(fi => sb.RoleMenuMId != fi.RoleMenuMId || sb.RoleGroupMId != fi.RoleGroupMId));
                var forDelete = setA.Where(sa => setB.All(sb => sa.RoleMenuMId != sb.RoleMenuMId || sa.RoleGroupMId != sb.RoleGroupMId));

                var allrolemenu = oracleDbContext.AutRoleMenus.ToList();
                //delete
                foreach (var x in forDelete)
                {
                    AutRoleMenu rolemune = new AutRoleMenu();
                    rolemune = allrolemenu.Where(y => y.Id == x.Id).FirstOrDefault();
                    oracleDbContext.AutRoleMenus.Remove(rolemune);
                }
                oracleDbContext.SaveChanges();
                if (setA.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var x in forInsert)
                {
                    AutRoleMenu rolemenu = new AutRoleMenu();
                    rolemenu.Id = x.Id;
                    rolemenu.RoleGroupMId = x.RoleGroupMId;
                    rolemenu.RoleMenuMId = x.RoleMenuMId;
                    rolemenu.IsRoleRead = x.IsRoleRead;
                    rolemenu.IsRoleCreate = x.IsRoleCreate;
                    rolemenu.IsRoleUpdate = x.IsRoleUpdate;
                    rolemenu.IsRoleDelete = x.IsRoleDelete;
                    rolemenu.IsRolePrint = x.IsRolePrint;
                    rolemenu.IsRoleApprove = x.IsRoleApprove;
                    rolemenu.IsRoleCancel = x.IsRoleCancel;
                    rolemenu.IsActive = x.IsActive;
                    rolemenu.IsStatus = x.IsStatus;
                    rolemenu.CreateDate = x.CreateDate;
                    rolemenu.CreateBy = x.CreateBy;
                    rolemenu.UpdateDate = x.UpdateDate;
                    rolemenu.UpdateBy = x.UpdateBy;
                    oracleDbContext.AutRoleMenus.Add(rolemenu);
                }
                oracleDbContext.SaveChanges();
                if (setA.Count() == 0)
                {
                    return "success";
                }

                
                //update 
                foreach (var x in Forupdate)
                {
                    AutRoleMenu rolemenu = new AutRoleMenu();
                    rolemenu = allrolemenu.Where(y => y.Id == x.Id).FirstOrDefault();
                    rolemenu.IsRoleRead = x.IsRoleRead;
                    rolemenu.IsRoleCreate = x.IsRoleCreate;
                    rolemenu.IsRoleUpdate = x.IsRoleUpdate;
                    rolemenu.IsRoleDelete = x.IsRoleDelete;
                    rolemenu.IsRolePrint = x.IsRolePrint;
                    rolemenu.IsRoleApprove = x.IsRoleApprove;
                    rolemenu.IsRoleCancel = x.IsRoleCancel;
                    rolemenu.IsActive = x.IsActive;
                    rolemenu.IsStatus = x.IsStatus;
                    rolemenu.CreateDate = x.CreateDate;
                    rolemenu.CreateBy = x.CreateBy;
                    rolemenu.UpdateDate = x.UpdateDate;
                    rolemenu.UpdateBy = x.UpdateBy;
                    oracleDbContext.Update(rolemenu);
                }
                oracleDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public class MyObjectComparer : IEqualityComparer<BatchsyncHospitalM>
        {
            public bool Equals(BatchsyncHospitalM x, BatchsyncHospitalM y)
            {
                return x.Code == y.Code && x.Name == y.Name;
            }
            public int GetHashCode(BatchsyncHospitalM obj)
            {
                return (obj.Code + obj.Name).GetHashCode();
            }
        }


        [HttpGet("syncHospitalM")]
        public async Task<string> syncHospitalM()
        {
            try
            {
                var setA = oracleDbContext.HosHospitals.ToList().Select(x => new BatchsyncHospitalM
                {
                    Name = x.HospName,
                    Code = x.HospCode9.Trim()
                });

                var setB = PortalDbContext.HospitalMs.ToList().Select(x => new BatchsyncHospitalM
                {
                    Name = x.Name,
                    Code = x.Code.Trim()
                });


                var forInsertAndUpdate = setA.Except(setB, new MyObjectComparer());
                //var checkforInsertAndUpdate = forInsertAndUpdate.Count();
                var forInsert = forInsertAndUpdate.Where(fiu => setB.All(sb =>  sb.Code != fiu.Code));
                var checkforInsert = forInsert.Count();
                var Forupdate = forInsertAndUpdate.Where(fiu => forInsert.All(fi => fi.Code != fiu.Code));
                var checkForupdate = Forupdate.Count();
                //var forDelete = setA.Where(sa => setB.All(sb => sa.Name != sb.Name || sa.Code != sb.Code));

                //delete
                //foreach (var x in forDelete)
                //{
                //    HospitalM hosm = new HospitalM();
                //    hosm = PortalDbContext.HospitalMs.Where(y => y.Code == x.Code || y.Name == x.Name).FirstOrDefault();
                //    PortalDbContext.HospitalMs.Remove(hosm);
                //    PortalDbContext.SaveChanges();
                //}


                if (setB.Count() == 0)
                {
                    forInsert = forInsertAndUpdate;
                }
                // insert
                foreach (var x in forInsert)
                {
                    HospitalM hosm = new HospitalM();
                    hosm.Code = x.Code;
                    hosm.Name = x.Name;
                    hosm.IsActive = true;
                    hosm.IsStatus = 1;
                    hosm.CreateBy = "mms-sync-system";
                    hosm.UpdateBy = "mms-sync-system";
                    hosm.HosType = "1";
                    PortalDbContext.HospitalMs.Add(hosm);
                }
                PortalDbContext.SaveChanges();
                if (setB.Count() == 0)
                {
                    return "success";
                }



                var allhosm = PortalDbContext.HospitalMs.ToList();
                //update 
                foreach (var x in Forupdate)
                {
             
                    
                    
                    HospitalM hosm = new HospitalM();
                    hosm = allhosm.Where(y => y.Code == x.Code).FirstOrDefault();
                    hosm.Name = x.Name;
                    hosm.Code = x.Code;
                    hosm.IsActive = true;
                    hosm.IsStatus = 1;
                    hosm.HosType = "1";
                    hosm.CreateBy = "mms-sync-system";
                    hosm.UpdateBy = "mms-sync-system";
                    PortalDbContext.Update(hosm);
                }
                PortalDbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("ping")]
        public async Task<ActionResult<IEnumerable<PingModels>>> Ping(PingModels data)
        {
            Console.WriteLine("ping pong");
            return Ok(new { status = true, message = "ping success" });
        }

    }
}


