using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.login.Extension;
using sso.mms.login.ViewModels;
using sso.mms.login.ViewModels.KeyCloak;
using sso.mms.login.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sso.mms.login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IdpDbContext db;
        private readonly PortalDbContext dbPortal;
        private readonly HttpClient httpClient;
        public string? env = ConfigureCore.ConfigENV;

        public LoginController(IdpDbContext idpDbContext, PortalDbContext portalDbContext, HttpClient httpClient)
        {
            this.httpClient = httpClient;

            this.db = idpDbContext;
            this.dbPortal = portalDbContext;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return "Value";
        }

        private string GenShortToken(string accessToken)
        {
            Guid GUID = Guid.NewGuid();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(accessToken);
            byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);

            return GUID + "-" + Convert.ToHexString(hashBytes);
        }

        private string CheckSecretRealmGroup(string realmGroup)
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

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {

            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixEpoch.AddSeconds(unixTimeStamp);
        }
        public int checkAccessTokenExpire(string access_token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(access_token) as JwtSecurityToken;
            IEnumerable<Claim> claims = jwtToken!.Claims;
            var exp = claims.FirstOrDefault(x => x.Type == "exp")?.Value;
            DateTime accessTokenExpirationDateTime = UnixTimeStampToDateTime(int.Parse(exp));
            DateTime currentDateTime = DateTime.UtcNow;

            int statusCode = 0;
            if (currentDateTime > accessTokenExpirationDateTime)
            {
                statusCode = 401;
                return statusCode;
            }
            statusCode = 200;
            return statusCode;
        }

        public async Task<IActionResult> RegisterSSOAdmin(CreateLoginKeyCloak requestLoginSSO)
        {

            var stream = $"{requestLoginSSO.access_token}";
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(stream) as JwtSecurityToken;
            IEnumerable<Claim> claims = jwtToken!.Claims;

            var name = claims.FirstOrDefault(x => x.Type == "name")?.Value;
            var preferred_username = claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
            var given_name = claims.FirstOrDefault(x => x.Type == "given_name")?.Value;
            var email = claims.FirstOrDefault(x => x.Type == "email")?.Value;
            var identification_number = claims.FirstOrDefault(x => x.Type == "identification_number")?.Value;
            var ssoBranchCode = claims.FirstOrDefault(x => x.Type == "SSObranchCode")?.Value;

            var splitDisplayName = given_name?.Split(" ");
            var fristname = splitDisplayName != null && splitDisplayName.Length > 0 ? splitDisplayName[0] : "";

            var lastname = splitDisplayName != null && splitDisplayName.Length > 1 ? splitDisplayName[1] : "";

            try
            {
                var results = await db.SsoUserMs
                    .FirstOrDefaultAsync(e => e.IsStatus == 1 && e.IsActive == true && e.UserName == requestLoginSSO.username);

                if (results == null)
                {

                    SsoUserM ssoUserM = new SsoUserM()
                    {
                        PrefixMCode = "",
                        FirstName = fristname,
                        MiddleName = "",
                        LastName = lastname,
                        SsoBranchCode = ssoBranchCode,
                        Email = email,
                        Mobile = "",
                        ImagePath = "",
                        ImageName = "",
                        GroupId = 82,
                        IsStatus = 1,
                        CreateDate = DateTime.Now,
                        CreateBy = requestLoginSSO.username,
                        UpdateDate = DateTime.Now,
                        UpdateBy = requestLoginSSO.username,
                        UserName = requestLoginSSO.username,
                        UserType = "",
                        IdentificationNumber = identification_number,
                        Password = "",
                        IsActive = true,

                    };

                    await db.SsoUserMs.AddAsync(ssoUserM);
                    await db.SaveChangesAsync();

                    var responseadduser = await httpClient.GetAsync($"{ConfigureCore.baseAddressbatchSync}api/batchsync/syncSsoUser");
                    Console.WriteLine(responseadduser);
                    return Ok(new { status = true, message = "success" });
                }
                else
                {
                    return Ok(new { status = true, message = "cloud not insert record" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        [HttpPost]
        public async Task<ActionResult<IEnumerable<SessionUserT>>> Login(CreateLoginKeyCloak valueKeyCloak)
        {

            SessionUserT sessionUser = new SessionUserT();
            Guid GUID = Guid.NewGuid();
            try
            {
                if (valueKeyCloak.realmGroup == "sso-mms-hospital")
                {

                    if (valueKeyCloak.username != null)
                    {
                        var results = await db.HospitalUserMs
                        .FirstOrDefaultAsync(e => e.IsStatus == 1 && e.IsActive == true && e.UserName == valueKeyCloak.username);

                        ResponseLogin userdata = new ResponseLogin();

                        if (results != null)
                        {
                            userdata = new ResponseLogin()
                            {
                                Id = results.Id,
                                PrefixMCode = results.PrefixMCode,
                                FirstName = results.FirstName,
                                LastName = results.LastName,
                                Email = results.Email,
                                Mobile = results.Mobile,
                                HospitalMId = results.HospitalMId,
                                IdenficationNumber = results.IdenficationNumber,
                                GroupId = results.GroupId,
                                IsActive = results.IsActive,
                                IsStatus = results.IsStatus,
                            };
                        }
                        else
                        {
                            return BadRequest();
                        }

                        var uid = results?.Id;
                        TokenKeyCLoak token = new TokenKeyCLoak();

                        token.access_token = valueKeyCloak.access_token;
                        token.refresh_token = valueKeyCloak.refresh_token;
                        token.refresh_expires_in = valueKeyCloak.refresh_expires_in;
                        token.expires_in = valueKeyCloak.expires_in;
                        token.token_type = valueKeyCloak.token_type;
                        token.identification_number = valueKeyCloak.identification_number;

                        if (results != null)
                        {
                            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(valueKeyCloak.access_token);
                            byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);
                            sessionUser.AccessToken = valueKeyCloak.idp_token;
                            sessionUser.CreateBy = valueKeyCloak.username;
                            sessionUser.UpdateBy = valueKeyCloak.username;
                            sessionUser.CreateDate = DateTime.Now;
                            sessionUser.UpdateDate = DateTime.Now;
                            sessionUser.IsActive = true;
                            sessionUser.IsStatus = 1;
                            sessionUser.ShortToken = GUID + "-" + Convert.ToHexString(hashBytes);
                            sessionUser.RealmGroup = valueKeyCloak.realmGroup;
                            sessionUser.ChannelLogin = valueKeyCloak.ChannelLogin;
                            sessionUser.HospitalUserMId = uid;
                            sessionUser.AccessType = "login";

                            await db.SessionUserTs.AddAsync(sessionUser);
                            await db.SaveChangesAsync();

                            return Ok(new
                            {
                                shortToken = sessionUser.ShortToken,
                                accessToken = valueKeyCloak.access_token,
                                responseUser = userdata
                            });
                        }
                        else
                        {
                            return Ok(new { message = "Not Found User" });
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else if (valueKeyCloak.realmGroup == "sso-mms-admin")
                {
                    if (valueKeyCloak.username != null && valueKeyCloak.password != null)
                    {

                        var responseLoginSSO = await RegisterSSOAdmin(valueKeyCloak);

                        var results = await db.SsoUserMs
                            .FirstOrDefaultAsync(e => e.IsStatus == 1 && e.IsActive == true && e.UserName == valueKeyCloak.username);

                        ResponseLogin userdata = new ResponseLogin();

                        if (results != null)
                        {
                            userdata = new ResponseLogin()
                            {
                                Id = results.Id,
                                PrefixMCode = results.PrefixMCode,
                                FirstName = results.FirstName,
                                LastName = results.LastName,
                                Email = results.Email,
                                Mobile = results.Mobile,
                                SsoDeptId = results.SsoDeptId,
                                SsoPositionId = results.SsoPositionId,
                                UserType = results.UserType,
                                GroupId = results.GroupId,
                                IsActive = results.IsActive,
                                IsStatus = results.IsStatus,
                            };
                        }
                        else
                        {
                            return BadRequest();
                        }

                        var uid = results?.Id;

                        TokenKeyCLoak token = new TokenKeyCLoak();

                        token.access_token = valueKeyCloak.access_token;
                        token.refresh_token = valueKeyCloak.refresh_token;
                        token.refresh_expires_in = valueKeyCloak.refresh_expires_in;
                        token.expires_in = valueKeyCloak.expires_in;
                        token.token_type = valueKeyCloak.token_type;

                        if (results != null)
                        {
                            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(valueKeyCloak.access_token);
                            byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);
                            sessionUser.AccessToken = valueKeyCloak.idp_token;
                            sessionUser.CreateBy = valueKeyCloak.username;
                            sessionUser.UpdateBy = valueKeyCloak.username;
                            sessionUser.CreateDate = DateTime.Now;
                            sessionUser.UpdateDate = DateTime.Now;
                            sessionUser.IsActive = true;
                            sessionUser.IsStatus = 1;
                            sessionUser.ShortToken = GUID + "-" + Convert.ToHexString(hashBytes);
                            sessionUser.RealmGroup = valueKeyCloak.realmGroup;
                            sessionUser.ChannelLogin = valueKeyCloak.ChannelLogin;
                            sessionUser.SsoUserMId = results.Id;
                            sessionUser.AccessType = "login";

                            await db.SessionUserTs.AddAsync(sessionUser);
                            await db.SaveChangesAsync();

                            return Ok(new { shortToken = sessionUser.ShortToken, accessToken = valueKeyCloak.access_token, responseUser = userdata });
                        }
                        else
                        {
                            return Ok(new { message = "Not Found User" });
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    if (valueKeyCloak.username != null && valueKeyCloak.password != null)
                    {
                        var results = await db.AuditorUserMs
                            .FirstOrDefaultAsync(e => e.IsStatus == 1 && e.IsActive == true && e.UserName == valueKeyCloak.username);
                        var userdata = new ResponseLogin()
                        {
                            Id = results.Id,
                            PrefixMCode = results.PrefixMCode,
                            FirstName = results.FirstName,
                            LastName = results.LastName,
                            Email = results.Email,
                            Mobile = results.Mobile,
                            RoleGroupMId = results.RoleGroupMId,
                            IsActive = results.IsActive,
                            IsStatus = results.IsStatus,
                        };
                        var uid = results?.Id;

                        TokenKeyCLoak token = new TokenKeyCLoak();

                        token.access_token = valueKeyCloak.access_token;
                        token.refresh_token = valueKeyCloak.refresh_token;
                        token.refresh_expires_in = valueKeyCloak.refresh_expires_in;
                        token.expires_in = valueKeyCloak.expires_in;
                        token.token_type = valueKeyCloak.token_type;

                        //if (results != null)
                        //{
                        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(valueKeyCloak.access_token);
                        byte[] hashBytes = MD5.Create().ComputeHash(inputBytes);
                        sessionUser.AccessToken = valueKeyCloak.idp_token;
                        sessionUser.CreateBy = valueKeyCloak.username;
                        sessionUser.UpdateBy = valueKeyCloak.username;
                        sessionUser.CreateDate = DateTime.Now;
                        sessionUser.UpdateDate = DateTime.Now;
                        sessionUser.IsActive = true;
                        sessionUser.IsStatus = 1;
                        sessionUser.ShortToken = GUID + "-" + Convert.ToHexString(hashBytes);
                        sessionUser.RealmGroup = valueKeyCloak.realmGroup;
                        sessionUser.ChannelLogin = valueKeyCloak.ChannelLogin;
                        sessionUser.AuditorUserMId = uid;
                        sessionUser.AccessType = "login";

                        await db.SessionUserTs.AddAsync(sessionUser);
                        await db.SaveChangesAsync();

                        return Ok(new { shortToken = sessionUser.ShortToken, accessToken = valueKeyCloak.access_token, responseUser = userdata });
                        //}
                        //else
                        //{
                        //    return Ok(new { message = "Not Found User" });
                        //}
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //        return Ok(new { shortToken = sessionUser.ShortToken, accessToken = valueKeyCloak.access_token, responseUser = results });
            //        //}
            //        //else
            //        //{
            //        //    return Ok(new { message = "Not Found User" });
            //        //}
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<IEnumerable<ResponseShortToken>>> CheckShortToken(ShortToken shortToken)
        {
            var result = await db.SessionUserTs.Where(w => w.ShortToken == shortToken.shortToken)
                 .Select(x =>
             new ResponseShortToken
             {
                 shortToken = x.ShortToken,
                 accessToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.access_token
                 // profile
             }).ToListAsync();

            return result;
        }

        [HttpGet("[action]/{shortToken}")]
        public async Task<ActionResult<ResponseShortToken>> GetToken(string shortToken)
        {

            var findShortToken = await db.SessionUserTs.Where(w => w.ShortToken == shortToken).ToListAsync();
            if (findShortToken[0].RealmGroup == "sso-mms-hospital")
            {
                var query = findShortToken.Join(db.HospitalUserMs, sut => sut.HospitalUserMId, hm => hm.Id,
                    (sut, hm) => new { sut, hm })
                      .Select(async x =>
                  new ResponseShortToken
                  {
                      shortToken = x.sut.ShortToken,
                      accessToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken).access_token,
                      expiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken)!.expires_in,
                      refreshExpiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!)!.refresh_expires_in,
                      refreshToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!)!.refresh_token,
                      tokenType = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).token_type,
                      notBeforePolicy = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).notbeforepolicy,
                      sessionState = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).session_state,
                      scope = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).scope,
                      identification_number = x.hm.IdenficationNumber,
                      HospitalUserMId = x.hm.Id,
                      given_name = x.hm.FirstName.ToString() + " " + x.hm.LastName,
                      //HospitalUserMDetail = await HospitalUserProfile(x.hm.Id),
                      //HospitalUserList = await HospitalUserProfileList(),
                      HospitalMId = x.hm.HospitalMId,
                      HospitalMCode = dbPortal.HospitalMs.FirstOrDefaultAsync(s => s.Id == x.hm.HospitalMId).Result.Code,
                      UserName = x.hm.UserName,
                      realmGroup = x.sut.RealmGroup,
                  }).FirstOrDefault();


                var checkTokenExpire = checkAccessTokenExpire(query!.Result.accessToken!);

                if (checkTokenExpire == 401)
                {
                    Console.WriteLine("Access Token is Unauthorized.");
                    return StatusCode(401, "Unauthorized");
                }
                return await query;

            }
            else if (findShortToken[0].RealmGroup == "sso-mms-admin")
            {
                var query = findShortToken.Join(db.SsoUserMs, sut => sut.SsoUserMId, sso => sso.Id, (sut, sso) => new { sut, sso })
                    .Select(async x =>
                new ResponseShortToken
                {
                    shortToken = x.sut.ShortToken,
                    accessToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken).access_token,
                    expiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken)!.expires_in,
                    refreshExpiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!)!.refresh_expires_in,
                    refreshToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!)!.refresh_token,
                    tokenType = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).token_type,
                    notBeforePolicy = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).notbeforepolicy,
                    sessionState = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).session_state,
                    scope = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).scope,
                    SsoUserMId = x.sso.Id,
                    given_name = x.sso.FirstName.ToString() + " " + x.sso.LastName,
                    //SsoUserMDetail = await SsoUserProfile(x.sso.Id),
                    //SsoUserList = await SsoUserProfileList(),
                    UserName = x.sso.UserName,
                    realmGroup = x.sut.RealmGroup,
                }).FirstOrDefault();

                var checkTokenExpire = checkAccessTokenExpire(query!.Result.accessToken!);

                if (checkTokenExpire == 401)
                {
                    Console.WriteLine("Access Token is Unauthorized.");
                    return StatusCode(401, "Unauthorized");
                }

                return await query;
            }
            else
            {
                var query = findShortToken.Join(db.AuditorUserMs, sut => sut.AuditorUserMId, aud => aud.Id, (sut, aud) => new { sut, aud })
                    .Select(async x =>
                new ResponseShortToken
                {
                    shortToken = x.sut.ShortToken,
                    accessToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken).access_token,
                    expiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken)!.expires_in,
                    refreshExpiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!)!.refresh_expires_in,
                    refreshToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!)!.refresh_token,
                    tokenType = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).token_type,
                    notBeforePolicy = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).notbeforepolicy,
                    sessionState = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).session_state,
                    scope = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.sut.AccessToken!).scope,
                    UserName = x.aud.UserName,
                    AuditorUserMId = x.aud.Id,
                    given_name = x.aud.FirstName.ToString() + " " + x.aud.LastName,
                    //AuditorUserMDetail = await AuditorUserProfile(x.aud.Id),
                    //AuditorUserList = await AuditorUserProfileList(),
                    realmGroup = x.sut.RealmGroup,
                }).FirstOrDefault();

                var checkTokenExpire = checkAccessTokenExpire(query!.Result.accessToken!);

                if (checkTokenExpire == 401)
                {
                    Console.WriteLine("Access Token is Unauthorized.");
                    return StatusCode(401, "Unauthorized");
                }

                return await query;
            }
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<ResponseShortToken>> RefreshToken(RequestRefreshToken request)
        {
            //if (request.refreshToken == "eyJhbGciOiJIUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI0YTM1ODg1Mi04NjM2LTRhNTUtYTgwNy1hNzcxOGRmNGY0YWMifQ.eyJleHAiOjE2ODYxOTg3OTIsImlhdCI6MTY4NjE5Njk5MiwianRpIjoiZjNjNjNlNjYtZDBlOC00MDc2LTk3YjEtNmViNTRhMGE4YzVlIiwiaXNzIjoiaHR0cHM6Ly9pZHBlc2VsZnVhdC5zc28uZ28udGgvYXV0aC9yZWFsbXMvc3Nvb2ZmaWNlciIsImF1ZCI6Imh0dHBzOi8vaWRwZXNlbGZ1YXQuc3NvLmdvLnRoL2F1dGgvcmVhbG1zL3Nzb29mZmljZXIiLCJzdWIiOiIwNzc0ZGM2ZS0xOWUwLTRjYjAtOWNjZS1jM2Y4YWVmYWRhOGYiLCJ0eXAiOiJSZWZyZXNoIiwiYXpwIjoic3NvY2xpZW50Iiwibm9uY2UiOiI1NmU5ZmJlZC1hNzM0LTQ0N2MtODNkYi04ODU5YWQ2YzVjMGQiLCJzZXNzaW9uX3N0YXRlIjoiYmVjMmQwN2UtY2YxZC00MTljLTljY2EtOGE5MTRlODYwYzM2Iiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCIsInNpZCI6ImJlYzJkMDdlLWNmMWQtNDE5Yy05Y2NhLThhOTE0ZTg2MGMzNiJ9.XuAycdip5fR7oPQBsvY8goi32Yum2ysBVG4XJ_cCzMA")
            //{

            //    var resultToken = await GetToken("27e0be8a-30e6-4dd2-b1cd-3bef5abbb6a2-616F17B1B837787910708A327441D047");
            //    return resultToken;
            //}

            Dictionary<string, string> data;

            data = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"client_id", $"client-{request.realmGroup}"},
                {"client_secret", CheckSecretRealmGroup(request.realmGroup)},
                {"refresh_token",$"{request.refreshToken}" }
            };

            var content = new FormUrlEncodedContent(data);

            var response = await httpClient.PostAsync($"{ConfigureCore.baseAddressIdpKeycloak}realms/{request.realmGroup}/protocol/openid-connect/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var convertJson = JsonConvert.DeserializeObject<TokenKeyCLoak>(responseContent);

            //if (!response.IsSuccessStatusCode)
            //{
            //    return Ok(responseContent);
            //}

            var findIdpToken = await db.SessionUserTs.Where(w => w.AccessToken.Contains(request.refreshToken)).Select(x => new
            {
                x.UpdateBy,
                x.CreateBy,
                accessToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.access_token,
                refreshToken = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken!)!.refresh_token!,
                expiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.expires_in,
                refreshExpiresIn = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.refresh_expires_in,
                tokenType = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.token_type,
                notBeforePolicy = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.notbeforepolicy,
                sessionState = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.session_state,
                scope = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.scope,
                realmGroup = JsonConvert.DeserializeObject<ResponseLoginTokenKeyCloak>(x.AccessToken)!.realmGroup,
                hospital_user_m_id = x.HospitalUserMId,
                sso_user_m_id = x.SsoUserMId,
                auditor_user_m_id = x.AuditorUserMId
            }).FirstOrDefaultAsync();


            TokenKeyCLoak token = new TokenKeyCLoak();

            token.access_token = convertJson!.access_token!;
            token.refresh_token = convertJson!.refresh_token;
            token.refresh_expires_in = convertJson.refresh_expires_in;
            token.expires_in = convertJson.expires_in;
            token.token_type = convertJson.token_type;
            token.not_before_policy = convertJson.not_before_policy;
            token.session_state = convertJson.session_state;
            token.scope = convertJson.scope;


            if (findIdpToken != null)
            {
                SessionUserT sessionUser = new SessionUserT();

                sessionUser.AccessToken = JsonConvert.SerializeObject(token);
                sessionUser.CreateBy = findIdpToken.CreateBy;
                sessionUser.UpdateBy = findIdpToken.UpdateBy;
                sessionUser.CreateDate = DateTime.Now;
                sessionUser.UpdateDate = DateTime.Now;
                sessionUser.IsActive = true;
                sessionUser.IsStatus = 1;
                sessionUser.RealmGroup = request.realmGroup;
                sessionUser.HospitalUserMId = findIdpToken.hospital_user_m_id;
                sessionUser.SsoUserMId = findIdpToken.sso_user_m_id;
                sessionUser.AuditorUserMId = findIdpToken.auditor_user_m_id;
                sessionUser.ShortToken = GenShortToken(findIdpToken.accessToken!);
                sessionUser.AccessType = "refreshToken";
                await db.SessionUserTs.AddAsync(sessionUser);
                await db.SaveChangesAsync();

                var resultToken = await GetToken(sessionUser.ShortToken);

                return resultToken;

            }
            else
            {
                return NotFound();
            }
        }
    }
}
