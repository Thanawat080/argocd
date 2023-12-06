using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IManageBookHealthCheckupExtServices
    {
        Task<string> CreateBookCheckup(ManageBookHealthCheckupView data);
        Task<string> UpdateBookCheckup(ManageBookHealthCheckupView data);
    }
}
