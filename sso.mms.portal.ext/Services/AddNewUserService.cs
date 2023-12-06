using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.notification.ViewModel;
using sso.mms.portal.admin.Pages.EditBanner;
using System.Security.Cryptography;
using sso.mms.portal.ext.ViewModels;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using sso.mms.login.Services;
using sso.mms.login.ViewModels.KeyCloak;
using sso.mms.login.Interface;
using sso.mms.login.ViewModels.Email;
using Microsoft.JSInterop;
using sso.mms.helper.Utility;
using sso.mms.login.ViewModels;
using System.Diagnostics;

namespace sso.mms.portal.ext.Services
{
    public class AddNewUserService
    {
        
        private readonly HttpClient httpClient;

        public string? SecretKey = ConfigureCore.SecretKey;
        public ResponseUpload responseUpload;
        private readonly KeyCloakService keyCloakService;
        private readonly UserService UserService;
        
        public AddNewUserService(HttpClient httpClient, KeyCloakService keyCloakService, UserService UserService)
        {

            this.httpClient = httpClient;
            this.keyCloakService = keyCloakService;
            this.UserService = UserService;
            
        }

        public async Task<HospitalUserM> GetHospitalUser(int? id)
        {
            try
            {
                var response = await httpClient.GetAsync("api/addnewuser/gethospitaluserbyid/" + $"{id}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<HospitalUserM>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HospitalUserM> gethospitaluserbyusername(string? id)
        {
            try
            {
                var response = await httpClient.GetAsync("api/addnewuser/gethospitaluserbyusername/" + $"{id}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<HospitalUserM>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<HospitalUserM>> gethospitaluserbyusernameList(string? username , int? hospitalmId)
        {
            try
            {
                var response = await httpClient.GetAsync("api/addnewuser/getHospitalUserByUsernameListAll/" + $"{username}"  + "/" + $"{hospitalmId}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<HospitalUserM>>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<string> updateHospitalM(HospitalUserM data,IBrowserFile file)
        {
            ResponseUpload resimg = new ResponseUpload();
            try
            {
                if (file != null)
                {
                    resimg = await UploadFile(file);
                }
                else
                {
                    resimg = new ResponseUpload();
                }
                data.ImageName = resimg.FileName;
                data.ImagePath = resimg.Path_Url;
                var response = await httpClient.PostAsJsonAsync("api/addnewuser/updatehospitaluser", data);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return "success";
                }
                else
                {
                    return "dontsuccess";
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<HospitalUserM>> getUserbyTypeHosIdAll(int? hosid, int adminid)
        {
            var response = await httpClient.GetAsync("api/addnewuser/getUserbyTypeHosIdAll/" + $"{hosid}" + "/" + $"{adminid}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<HospitalUserM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<HospitalUserM>> GetUserHos(int? hosid, int adminid)
        {
            var response = await httpClient.GetAsync("api/addnewuser/getUserbyTypeHosId/" + $"{hosid}" + "/" + $"{adminid}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<HospitalUserM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RoleGroupM>> GetRoleGroupMByType(string text)
        {
            var response = await httpClient.GetAsync("api/addnewuser/getrolegroupmbytype/" + $"{text}");
            response.EnsureSuccessStatusCode();
            try
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<RoleGroupM>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> insertRoleUserMapping(List<RoleUserMapping> data, string? username)
        {

            try
            {
                var response1 = await httpClient.GetAsync("api/addnewuser/deleteroleusermapping/" + $"{username}");
                response1.EnsureSuccessStatusCode();

                foreach (var item in data) 
                {
                    ViewModelMapRoleUserMApping item1 = new ViewModelMapRoleUserMApping();
                    item1.RoleGroupId = item.RoleGroupId;
                    item1.UserName = username;
                    item1.UpdateBy = item.UpdateBy;
                    item1.CreateBy = item.CreateBy;
                    if (item.IsActive == null)
                    {
                        item1.IsActive = false;
                    }
                    else {
                        item1.IsActive = item.IsActive;
                    }
                    item1.UserType  = item.UserType;


                    var response = await httpClient.PostAsJsonAsync("api/addnewuser/insertroleusermapping", item1);
                    response.EnsureSuccessStatusCode();
                }
               
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<RoleUserMapping>> getRoleUserMapping(string username)
        {

            try
            {
                var response = await httpClient.GetAsync("api/addnewuser/getroleusermapping/" + $"{username}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<RoleUserMapping>>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<HospitalM> getHospitalCode(int? hospitalid)
        {

            try
            {
                var response = await httpClient.GetAsync("api/addnewuser/gethospitalcode/" + $"{hospitalid}");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<HospitalM>(result);
                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<string> addNewHosUser(HospitalUserM? data, int? hnid, string hospitalcode, int adminid,IBrowserFile file)
        {
            ResponseUpload resimg = new ResponseUpload();
            try
            {
                if (file != null)
                {
                     resimg = await UploadFile(file);
                }
                else
                {
                     resimg = new ResponseUpload();
                }


                // hospital name by hospitalcode
                var reshoshname = await httpClient.GetAsync("api/addnewuser/getHospitalNameByCode/" + $"{hospitalcode}");
                reshoshname.EnsureSuccessStatusCode();
                var result = reshoshname.Content.ReadAsStringAsync().Result;
                //var hospitalname = JsonConvert.DeserializeObject<string>(result);


                // get Id for username and genusername
                List<HospitalUserM> userlist = await getUserbyTypeHosIdAll(hnid, adminid);
                int idusernamesave = 0;
                if (userlist.Count() == 0)
                {
                    idusernamesave = 1;
                }
                else {
                    idusernamesave = userlist.Count() + 1;
                }
                string usernameForSaveUser = hospitalcode.Trim() + ".user" + idusernamesave;

                // check usrname not exit in hospital
                List<HospitalUserM>  checkuer = await gethospitaluserbyusernameList(usernameForSaveUser, data.HospitalMId);
                if (checkuer.Count() > 0)
                {
                    return "usernameisnotexit";
                }
                else {
                    // generate password
                    byte[] toBytes = Encoding.UTF8.GetBytes(usernameForSaveUser);
                    byte[] hashBytes;
                    using (MD5 md5 = MD5.Create())
                    {
                        hashBytes = md5.ComputeHash(toBytes);
                    }
                    string toString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                    string firstpass = toString.Substring(0, 7);
                    Console.Write(firstpass);

                    // generate token
                    ResponseCreateTokenKeyCloak token = await keyCloakService.CreateTokenKeyCloak("sso-mms-hospital");

                    //send email
                    RequestEmail emailsend = new RequestEmail();
                    emailsend.ToEmail = data.Email;
                    emailsend.SubjectEmail = "sendemail";
                    emailsend.Username = usernameForSaveUser;
                    emailsend.Password = firstpass;
                    await UserService.SendMailAsync(emailsend);

                    // create user keycloak and postgres
                    data.UserName = usernameForSaveUser;
                    data.Password = firstpass;
                    data.IsStatus = 1;
                    data.ImageName = resimg.FileName;
                    data.ImagePath = resimg.Path_Url;
                    data.MedicalCode = hospitalcode;
                    data.MedicalName = result;
                    ResponseModel res = await keyCloakService.CreateUserKeyCloak(data,null,token.access_token, "sso-mms-hospital");
                    if ((bool)(res?.issucessStatus))
                    {
                        return usernameForSaveUser;
                    }
                    else 
                    {
                        return "fail";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return "fail";
            }
        }



        public async Task<string> changepassword(HospitalUserM userhosdata)
        {
            try
            {
                var decryptedOldPassword = AesOperation.DecryptString(SecretKey, userhosdata.Password);
                string data = userhosdata.IdenficationNumber + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                byte[] toBytes = Encoding.UTF8.GetBytes(data);
                byte[] hashBytes;
                using (MD5 md5 = MD5.Create())
                {
                    hashBytes = md5.ComputeHash(toBytes);
                }
                string toString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                string firstpass = toString.Substring(0, 7);

                ResponseChangePasswordModel response = await UserService.ChangePassword(firstpass, userhosdata.UserName, decryptedOldPassword, userhosdata.Id, "sso-mms-hospital");
                if (response.isStatus)
                {
                    var senddata = new RequestEmail
                    {
                        ToEmail = userhosdata.Email,
                        SubjectEmail = "Reset PassWord Success ",
                        Password = firstpass,
                        Username = userhosdata.UserName
                    };
                    var mailresponse = await UserService.SendMailAsync(senddata);

                    if ((bool)mailresponse.issucessStatus)
                    {
                        return "changepasswordsuccess";
                    }
                    else
                    {
                        return "changepasswordnotsuccess";
                    }
                }
                else {
                    return "changepasswordnotsuccess";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ResponseModel> changeStatusToDelete(int userid)
        {
            try
            {
                ResponseModel res = new ResponseModel();
                var response = await httpClient.GetAsync("api/addnewuser/changestautsdelete/" + $"{userid}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    res.issucessStatus = true;
                    return res;
                }
                else {
                    res.issucessStatus = false;
                    return res;
                }
            }
            catch (Exception ex)
            {
                ResponseModel res = new ResponseModel();
                res.issucessStatus = false;
                return res;
            }
        }


        public async Task<List<PrefixM>> getPrefixs()
        {
            try
            {
                var response = await httpClient.GetAsync("api/addnewuser/getprefix");
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var LogList = JsonConvert.DeserializeObject<List<PrefixM>>(result);

                return LogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseUpload> UploadFile(IBrowserFile file)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        var fileContent = new StreamContent(fileStream);
                        formData.Add(fileContent, "file", file.Name);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var response = await httpClient.PostAsync("api/addnewuser/UploadImage", formData);
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;

                            var responseUpload = JsonConvert.DeserializeObject<ResponseUpload>(result);

                            var success = new ResponseUpload
                            {
                                FileName = responseUpload?.FileName,
                                Path_Url = responseUpload?.Path_Url,
                            };

                            return success;
                        }
                        else
                        {
                            var fail = new ResponseUpload
                            {
                                FileName = "",
                                Path_Url = "",
                            };
                            return fail;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var fail = new ResponseUpload
                {
                    Error = ex.Message,
                    FileName = "",
                    Path_Url = "",
                };
                return fail;
            }

        }

    }
}
