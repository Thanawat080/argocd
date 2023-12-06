using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace sso.mms.fees.ext.Providers.PromoteHealth.DetermineReferenceValue
{
    public class DetermineReferenceServices
    {
        private readonly HttpClient httpClient;

        public DetermineReferenceServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<AaiHealthSetRefNicknameCfg>> GetSetRefNickName(ManageSetRefNicknameCfgView data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<ManageSetRefNicknameCfgView>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/ext/ManageSetRefNicknameCfg/GetSetRefNicknameCfg", data);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<List<AaiHealthSetRefNicknameCfg>>();
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<AaiHealthChecklistD>> GetCheckListD(decimal? Checklistid)
        {
            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AaiHealthChecklistD>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListD/GetByCheckListIdRawModel/{Checklistid}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<AaiHealthChecklistM>> GetCheckListMByNowYear(int year)
        {

            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AaiHealthChecklistM>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListM/GetByYear/{year}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public async Task<List<CheckListDAndManageChecklistCfg>> GetOptionTabs(decimal checklistMId, string hosCode)
        {

            try
            {
                var result = await httpClient.GetFromJsonAsync<List<CheckListDAndManageChecklistCfg>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageCheckListD/GetAll?checklistMId={checklistMId}&hosCode={hosCode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public async Task<List<AaiHealthSetRefDoctorMView>> GetRefDoctor(decimal checklistMId, string hosCode)
        {

            try
            {
                var result = await httpClient.GetFromJsonAsync<List<AaiHealthSetRefDoctorMView>>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/Ext/ManageAaiHealthSetRefDoctorCfg/GetAll?checklistMId={checklistMId}&hosCode={hosCode}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<string> SaveAndUpdate(SaveDetermineReferenceValue data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<SaveDetermineReferenceValue>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/ext/SaveDetermineReferenceValue/Saveandupdate", data);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> saveandupdateDoctor(DataSaveDoctor data)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync<DataSaveDoctor>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/ext/SaveDetermineReferenceValue/saveandupdateDoctor", data);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        
    }
}
