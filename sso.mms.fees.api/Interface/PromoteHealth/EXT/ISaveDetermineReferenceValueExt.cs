using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;

namespace sso.mms.fees.api.Interface.PromoteHealth.EXT
{
    public interface ISaveDetermineReferenceValueExt
    {
        Task<string> saveandupdate(SaveDetermineReferenceValue data);

        Task<string> saveandupdateDoctor(DataSaveDoctor data);
        
    }
}
