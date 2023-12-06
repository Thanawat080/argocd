using AutoMapper;
using Newtonsoft.Json;
using sso.mms.helper.Configs;
using sso.mms.helper.Data;
using sso.mms.helper.PortalModel;
using sso.mms.helper.ViewModels;
using sso.mms.notification.ViewModel.BoardCast;
using sso.mms.notification.ViewModel.Response;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;

namespace sso.mms.notification.Services
{
    public class NotificationService
    {
        private readonly HttpClient httpClient;

        public NotificationService(HttpClient httpClient)
        {

            this.httpClient = httpClient;
        }
        public async Task<List<NotiM>> GetNotificationM()
        {
            try
            {
                var responseNotiM = await httpClient.GetAsync("api/Notification/GetListCategoryM");
                responseNotiM.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiM.Content.ReadAsStringAsync());
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    var NotiList = JsonConvert.DeserializeObject<List<NotiM>>(result);
                    return NotiList;
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

        public async Task<List<NotiM>> GetNotiByUser(NotiTApiModel notiT)
        {
            try
            {
                var content = new StringContent(
                                    JsonConvert.SerializeObject(notiT),
                                    Encoding.UTF8,
                                    "application/json"
                                     );
                var responseNotiT = await httpClient.PostAsync("api/Notification/GetNotiTByUser", content);
                responseNotiT.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiT.Content.ReadAsStringAsync());
                if (responseNotiT.IsSuccessStatusCode)
                {
                    var result = responseNotiT.Content.ReadAsStringAsync().Result;
                    var NotiList = JsonConvert.DeserializeObject<List<NotiT>>(result);
                    var contentM = new StringContent(
                                    JsonConvert.SerializeObject(NotiList),
                                    Encoding.UTF8,
                                    "application/json"
                                     );
                    var responseNotiM = await httpClient.PostAsync("api/Notification/GetNotiMByUser", contentM);
                    responseNotiM.EnsureSuccessStatusCode();
                    if (responseNotiM.IsSuccessStatusCode)
                    {
                        var resultM = responseNotiM.Content.ReadAsStringAsync().Result;
                        var NotiMList = JsonConvert.DeserializeObject<List<NotiM>>(resultM);
                        return NotiMList;
                    }
                    else
                    {
                        return null;
                    }
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

        public async Task<List<NotificationTModel>> GetNotiT()
        {
            try
            {
                var responseNotiM = await httpClient.GetAsync("api/Notification/getNotiT");
                responseNotiM.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiM.Content.ReadAsStringAsync());
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    var tranSectionList = JsonConvert.DeserializeObject<List<NotificationTModel>>(result);
                    return tranSectionList;
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


        public async Task<List<NotiT>> GetAppNotiT(string userType, List<string> roleCodeList, string username, string orgCode)
        {
            try
            {
                var responseNotiM = await httpClient.GetAsync("api/Notification/getAppNotiT");
                responseNotiM.EnsureSuccessStatusCode();
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    var tranSactionList = JsonConvert.DeserializeObject<List<NotificationT>>(result);

                    // filter userType
                    if(userType == "sso-mms-admin")
                    {
                        tranSactionList = tranSactionList.Where(tsl => tsl.UserType == "C" || tsl.UserType == "P").ToList();
                    }
                    if(userType == "sso-mms-hospital")
                    {
                        tranSactionList = tranSactionList.Where(tsl => tsl.UserType == "H").ToList();
                    }

                    // filter by Role => RoleCode
                    var tranSactionByRole = tranSactionList.Where(x => x.NotifyOption == "R" && roleCodeList.Contains(x.RoleCode) && x.OrgCode == orgCode).ToList();


                    // filter by UserName => userName
                    var tranSactionByUser = tranSactionList.Where(x => x.NotifyOption == "U" && x.UserName == username).ToList();


                    // Auto Mapping NotificationT => NotiT
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<NotificationT, NotiT>());
                    var mapper = config.CreateMapper();

                    tranSactionList = tranSactionByRole.Concat(tranSactionByUser).OrderByDescending(tsl => tsl.CreateDate).ToList();

                    List<NotiT> res = mapper.Map<List<NotiT>>(tranSactionList);
                    return res;
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

        public async Task<NotiM> mappingTransaction(NotiM notificationM, List<NotiT> notiT)
        {
            notiT = notiT.Where(nt => nt.IsStatus == 1).ToList();
            if (notificationM.Id == 1)
            {
                notificationM.NotificationTs = notificationM.NotificationTs.Concat(notiT.Where(nt => (nt.AppCode == "POST-AUDIT" || nt.AppCode == "PRE-AUDIT")).ToList() ?? new List<NotiT>()).ToList();
            }
            return notificationM;
        }


        public async Task<ResponseStatus> AddNotiTransection(NotificationT notificationT)
        {
            try
            {
                NotificationT notifications = new NotificationT();
                notifications.Id = notificationT.Id;
                notifications.Title = notificationT.Title;
                notifications.Content = notificationT.Content;
                notifications.NotiMId = notificationT.NotiMId;
                notifications.IsActive = true;
                notifications.IsStatus = 1;
                notifications.CreateDate = DateTime.Now;
                notifications.CreateBy = notificationT.CreateBy;
                notifications.UpdateDate = DateTime.Now;

                var responseNotiM = await httpClient.PostAsJsonAsync("api/Notification/addNotificationT", notifications);
                responseNotiM.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiM.Content.ReadAsStringAsync());
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    var tranSectionList = JsonConvert.DeserializeObject<ResponseStatus>(result);
                    return tranSectionList;
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

        public async Task<List<NotiT>> GetNotificationT(int notiMId)
        {
            try
            {
                var responseNotiM = await httpClient.GetAsync($"api/Notification/GetNotiTbyId/{notiMId}");
                responseNotiM.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiM.Content.ReadAsStringAsync());
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    var NotiList = JsonConvert.DeserializeObject<List<NotiT>>(result);
                    return NotiList;
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

        public async Task<List<NotiT>> GetNotificationTByUser(int notiMId, NotiTApiModel notiT)
        {
            try
            {
                var content = new StringContent(
                                    JsonConvert.SerializeObject(notiT),
                                    Encoding.UTF8,
                                    "application/json"
                                     );
                var responseNotiT = await httpClient.PostAsync("api/Notification/GetNotiTByUser", content);
                responseNotiT.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiT.Content.ReadAsStringAsync());
                if (responseNotiT.IsSuccessStatusCode)
                {
                    var result = responseNotiT.Content.ReadAsStringAsync().Result;
                    var NotiList = JsonConvert.DeserializeObject<List<NotiT>>(result);
                    return NotiList.Where(item => item.NotiMId == notiMId).ToList();
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

        public async Task<NotiT> GetContentNotiT(int notiT)
        {
            try
            {
                var responseNotiM = await httpClient.GetAsync($"api/Notification/GetNotiTDatabyId/{notiT}");
                responseNotiM.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiM.Content.ReadAsStringAsync());
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    var NotiList = JsonConvert.DeserializeObject<NotiT>(result);
                    return NotiList;
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

        public async Task<bool> UpdateStatusNotiT(int notiTId, int status)
        {

            try
            {

                var responseNotiM = await httpClient.GetAsync($"api/Notification/UpdateNotiT/{notiTId}/{status}");
                responseNotiM.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiM.Content.ReadAsStringAsync());
                if (responseNotiM.IsSuccessStatusCode)
                {
                    var result = responseNotiM.Content.ReadAsStringAsync().Result;
                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> AddNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                var responseNotiLog = await httpClient.PostAsJsonAsync("api/Notification/AddNotificationLog", notificationLog);
                responseNotiLog.EnsureSuccessStatusCode();
                //Console.WriteLine(await responseNotiLog.Content.ReadAsStringAsync());
                if (responseNotiLog.IsSuccessStatusCode)
                {
                    //var data = responseNotiLog.Content.ReadAsStringAsync().Result;
                    //var result = JsonConvert.DeserializeObject<ResponseStatus>(data);
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
