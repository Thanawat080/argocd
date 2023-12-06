

using sso.mms.fees.api.ViewModels.Responses;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IGeneratePreAuditServices
    {
        Task<bool> GenerateFile();

        Task<bool> ChekcStatus(int jobId);

        Task<bool> PingAi(PingModels data);
    }
}
