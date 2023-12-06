using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Configs;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.Interface.Dental.Base;
using sso.mms.fees.api.Interface.Dental.EXT;

using sso.mms.fees.api.Services.Dental.EXT;
using sso.mms.fees.api.Services.Dental.Base;


namespace sso.mms.fees.api.Startup.DependencyInjectionSetup
{
   
    public static class DentalSetup 
    {
        public static ConnectionStringModels? ConnectionStrings { get; set; }
        public static ConfigurationManager DentalConfigurationBuilder(this ConfigurationManager configurationBuilder)
        {
            var Environment = configurationBuilder.GetSection("Environment").Value;
            ConnectionStrings = configurationBuilder.GetSection($"ConnectionStrings_{Environment}").Get<ConnectionStringModels>();
           
            return configurationBuilder;
        }
        public static IServiceCollection RegisterDentalServices(this IServiceCollection services)
        {
            services.AddTransient<ICarRecord, CarRecordServices>();
            services.AddTransient<ITreatmentRecord, TreatmentRecordServices>();
            services.AddTransient<IMProvince, ProvinceServices>();
            services.AddTransient<IListWithdrawals, ListWithdrawalServices>();
            services.AddTransient<IHistoryWithdrawals, HistoryWithdrawalServices>();
            services.AddDbContext<DentalContext>(options =>
           options.UseOracle($"{ConnectionStrings!.DentalContext}"));

            return services;
        }

       
    }
}
