using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.KeyCloak;
using System;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static sso.mms.login.ViewModels.KeyCloak.CreateUserTokenKeyCloak;
using sso.mms.helper.ViewModels;
using System.Text;
using Microsoft.EntityFrameworkCore;
using sso.mms.login.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sso.mms.helper.Utility;
using Newtonsoft.Json.Linq;
using MatBlazor;
using Attributes = sso.mms.login.ViewModels.KeyCloak.CreateUserTokenKeyCloak.Attributes;
using sso.mms.login.ViewModels.User;
using System.Net.Http;
using sso.mms.helper.PortalModel;
using System.Collections.Generic;
using sso.mms.helper;


namespace sso.mms.login.Services
{
    public class KeyCloakService : IKeyCloakService
    {
        public string? SecretKey = ConfigureCore.SecretKey;
        private readonly HttpClient httpClient;
        private string adminUrl = "";
        private string loginUrl = "";
        private readonly IdpDbContext db;
        private readonly UserRoleService userRoleService;

        public string dataDictionary;
        public KeyCloakService(HttpClient httpClient, IdpDbContext idpDbContext)
        {
            this.httpClient = httpClient;
            httpClient.Timeout = TimeSpan.FromMilliseconds(300000);

            adminUrl = ConfigureCore.baseAddressPortalAdmin;
            loginUrl = ConfigureCore.baseAddressLogin;
            this.db = idpDbContext;
        }


        public string CheckSecretRealmGroup(string realmGroup)
        {
            switch (realmGroup)
            {
                case "sso-mms-hospital":
                    return ConfigureCore.secretKeyRealmGroupHospital;

                case "sso-mms-admin":
                    return ConfigureCore.secretKeyRealmGroupAdmin;

                case "sso-mms-auditor":
                    return ConfigureCore.secretKeyRealmGroupAuditor;
            }
            return "";
        }
        public async Task<ResponseCreateTokenKeyCloak> CreateTokenKeyCloak(string realmGroup)
        {

            var data = new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"client_id", $"client-{realmGroup}"},
                {"client_secret", CheckSecretRealmGroup(realmGroup)}
            };

            var content = new FormUrlEncodedContent(data);

            var response = await httpClient.PostAsync($"realms/{realmGroup}/protocol/openid-connect/token", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseCreateTokenKeyCloak>(responseContent)!;

        }

        public async Task<Response<UserKeycloak>> GetUserKeyCloak()
        {

            try
            {
                var getToken = await CreateTokenKeyCloak("sso-mms-hospital");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", getToken.access_token);
                var response = await httpClient.GetFromJsonAsync<List<UserKeycloak>>($"admin/realms/sso-mms-hospital/users");

                if (response != null)
                {
                    var responseSuccess = new Response<UserKeycloak>();
                    responseSuccess.StatusCode = 200;
                    responseSuccess.Message = "get user keycloak successfully";
                    responseSuccess.ResultList = response;

                    return responseSuccess;
                }
                else
                {
                    var responseError = new Response<UserKeycloak>();
                    responseError.StatusCode = 400;
                    responseError.Message = "get user keycloak not found";
                    responseError.ResultList = response;

                    return responseError;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                var responseError = new Response<UserKeycloak>();
                responseError.Message = errorMessage;
                responseError.StatusCode = 400;
                responseError.ResultList = null;

                return responseError;
            }
        }

        public async Task<Response<UserKeycloak>> GetUserByIdKeycloak(string Id)
        {
            try
            {
                var getToken = await CreateTokenKeyCloak("sso-mms-hospital");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", getToken.access_token);
                var response = await httpClient.GetFromJsonAsync<UserKeycloak>($"admin/realms/sso-mms-hospital/users/{Id}");

                if (response != null)
                {
                    var responseSuccess = new Response<UserKeycloak>();
                    responseSuccess.StatusCode = 200;
                    responseSuccess.Message = "Get UserById Keycloak successfully";
                    responseSuccess.Result = response;

                    return responseSuccess;
                }
                else
                {
                    var responseSuccess = new Response<UserKeycloak>();
                    responseSuccess.StatusCode = 400;
                    responseSuccess.Message = "Get UserById Keycloak fail";
                    responseSuccess.Result = response;

                    return responseSuccess;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                var responseError = new Response<UserKeycloak>();
                responseError.Message = errorMessage;
                responseError.StatusCode = 400;
                responseError.Result = null;

                return responseError;
            }


        }

        public async Task<ResponseModel> DeleteUserKeycloak(int hospitalUserMId,string username)
        {
            try
            {
                var getUserKeyCloak = await GetUserKeyCloak();

                var findUserByUsername = getUserKeyCloak.ResultList.Find(x => x.username == username);

                if (findUserByUsername != null)
                {
                    var getToken = await CreateTokenKeyCloak("sso-mms-hospital");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", getToken.access_token);
                    var response = await httpClient.DeleteAsync($"admin/realms/sso-mms-hospital/users/{findUserByUsername.id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseSuccess = new ResponseModel();
                        responseSuccess.issucessStatus = true;
                        responseSuccess.statusMessage = "Delete UserById Keycloak successfully";
                        responseSuccess.statusCode = "200";
                        RequestUserHospitalMId iduser = new RequestUserHospitalMId();
                        iduser.Id = hospitalUserMId;
                        var deleteHospitalUser = await httpClient.PostAsJsonAsync("/api/User/deleteUserHospital", iduser);

                        return responseSuccess;
                    }
                    else
                    {
                        var responseError = new ResponseModel();
                        responseError.issucessStatus = true;
                        responseError.statusMessage = "Delete UserById Keycloak fail";
                        responseError.statusCode = "400";
                        return responseError;
                    }
                }
                else
                {
                    var responseError = new ResponseModel();
                    responseError.issucessStatus = false;
                    responseError.statusMessage = "Not Found Username";
                    responseError.statusCode = "400";
                    return responseError;
                }
            }
            catch (Exception ex)
            {
                var responseError = new ResponseModel();
                responseError.issucessStatus = false;
                responseError.statusMessage = ex.Message;
                responseError.statusCode = "400";
                return responseError;
            }
        }

        private string GenShortToken(string accessToken)
        {
            Guid GUID = Guid.NewGuid();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(accessToken);
            byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);
            return GUID + "-" + Convert.ToHexString(hashBytes);
        }

        public async Task<ResponseLoginTokenKeyCloak> LoginKeyCloak(string username, string password, string realmGroup)
        {
            try
            {
                Console.WriteLine("username : {0}, password : {1}, realm group : {2}", username, password, realmGroup);
                Dictionary<string, string> data;

                if (realmGroup == "sso-mms-hospital")
                {
                    data = new Dictionary<string, string>
                    {
                    {"grant_type", "password"},
                    {"client_id", $"client-{realmGroup}"},
                    {"client_secret", CheckSecretRealmGroup(realmGroup)},
                    {"username", $"{username}"},
                    {"password", $"{password}"},
                    {"scope", "identification_number OrganizeId UserId"}
                    };
                }
                else if(realmGroup == "sso-mms-admin")
                {
                    data = new Dictionary<string, string>
                    {
                    {"grant_type", "password"},
                    {"client_id", $"client-{realmGroup}"},
                    {"client_secret", CheckSecretRealmGroup(realmGroup)},
                    {"username", $"{username}"},
                    {"password", $"{password}"},
                    {"scope", "identification_number OrganizeId UserId"}
                    };
                }
                else
                {
                    data = new Dictionary<string, string>
                    {
                    {"grant_type", "password"},
                    {"client_id", $"client-{realmGroup}"},
                    {"client_secret", CheckSecretRealmGroup(realmGroup)},
                    {"username", $"{username}"},
                    {"password", $"{password}"},
                    };
                }


                var content = new FormUrlEncodedContent(data);
                Console.WriteLine("data {0}", data);
                var response = await httpClient.PostAsync($"realms/{realmGroup}/protocol/openid-connect/token", content);
                Console.WriteLine("response {0}", JsonConvert.SerializeObject(response));

                if (response.IsSuccessStatusCode)
                {
                    var resLoginSuccess = await response.Content.ReadAsStringAsync();
                    var convertRes = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(resLoginSuccess);

                    var stream = $"{convertRes.access_token}";
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(stream) as JwtSecurityToken;

                    CreateLoginKeyCloak createLogin = new CreateLoginKeyCloak();
                    createLogin.username = username;
                    createLogin.password = password;
                    createLogin.access_token = convertRes.access_token;
                    createLogin.expires_in = convertRes.expires_in;
                    createLogin.refresh_expires_in = convertRes.expires_in;
                    createLogin.refresh_token = convertRes.refresh_token;
                    createLogin.token_type = convertRes.token_type;
                    createLogin.idp_token = resLoginSuccess;
                    createLogin.realmGroup = realmGroup;


                    if (realmGroup == "sso-mms-hospital")
                    {
                        createLogin.ChannelLogin = "Portal รพ.";
                    }
                    else
                    {
                        createLogin.ChannelLogin = "Portal สปส.";
                    }


                    var responseLoginApi = await httpClient.PostAsJsonAsync($"{loginUrl}api/Login", createLogin);
                    Console.WriteLine(JsonConvert.SerializeObject(responseLoginApi));
                    if (responseLoginApi.IsSuccessStatusCode)
                    {
                        var token = responseLoginApi.Content.ReadAsStringAsync();
                        var value = JsonConvert.DeserializeObject<ResponseShortToken>(token.Result);

                        convertRes.shortToken = value.shortToken!;
                        convertRes.access_token = value.accessToken;
                        convertRes.refresh_token = convertRes.refresh_token;
                        convertRes.realmGroup = realmGroup;
                        convertRes.responseUser = value.responseUser;

                    }
                    return convertRes;
                }
                return null!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
        public async Task<ResponseModel> CreateUserKeyCloak(HospitalUserM? hospitalUser, AuditorUserM? auditorUser, string token, string realmGroup)
        {
            ResponseModel responseModel = new ResponseModel();
            var body = new CreateUserTokenKeyCloak();
            if (realmGroup == "sso-mms-hospital")
            {
                body.email = hospitalUser.Email;
                body.firstName = hospitalUser.FirstName;
                body.lastName = hospitalUser.LastName;
                body.enabled = true;
                body.credentials = new List<Credential>
                {
                      new Credential { type = "password",value = hospitalUser.Password,temporary = false}
                };
                body.attributes = new Attributes
                {
                    identification_number = hospitalUser.IdenficationNumber,
                    OrganizeId = "1",
                    //userId =  
                    
                };
                body.username = hospitalUser.UserName!;
            }
            else if (realmGroup == "sso-mms-admin")
            {
                body.email = auditorUser.Email;
                body.firstName = auditorUser.FirstName;
                body.lastName = auditorUser.LastName;
                body.enabled = true;
                body.credentials = new List<Credential>
                {
                      new Credential { type = "password",value = auditorUser.Password,temporary = false}
                };
                body.attributes = new Attributes
                {
                    OrganizeId = "1",
                };
                body.username = auditorUser.UserName!;
            }
            else
            {
                body.email = auditorUser.Email;
                body.firstName = auditorUser.FirstName;
                body.lastName = auditorUser.LastName;
                body.enabled = true;
                body.credentials = new List<Credential>
                {
                      new Credential { type = "password",value = auditorUser.Password,temporary = false}
                };
                body.username = auditorUser.UserName!;
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsJsonAsync($"admin/realms/{realmGroup}/users", body);

            if (response.IsSuccessStatusCode)
            {
                if (realmGroup == "sso-mms-hospital")
                {
                    // hashPassword
                    var encryptedString = AesOperation.EncryptString(SecretKey, hospitalUser.Password);
                    //Console.WriteLine($"decrypted string = {decryptedString}");
                    hospitalUser.Password = encryptedString;
                    var createUserToDB = await httpClient.PostAsJsonAsync($"{loginUrl}api/Register", hospitalUser);

                    if(hospitalUser.UserName.Split('.').Last() == "admin")
                    {
                        RoleUserMappingModel roleInsertHOSADMIN = new RoleUserMappingModel
                            {
                            RoleGroupId = 28,
                            UserName = hospitalUser.UserName,
                            UserType = "HOS",
                            CreateBy = hospitalUser.UserName,
                            UpdateBy = hospitalUser.UserName
                        };
                        RoleUserMappingModel roleInsertHOSPITAL = new RoleUserMappingModel
                        {
                            RoleGroupId = 118,
                            UserName = hospitalUser.UserName,
                            UserType = "HOS",
                            CreateBy = hospitalUser.UserName,
                            UpdateBy = hospitalUser.UserName
                        };
                        try
                        {

                            var insertRoleAdmin = await httpClient.PostAsJsonAsync($"{loginUrl}api/userrole/insertRoleUserMapping", roleInsertHOSADMIN);
                            var insertRoleHos = await httpClient.PostAsJsonAsync($"{loginUrl}api/userrole/insertRoleUserMapping", roleInsertHOSPITAL);
                            if (insertRoleHos.IsSuccessStatusCode)
                            {
                                // sync role user mapping
                                var responseusermapping = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncRoleUserMapping");
                                Console.WriteLine(responseusermapping);

                            }
                            response.EnsureSuccessStatusCode();
                            Console.WriteLine("insert success");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    var result = createUserToDB.Content.ReadAsStringAsync();

                }
                else
                {
                    var encryptedString = AesOperation.EncryptString(SecretKey, auditorUser.Password);
                    //Console.WriteLine($"decrypted string = {decryptedString}");
                    auditorUser.Password = encryptedString;
                    var createUserToDB = await httpClient.PostAsJsonAsync(adminUrl + "api/audit/AddAudit", auditorUser);

                    var result = createUserToDB.Content.ReadAsStringAsync();
                }
                responseModel = new ResponseModel
                {
                    issucessStatus = true,
                    statusCode = "200",
                    statusMessage = "Success !"
                };
                return responseModel;
            }
            responseModel = new ResponseModel
            {
                issucessStatus = false,
                statusCode = "400",
                statusMessage = "Error !"
            };
            return responseModel;
        }
        public async Task<ActionResult<ResponseLoginTokenKeyCloak>> RefreshToken(string refreshToken, string realmGroup)
        {
            var data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"client_id", $"client-{realmGroup}"},
                {"client_secret", CheckSecretRealmGroup(realmGroup)},
                {"refresh_token",$"{refreshToken}" }
            };

            var content = new FormUrlEncodedContent(data);

            var response = await httpClient.PostAsync($"realms/${realmGroup}/protocol/openid-connect/token", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                var responseIdPToken = await httpClient.PostAsJsonAsync($"{loginUrl}api/Login/getTokenByRefreshToken", refreshToken);

                var resultIdPToken = responseIdPToken.Content.ReadAsStringAsync().Result;
                var convertToJson = JsonConvert.DeserializeObject<ResponseRefreshToken>(resultIdPToken);
                //convertToJson.shortToken = GenShortToken(convertToJson!.accessToken!);

                var responseMessage = await httpClient.PostAsJsonAsync($"{loginUrl}api/Login/saveRefreshToken", convertToJson);

                //return result;
            }

            return JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(responseContent)!;
        }
        public async Task<ResponseModel> CheckUserName(string username)
        {
            var resultResponse = new ResponseModel();
            try
            {

                var getToken = await CreateTokenKeyCloak("sso-mms-hospital");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", getToken.access_token);
                var response = await httpClient.GetFromJsonAsync<List<getUserKeyCloakModel>>($"admin/realms/sso-mms-hospital/users");

                var getUser = (from userid in response where userid.username == username select userid).FirstOrDefault();
                if (getUser != null)
                {
                    resultResponse = new ResponseModel()
                    {
                        issucessStatus = false,
                        statusCode = "400",
                        statusMessage = "Already have an account!"

                    };
                    return resultResponse;
                }
                else
                {
                    resultResponse = new ResponseModel()
                    {
                        issucessStatus = true,
                        statusCode = "200",
                        statusMessage = "Success!"

                    };
                    return resultResponse;
                }

            }
            catch (Exception ex)
            {
                resultResponse = new ResponseModel()
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = ex.Message

                };
                return resultResponse;
            }

        }
        public async Task<HospitalM> CheckHospCode(string hospcode)
        {

            var resultResponse = new ResponseModel();
            try
            {
                var response = await httpClient.GetAsync($"{loginUrl}api/register/checkHospCode");
                var strRes = response.Content.ReadAsStringAsync().Result;
                if (response != null)
                {
                    var convertToJson = JsonConvert.DeserializeObject<List<HospitalM>>(strRes);
                    
                    HospitalM result = convertToJson.FirstOrDefault(w => w.IsActive == true && w.Code == hospcode);
                    if (result == null)
                    {
                    HospitalM List = convertToJson.FirstOrDefault(w => w.IsActive == true && w.Id == int.Parse(hospcode));
                    return List;

                    }
                    return result;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<HospitalM>> GetHospital()
        {
            var resultResponse = new ResponseModel();
            try
            {
                var response = await httpClient.GetAsync($"{loginUrl}api/register/checkHospCode");
                var strRes = response.Content.ReadAsStringAsync().Result;
                if (response != null)
                {
                    var convertToJson = JsonConvert.DeserializeObject<List<HospitalM>>(strRes);
                    return convertToJson;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseModel> GetHospId(string hospcode)
        {
            var resultResponse = new ResponseModel();
            try
            {
                var response = await httpClient.GetAsync($"{loginUrl}api/register/checkHospCode");
                var strRes = response.Content.ReadAsStringAsync().Result;
                var convertToJson = JsonConvert.DeserializeObject<List<HospitalM>>(strRes);
                HospitalM result = convertToJson.FirstOrDefault(w => w.IsActive == true && w.Code == hospcode);
                resultResponse = new ResponseModel()
                {
                    issucessStatus= true,
                    statusCode ="200",
                    statusMessage= result.Id.ToString()
                };
                return resultResponse;
             

            }
            catch (Exception ex)
            {
                resultResponse = new ResponseModel()
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = ex.Message
                };
                return resultResponse;
            }
            
        }
        public async Task<string> AddHospData(HospitalM hospData)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var responseAdd = await httpClient.PostAsJsonAsync($"{loginUrl}api/register/addHospital", hospData);
                if(responseAdd.IsSuccessStatusCode)
                {
                    return "บันทึก สำเร็จ";
                }
                else
                {
                    return "บันทึก ไม่สำเร็จ";
                }
               
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}