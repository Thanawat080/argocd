using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.Configs;

namespace sso.mms.fees.ext.Providers.PromoteHealth.DetermineDoctor
{
    public class DetermineDoctorServices
    {
        private readonly HttpClient httpClient;

        public DetermineDoctorServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<AaiHealthChecklistM> GetById(decimal? Checklistid)
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<AaiHealthChecklistM>($"{ConfigureCore.FeesApiAddress}/api/PromoteHealth/base/AaiHealthCheckListM/GetById/{Checklistid}");
                return res;
            }
            catch
            {
                return null;
            }
        }

    }
}
