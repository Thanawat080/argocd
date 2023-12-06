using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
namespace sso.mms.fees.api.Interface.Dental.Base
{
    public interface IMProvince
    {
        Task<List<AdbMProvince>> GetProvince();
        Task<List<AdbMDistrict>> GetDistrict(int provinceId);
        Task<List<AdbMSubdistrict>> GetSubDistrict(int disId);
        
    }
}
