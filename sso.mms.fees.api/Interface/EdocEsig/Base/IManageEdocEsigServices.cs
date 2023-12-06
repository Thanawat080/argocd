using sso.mms.fees.api.ViewModels.EdocEsig;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.fees.api.ViewModels.Responses;
namespace sso.mms.fees.api.Interface.EdocEsig.Base
{
    public interface  IManageEdocEsigServices
    {
        Task<Response<string>> GenReportFromDB(string urlapi, string pathFile);

        Task<Response<ResCreateDocument>> CreateDocument(CreateDocument data);

        Task<Response<ResSendSign>> SendSign(SendSignView data);
        
    }
}
