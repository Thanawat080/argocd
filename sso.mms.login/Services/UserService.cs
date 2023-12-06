using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.Utility;
using sso.mms.helper.ViewModels;
using sso.mms.login.Interface;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.Email;
using sso.mms.login.ViewModels.KeyCloak;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AntDesign;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components;

namespace sso.mms.login.Services
{
    public class UserService : IUserService
    {
        public string? env = ConfigureCore.ConfigENV;
        public string? SecretKey = ConfigureCore.SecretKey;

        private readonly HttpClient httpClient;
        private readonly IdpDbContext db;
        private readonly KeyCloakService keyCloakService;
        private readonly ILocalStorageService _localStorage;
        public UserService(HttpClient httpClient,
            IdpDbContext idpDbContext,
            KeyCloakService keyCloakService,
            ISessionStorageService sessionStorage,
            ILocalStorageService localStorage
            )
        {
            this.httpClient = httpClient;
            this.db = idpDbContext;
            this.keyCloakService = keyCloakService;
            _localStorage = localStorage;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {

            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixEpoch.AddSeconds(unixTimeStamp);
        }

        public async Task<bool> CheckSessionRedirectLogin()
        {

            var session = await _localStorage.GetItemAsync<ResponseLoginTokenKeyCloak>("SessionUser");

            //mock access_token
            if (session != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(session.access_token) as JwtSecurityToken;
                IEnumerable<Claim> claims = jwtToken!.Claims;
                var exp = claims.FirstOrDefault(x => x.Type == "exp")?.Value;
                DateTime accessTokenExpirationDateTime = UnixTimeStampToDateTime(int.Parse(exp));
                DateTime currentDateTime = DateTime.UtcNow;

                Console.WriteLine(currentDateTime);
                if (currentDateTime > accessTokenExpirationDateTime)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task CheckSessionUserStorage()
        {

            var session = await _localStorage.GetItemAsync<ResponseLoginTokenKeyCloak>("SessionUser");

            //mock access_token
            if (session != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(session.access_token) as JwtSecurityToken;
                IEnumerable<Claim> claims = jwtToken!.Claims;
                var exp = claims.FirstOrDefault(x => x.Type == "exp")?.Value;
                DateTime accessTokenExpirationDateTime = UnixTimeStampToDateTime(int.Parse(exp));
                DateTime currentDateTime = DateTime.UtcNow;

                Console.WriteLine(currentDateTime);
                if (currentDateTime > accessTokenExpirationDateTime)
                {

                    //send to refreshToken
                    await RefreshTokenForSessionStorage(session.refresh_token!, session.realmGroup!);

                }
            }
        }

        public async Task RefreshTokenForSessionStorage(string refresh_token, string realmGroup)
        {

            RequestRefreshToken refreshTokenObject = new RequestRefreshToken();

            refreshTokenObject.refreshToken = refresh_token;
            refreshTokenObject.realmGroup = realmGroup;

            var response = await httpClient.PostAsJsonAsync($"{ConfigureCore.baseAddressLogin}api/Login/refreshToken", refreshTokenObject);

            if (response.IsSuccessStatusCode == true)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var convertJson = JsonConvert.DeserializeObject<ResponseShortToken>(responseContent);

                ResponseLoginTokenKeyCloak responseLoginTokenKeyCloak = new ResponseLoginTokenKeyCloak();

                responseLoginTokenKeyCloak.access_token = convertJson.accessToken;
                responseLoginTokenKeyCloak.refresh_token = convertJson.refreshToken;
                responseLoginTokenKeyCloak.realmGroup = convertJson.realmGroup;

                await _localStorage.SetItemAsync("shortToken", convertJson.shortToken);

            }
        }

        public async Task<dynamic> GetUserById(int id, string rleamGroup)
        {
            try
            {
                var response = await httpClient.GetAsync("api/user/GetUserById?id=" + $"{id}&rleamGroup={rleamGroup}");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    if (rleamGroup == "sso-mms-hospital")
                    {
                        var convertRes = JsonConvert.DeserializeObject<HospitalUserM>(result.Result);
                        return convertRes;
                    }
                    else
                    {
                        var convertRes = JsonConvert.DeserializeObject<AuditorUserM>(result.Result);
                        return convertRes;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<ResponseModel> checkRegis(string moblie, string Iden)
        {
            var resultResponse = new ResponseModel();
            try
            {
                var response = await httpClient.GetAsync("api/user/GetUser");
                var result = response.Content.ReadAsStringAsync();
                var convertRes = JsonConvert.DeserializeObject<List<HospitalUserM>>(result.Result);
                var getmoblie = (from x in convertRes where x.Mobile == moblie select x).FirstOrDefault();
                var getIden = (from x in convertRes where x.IdenficationNumber == Iden select x).FirstOrDefault();
                if (getmoblie != null || getIden != null)
                {
                    resultResponse = new ResponseModel()
                    {
                        issucessStatus = false,
                        statusCode = "400",
                        statusMessage = "มีเบอร์โทรศัพท์ หรือ เลขบัตรประจำตัวประชาชนในระบบ"

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
        public async Task<HospitalUserM> GetUserByEmail(string email)
        {
            try
            {
                var response = await httpClient.GetAsync("api/user/GetUserByEmail?email=" + $"{email}");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();

                    var convertRes = JsonConvert.DeserializeObject<HospitalUserM>(result.Result);

                    return convertRes;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public async Task<ResponseChangePasswordModel> ChangePassword(string newPassword, string userName, string oldPassword, int userId, string realmGroup)
        {

            var client_secret = keyCloakService.CheckSecretRealmGroup(realmGroup);
            try
            {
                var data = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"client_id", $"client-{realmGroup}"},
                    {"client_secret", $"{client_secret}"},
                    {"username", $"{userName}"},
                    {"password", $"{oldPassword}"}
                };
                var content = new FormUrlEncodedContent(data);
                var response = await httpClient.PostAsync($"{ConfigureCore.baseAddressIdpKeycloak}realms/{realmGroup}/protocol/openid-connect/token", content);

                Console.WriteLine("response.Content", response);

                if (response.IsSuccessStatusCode)
                {
                    var resLoginSuccess = await response.Content.ReadAsStringAsync();

                    var convertRes = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(resLoginSuccess);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", convertRes.access_token);
                    var userResponse = await httpClient.GetAsync($"{ConfigureCore.baseAddressIdpKeycloak}admin/realms/{realmGroup}/users");

                    Console.WriteLine("response.userResponse", response);

                    if (userResponse.IsSuccessStatusCode)
                    {
                        var resGetUserResponse = await userResponse.Content.ReadAsStringAsync();
                        var userConvertres = JsonConvert.DeserializeObject<List<getUserKeyCloakModel>>(resGetUserResponse);
                        var getUser = (from userid in userConvertres where userid.username == userName select userid).FirstOrDefault();

                        if (getUser != null)
                        {
                            var putDataContent = new StringContent(JsonConvert.SerializeObject(
                                new
                                {
                                    type = "password",
                                    value = newPassword,
                                    temporary = false
                                }),
                                Encoding.UTF8,
                                "application/json"
                                );

                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", convertRes.access_token);
                            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var putDataToUpdate = await httpClient.PutAsync($"{ConfigureCore.baseAddressIdpKeycloak}admin/realms/{realmGroup}/users/{getUser.id.Trim('"')}/reset-password", putDataContent);
                            if (putDataToUpdate.IsSuccessStatusCode)
                            {
                                var changePasswordContent = new StringContent(
                                    JsonConvert.SerializeObject(new
                                    {
                                        newpassword = newPassword,
                                        userid = userId
                                    }),
                                    Encoding.UTF8,
                                    "application/json"
                                     );

                                if (realmGroup == "sso-mms-hospital")
                                {
                                    var responseSaveToDb = await httpClient.PostAsync("api/user/ChangePassword", changePasswordContent);
                                    if (responseSaveToDb.IsSuccessStatusCode)
                                    {
                                        ResponseChangePasswordModel responsedata = new ResponseChangePasswordModel
                                        {
                                            isStatus = true,
                                            StatusCode = 200,
                                            Message = "Change Password Complete"
                                        };
                                        return responsedata;
                                    }
                                }
                                else
                                {
                                    var responseSaveToDb = await httpClient.PostAsync($"{ConfigureCore.baseAddressPortalAdmin}api/audit/ChangePassword", changePasswordContent);
                                    if (responseSaveToDb.IsSuccessStatusCode)
                                    {
                                        ResponseChangePasswordModel responsedata = new ResponseChangePasswordModel
                                        {
                                            isStatus = true,
                                            StatusCode = 200,
                                            Message = "Change Password Complete"
                                        };
                                        return responsedata;
                                    }
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ResponseChangePasswordModel responsedata = new ResponseChangePasswordModel
                {
                    isStatus = false,
                    StatusCode = 400,
                    Message = "Bad Request : " + ex.Message
                };
                return responsedata;
            }
        }
        public async Task<ResponseModel> VerifyOtp(VerifyOTP verifyOTP)
        {
            var sendToUrl = await httpClient.PostAsJsonAsync("api/User/sendVerifyOTPMail", verifyOTP);
            if (sendToUrl.IsSuccessStatusCode)
            {
                var response = await sendToUrl.Content.ReadAsStringAsync();

                var convertRes = JsonConvert.DeserializeObject<ResponseModel>(response);
                return convertRes;
            }
            else
            {
                var response = new ResponseModel()
                {
                    issucessStatus = false,
                    statusCode = "400",
                    statusMessage = "error"
                };
                return response;
            }
        }
        public async Task<ResponseModel> SendMailAsync(RequestEmail requestEmail)
        {
            var sendToUrl = await httpClient.PostAsJsonAsync("api/User/sendMail", requestEmail);
            if (sendToUrl.IsSuccessStatusCode)
            {
                var response = await sendToUrl.Content.ReadAsStringAsync();

                var convertRes = JsonConvert.DeserializeObject<ResponseModel>(response);
                return convertRes;
            }
            else
            {
                return null;
            }
        }
        public async Task<ResponseModel> SendOtpToMail(RequestOtpEmail requestOtp)
        {
            var sendToUrl = await httpClient.PostAsJsonAsync("api/User/SendOtpToMail", requestOtp);
            if (sendToUrl.IsSuccessStatusCode)
            {
                var response = await sendToUrl.Content.ReadAsStringAsync();
                var convertRes = JsonConvert.DeserializeObject<ResponseModel>(response);
                return convertRes;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<PrefixM>> getPrefix()
        {
            try
            {
                var prefixM = await httpClient.GetAsync("api/user/getPrefix");
                if (prefixM.IsSuccessStatusCode)
                {
                    var result = prefixM.Content.ReadAsStringAsync();

                    var convertRes = JsonConvert.DeserializeObject<List<PrefixM>>(result.Result);

                    return convertRes;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ResponseModel> CheckDopa(CheckDopaModel dopaData)
        {
            var response = new ResponseModel();
            try
            {
                var sendToUrl = await httpClient.PostAsJsonAsync("api/Register/CheckDopa", dopaData);
                if (sendToUrl.IsSuccessStatusCode)
                {
                    response = new ResponseModel
                    {
                        issucessStatus = true,
                        statusCode = "200",
                        statusMessage = "true"
                    };
                    return response;
                }
                else
                {
                    response = new ResponseModel
                    {
                        issucessStatus = sendToUrl.IsSuccessStatusCode,
                        statusCode = sendToUrl.StatusCode.ToString(),
                        statusMessage = "bad request"
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
        public async Task<ResponseModel> CheckDopaProd(CheckDopaModel dopaData)
        {
            var response = new ResponseModel();
            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                string dopaUrl1 = String.Format($"{ConfigureCore.baseDopavalidateurl}?ConsumerSecret=n1MPfBuwyxS&AgentID={dopaData.UID13}");
                Console.WriteLine(dopaUrl1);
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
                    Console.WriteLine(verify);
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
    }
}
