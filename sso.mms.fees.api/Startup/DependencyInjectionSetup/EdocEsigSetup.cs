using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Services.EdocEsig;
using sso.mms.fees.api.Services.EdocEsig.Base;
using sso.mms.fees.api.Configs;
using sso.mms.fees.api.Interface.EdocEsig.Base;
using sso.mms.fees.api.Services.PromoteHealth.Admin;

namespace sso.mms.fees.api.Startup.DependencyInjectionSetup
{
    public static class EdocEsigSetup
    {
        public static IServiceCollection RegisterEdocEsigServices(this IServiceCollection services)
        {
            services.AddTransient<IManageEdocEsigServices, ManageEdocEsigServices>();
            return services;
        }
    }
}
