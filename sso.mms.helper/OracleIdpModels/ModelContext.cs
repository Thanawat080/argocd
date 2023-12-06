using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.helper.OracleIdpModels;

public partial class PromoteHealthContext : DbContext
{
    public PromoteHealthContext()
    {
    }

    public PromoteHealthContext(DbContextOptions<PromoteHealthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutMApp> AutMApps { get; set; }

    public virtual DbSet<AutMMenu> AutMMenus { get; set; }

    public virtual DbSet<AutMRoleGroup> AutMRoleGroups { get; set; }

    public virtual DbSet<AutRoleMenu> AutRoleMenus { get; set; }

    public virtual DbSet<AutRoleUserMapping> AutRoleUserMappings { get; set; }

    public virtual DbSet<HosHospital> HosHospitals { get; set; }

    public virtual DbSet<UserMAuditor> UserMAuditors { get; set; }

    public virtual DbSet<UserMHospital> UserMHospitals { get; set; }

    public virtual DbSet<UserMSso> UserMSsos { get; set; }

    public virtual DbSet<UserPrefixM> UserPrefixMs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("MMS_UTILITY")   // MMS_UTILITY
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AutMApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C007555");

            entity.ToTable("AUT_M_APP");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AppCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APP_CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.SystemDesc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("SYSTEM_DESC");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<AutMMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C007551");

            entity.ToTable("AUT_M_MENU");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AppCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APP_CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.MenuCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MENU_CODE");
            entity.Property(e => e.MenuDesc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MENU_DESC");
            entity.Property(e => e.MenuName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MENU_NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AutMRoleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AUT_M_ROLE_GROUP_PK");

            entity.ToTable("AUT_M_ROLE_GROUP");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLE_CODE");
            entity.Property(e => e.RoleDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ROLE_DESC");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserGroup)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("USER_GROUP");
        });

        modelBuilder.Entity<AutRoleMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C007553");

            entity.ToTable("AUT_ROLE_MENU");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsRoleApprove)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_APPROVE");
            entity.Property(e => e.IsRoleCancel)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_CANCEL");
            entity.Property(e => e.IsRoleCreate)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_CREATE");
            entity.Property(e => e.IsRoleDelete)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_DELETE");
            entity.Property(e => e.IsRolePrint)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_PRINT");
            entity.Property(e => e.IsRoleRead)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_READ");
            entity.Property(e => e.IsRoleUpdate)
                .HasPrecision(1)
                .HasColumnName("IS_ROLE_UPDATE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.RoleGroupMId)
                .HasPrecision(10)
                .HasColumnName("ROLE_GROUP_M_ID");
            entity.Property(e => e.RoleMenuMId)
                .HasPrecision(10)
                .HasColumnName("ROLE_MENU_M_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AutRoleUserMapping>(entity =>
        {
            entity.HasKey(e => new { e.RoleGroupId, e.UserName }).HasName("AUT_ROLE_USER_MAPPING_PK");

            entity.ToTable("AUT_ROLE_USER_MAPPING");

            entity.Property(e => e.RoleGroupId)
                .HasPrecision(10)
                .HasColumnName("ROLE_GROUP_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("USER_TYPE");
        });

        modelBuilder.Entity<HosHospital>(entity =>
        {
            entity.HasKey(e => e.HospId).HasName("HOS_HOSPITAL_PK");

            entity.ToTable("HOS_HOSPITAL", "HOS_PROD"); // HOS_PROD

            entity.Property(e => e.HospId)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("HOSP_ID");
            entity.Property(e => e.AffId)
                .HasPrecision(10)
                .HasColumnName("AFF_ID");
            entity.Property(e => e.CorpAddrNo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CORP_ADDR_NO");
            entity.Property(e => e.CorpDistId)
                .HasPrecision(10)
                .HasColumnName("CORP_DIST_ID");
            entity.Property(e => e.CorpEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORP_EMAIL");
            entity.Property(e => e.CorpFax)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORP_FAX");
            entity.Property(e => e.CorpName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CORP_NAME");
            entity.Property(e => e.CorpPostcode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CORP_POSTCODE");
            entity.Property(e => e.CorpProvId)
                .HasPrecision(10)
                .HasColumnName("CORP_PROV_ID");
            entity.Property(e => e.CorpRoad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CORP_ROAD");
            entity.Property(e => e.CorpSubdistId)
                .HasPrecision(10)
                .HasColumnName("CORP_SUBDIST_ID");
            entity.Property(e => e.CorpTax)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CORP_TAX");
            entity.Property(e => e.CorpTel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORP_TEL");
            entity.Property(e => e.CorpTypeId)
                .HasPrecision(10)
                .HasColumnName("CORP_TYPE_ID");
            entity.Property(e => e.CorpWebsite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORP_WEBSITE");
            entity.Property(e => e.CreateDtm)
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DTM");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.HospAddrNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HOSP_ADDR_NO");
            entity.Property(e => e.HospCode5)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSP_CODE5");
            entity.Property(e => e.HospCode7)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSP_CODE7");
            entity.Property(e => e.HospCode9)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSP_CODE9");
            entity.Property(e => e.HospDisplayName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HOSP_DISPLAY_NAME");
            entity.Property(e => e.HospDistId)
                .HasPrecision(10)
                .HasColumnName("HOSP_DIST_ID");
            entity.Property(e => e.HospEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOSP_EMAIL");
            entity.Property(e => e.HospFax)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOSP_FAX");
            entity.Property(e => e.HospName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HOSP_NAME");
            entity.Property(e => e.HospOfficialName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HOSP_OFFICIAL_NAME");
            entity.Property(e => e.HospPerson)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HOSP_PERSON");
            entity.Property(e => e.HospPostcode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSP_POSTCODE");
            entity.Property(e => e.HospProvId)
                .HasPrecision(10)
                .HasColumnName("HOSP_PROV_ID");
            entity.Property(e => e.HospRoad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOSP_ROAD");
            entity.Property(e => e.HospSubdistId)
                .HasPrecision(10)
                .HasColumnName("HOSP_SUBDIST_ID");
            entity.Property(e => e.HospTel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOSP_TEL");
            entity.Property(e => e.HospType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSP_TYPE");
            entity.Property(e => e.HospTypeId)
                .HasPrecision(10)
                .HasColumnName("HOSP_TYPE_ID");
            entity.Property(e => e.HospWebsite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOSP_WEBSITE");
            entity.Property(e => e.UpdateDtm)
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DTM");
            entity.Property(e => e.UpdateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("UPDATE_USER");
        });

        modelBuilder.Entity<UserMAuditor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_M_AUDITOR_PK");

            entity.ToTable("USER_M_AUDITOR");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Birthdate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("BIRTHDATE");
            entity.Property(e => e.CertNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CERT_NO");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.ExpireDate)
                .HasPrecision(6)
                .HasColumnName("EXPIRE_DATE");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IdenficationNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("IDENFICATION_NUMBER");
            entity.Property(e => e.ImageName)
                .HasColumnType("CLOB")
                .HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath)
                .HasColumnType("CLOB")
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POSITION");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.StartDate)
                .HasPrecision(6)
                .HasColumnName("START_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
        });

        modelBuilder.Entity<UserMHospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_M_HOSPITAL_PK");

            entity.ToTable("USER_M_HOSPITAL");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DISTRICT_CODE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GroupId)
                .HasPrecision(10)
                .HasColumnName("GROUP_ID");
            entity.Property(e => e.HospitalMCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HOSPITAL_M_CODE");
            entity.Property(e => e.HospitalMId)
                .HasPrecision(10)
                .HasColumnName("HOSPITAL_M_ID");
            entity.Property(e => e.IdenficationNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("IDENFICATION_NUMBER");
            entity.Property(e => e.ImageName)
                .HasColumnType("CLOB")
                .HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath)
                .HasColumnType("CLOB")
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCheckDopa)
                .HasPrecision(1)
                .HasColumnName("IS_CHECK_DOPA");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MedicalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MEDICAL_CODE");
            entity.Property(e => e.MedicalName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("MEDICAL_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Moo)
                .HasPrecision(10)
                .HasColumnName("MOO");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POSITION_NAME");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PROVINCE_CODE");
            entity.Property(e => e.SsoBranchCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("SSO_BRANCH_CODE");
            entity.Property(e => e.SubdistrictCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SUBDISTRICT_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ZIP_CODE");
        });

        modelBuilder.Entity<UserMSso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_M_SSO_PK");

            entity.ToTable("USER_M_SSO");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GroupId)
                .HasColumnType("NUMBER")
                .HasColumnName("GROUP_ID");
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("IDENTIFICATION_NUMBER");
            entity.Property(e => e.ImageName)
                .HasColumnType("CLOB")
                .HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath)
                .HasColumnType("CLOB")
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MOBILE");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.SsoBranchCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("SSO_BRANCH_CODE");
            entity.Property(e => e.SsoDeptId)
                .HasColumnType("NUMBER")
                .HasColumnName("SSO_DEPT_ID");
            entity.Property(e => e.SsoPositionId)
                .HasColumnType("NUMBER")
                .HasColumnName("SSO_POSITION_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("USER_TYPE");
        });

        modelBuilder.Entity<UserPrefixM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C007557");

            entity.ToTable("USER_PREFIX_M");

            entity.Property(e => e.Id)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasPrecision(10)
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasDefaultValueSql("SYSTIMESTAMP ")
                .HasColumnName("UPDATE_DATE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
