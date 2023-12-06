using Microsoft.AspNetCore.Components.Forms;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using sso.mms.helper.ViewModels;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface IManageWithdrawalExtServices
    {
        Task<List<WithdrawalView>> GetWithdrawalByHoscode(string hoscode);

        Task<List<ViewAaiHealthCheckupH>> GetCheckupHInWithdrawal(string hoscode, string withdrawalNo);

        Task<string> EditWithdrawalDoc(WithdrawalDocView dataDoc);

        //Task<ResponseUpload> UploadFile(IBrowserFile file);
    }
}
