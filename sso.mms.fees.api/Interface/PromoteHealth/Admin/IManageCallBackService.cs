using sso.mms.fees.api.ViewModels.Responses;
namespace sso.mms.fees.api.Interface.PromoteHealth.Admin
{
    public interface IManageCallBackService
    {
        Task<Response<string>> CallBackPromoteHealth(string PayOrderId, string SignBy);
    }
}
