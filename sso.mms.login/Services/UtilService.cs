using Microsoft.EntityFrameworkCore;
using sso.mms.helper.Data;
using sso.mms.login.Interface;
using sso.mms.login.ViewModels.UserModels;

namespace sso.mms.login.Services
{
    public class UtilService: IUtilService
    {
        private readonly IdpDbContext db;

        public UtilService(IdpDbContext idpDbContext)
        {
            this.db = idpDbContext;

        }
        public async Task<HospitalUserProfile> HospitalUserProfile(int hospitalMId)
        {

            var findHosptalUserProfile = await db.HospitalUserMs.Where(x => x.Id == hospitalMId).Select(x => new HospitalUserProfile
            {
                Id = x.Id,
                PrefixMCode = x.PrefixMCode,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                Email = x.Email,
                ImagePath = x.ImagePath,
                ImageName = x.ImageName,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy,
                UserName = x.UserName,
                PositionName = x.PositionName
            }).FirstOrDefaultAsync();

            return findHosptalUserProfile!;
        }
        public async Task<AuditorUserProfile> AuditorUserProfile(int auditorId)
        {

            var findHosptalUserProfile = await db.AuditorUserMs.Where(x => x.Id == auditorId).Select(x => new AuditorUserProfile
            {
                Id = x.Id,
                PrefixMCode = x.PrefixMCode,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
              
                StartDate = x.StartDate,
                ExpireDate = x.ExpireDate,
                Email = x.Email,
                ImagePath = x.ImagePath,
                ImageName = x.ImageName,
                RoleGroupMId = x.RoleGroupMId,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy,
                UserName = x.UserName,
            }).FirstOrDefaultAsync();

            return findHosptalUserProfile!;
        }
        public async Task<SsoUserProfile> SsoUserProfile(int ssoUserId)
        {

            var findHosptalUserProfile = await db.SsoUserMs.Where(x => x.Id == ssoUserId).Select(x => new SsoUserProfile
            {
                Id = x.Id,
                PrefixMCode = x.PrefixMCode,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Email = x.Email,
                ImagePath = x.ImagePath,
                ImageName = x.ImageName,
                GroupId = x.GroupId,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy,
                UserName = x.UserName,
            }).FirstOrDefaultAsync();

            return findHosptalUserProfile!;
        }
        public async Task<List<HospitalUserProfile>> HospitalUserProfileList()
        {

            var findHosptalUserProfile = await db.HospitalUserMs.Select(x => new HospitalUserProfile
            {
                Id = x.Id,
                PrefixMCode = x.PrefixMCode,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                Email = x.Email,
                ImagePath = x.ImagePath,
                ImageName = x.ImageName,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy,
                UserName = x.UserName,
                PositionName = x.PositionName
            }).ToListAsync();

            return findHosptalUserProfile!;
        }
        public async Task<List<AuditorUserProfile>> AuditorUserProfileList()
        {

            var findHosptalUserProfile = await db.AuditorUserMs.Select(x => new AuditorUserProfile
            {
                Id = x.Id,
                PrefixMCode = x.PrefixMCode,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                StartDate = x.StartDate,
                ExpireDate = x.ExpireDate,
                Email = x.Email,
                ImagePath = x.ImagePath,
                ImageName = x.ImageName,
                RoleGroupMId = x.RoleGroupMId,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy,
                UserName = x.UserName,
            }).ToListAsync();

            return findHosptalUserProfile!;
        }
        public async Task<List<SsoUserProfile>> SsoUserProfileList()
        {

            var findHosptalUserProfile = await db.SsoUserMs.Select(x => new SsoUserProfile
            {
                Id = x.Id,
                PrefixMCode = x.PrefixMCode,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Email = x.Email,
                ImagePath = x.ImagePath,
                ImageName = x.ImageName,
                GroupId = x.GroupId,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                UpdateDate = x.UpdateDate,
                UpdateBy = x.UpdateBy,
                UserName = x.UserName,
            }).ToListAsync();

            return findHosptalUserProfile!;
        }
    }
}
