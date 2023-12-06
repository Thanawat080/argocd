using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Interface.Dental.Base;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using System.Linq;

namespace sso.mms.fees.api.Services.Dental.Base
{

    public class ProvinceServices : IMProvince
    {
        private readonly DentalContext db;
        public ProvinceServices(DentalContext DbContext)
        {
            db = DbContext;
        }

        public async Task<List<AdbMDistrict>> GetDistrict(int provinceId)
        {
            return await db.AdbMDistricts.Where(w => w.ProvId == provinceId).ToListAsync();
        }

        public async Task<List<AdbMProvince>> GetProvince()
        {
            return await db.AdbMProvinces.ToListAsync();
        }

        public async Task<List<AdbMSubdistrict>> GetSubDistrict(int disId)
        {
            return await db.AdbMSubdistricts.Where(w => w.DistId == disId).ToListAsync();
        }
    }
}
