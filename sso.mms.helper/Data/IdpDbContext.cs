using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.helper.Data;

public partial class IdpDbContext : DbContext
{
    public IdpDbContext()
    {
    }

    public IdpDbContext(DbContextOptions<IdpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditorUserM> AuditorUserMs { get; set; }

    public virtual DbSet<DepartmentM> DepartmentMs { get; set; }

    public virtual DbSet<DistrictM> DistrictMs { get; set; }

    public virtual DbSet<HospitalUserM> HospitalUserMs { get; set; }

    public virtual DbSet<MSsoBranch> MSsoBranches { get; set; }

    public virtual DbSet<PositionM> PositionMs { get; set; }

    public virtual DbSet<PrefixM> PrefixMs { get; set; }

    public virtual DbSet<ProvinceM> ProvinceMs { get; set; }

    public virtual DbSet<RoleAppM> RoleAppMs { get; set; }

    public virtual DbSet<RoleGroupListT> RoleGroupListTs { get; set; }

    public virtual DbSet<RoleGroupM> RoleGroupMs { get; set; }

    public virtual DbSet<RoleMenuM> RoleMenuMs { get; set; }

    public virtual DbSet<RoleMenuMapping> RoleMenuMappings { get; set; }

    public virtual DbSet<RoleSystemM> RoleSystemMs { get; set; }

    public virtual DbSet<RoleUserMapping> RoleUserMappings { get; set; }

    public virtual DbSet<SessionUserT> SessionUserTs { get; set; }

    public virtual DbSet<SettingM> SettingMs { get; set; }

    public virtual DbSet<SsoOtpT> SsoOtpTs { get; set; }

    public virtual DbSet<SsoUserM> SsoUserMs { get; set; }

    public virtual DbSet<SubdistrictM> SubdistrictMs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditorUserM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AUDITOR_USER_M_pkey");

            entity.ToTable("AUDITOR_USER_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("BIRTHDATE");
            entity.Property(e => e.CertNo)
                .HasMaxLength(20)
                .HasColumnName("CERT_NO");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.ExpireDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("EXPIRE_DATE");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IdenficationNumber)
                .HasMaxLength(13)
                .HasColumnName("IDENFICATION_NUMBER");
            entity.Property(e => e.ImageName).HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath).HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("0")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("POSITION");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.RoleGroupMId).HasColumnName("ROLE_GROUP_M_ID");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("START_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.RoleGroupM).WithMany(p => p.AuditorUserMs)
                .HasForeignKey(d => d.RoleGroupMId)
                .HasConstraintName("AUDITOR_USER_M_ROLE_GROUP_M_ID_fkey");
        });

        modelBuilder.Entity<DepartmentM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DEPARTMENT_M_pkey");

            entity.ToTable("DEPARTMENT_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("1")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<DistrictM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DISTRICT_M_pkey");

            entity.ToTable("DISTRICT_M");

            entity.HasIndex(e => e.Code, "DISTRICT_CODE_FK").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("NAME_EN");
            entity.Property(e => e.NameTh)
                .HasMaxLength(50)
                .HasColumnName("NAME_TH");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(10)
                .HasColumnName("PROVINCE_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.DistrictMs)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.ProvinceCode)
                .HasConstraintName("DISTRICT_M_PROVINCE_CODE_fk");
        });

        modelBuilder.Entity<HospitalUserM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HOSPITAL_USER_M_pkey");

            entity.ToTable("HOSPITAL_USER_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(10)
                .HasColumnName("DISTRICT_CODE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.HospitalMId).HasColumnName("HOSPITAL_M_ID");
            entity.Property(e => e.IdenficationNumber)
                .HasMaxLength(13)
                .HasColumnName("IDENFICATION_NUMBER");
            entity.Property(e => e.ImageName).HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath).HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCheckDopa).HasColumnName("IS_CHECK_DOPA");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("0")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.LazerCode)
                .HasMaxLength(12)
                .HasColumnName("LAZER_CODE");
            entity.Property(e => e.MedicalCode)
                .HasMaxLength(10)
                .HasColumnName("MEDICAL_CODE");
            entity.Property(e => e.MedicalName)
                .HasMaxLength(150)
                .HasColumnName("MEDICAL_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Moo).HasColumnName("MOO");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .HasColumnName("POSITION_NAME");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(10)
                .HasColumnName("PROVINCE_CODE");
            entity.Property(e => e.SsoBranchCode)
                .HasMaxLength(4)
                .HasColumnName("SSO_BRANCH_CODE");
            entity.Property(e => e.SubdistrictCode)
                .HasMaxLength(10)
                .HasColumnName("SUBDISTRICT_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("User_Name");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .HasColumnName("ZIP_CODE");

            entity.HasOne(d => d.DistrictCodeNavigation).WithMany(p => p.HospitalUserMs)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.DistrictCode)
                .HasConstraintName("HOSPITAL_USER_M_DISTRICT_CODE_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.HospitalUserMs)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("HOSPITAL_USER_M_GROUP_ID_fkey");

            entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.HospitalUserMs)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.ProvinceCode)
                .HasConstraintName("HOSPITAL_USER_M_PROVINCE_CODE_fkey");

            entity.HasOne(d => d.SubdistrictCodeNavigation).WithMany(p => p.HospitalUserMs)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.SubdistrictCode)
                .HasConstraintName("HOSPITAL_USER_M_SUBDISTRICT_CODE_fkey");
        });

        modelBuilder.Entity<MSsoBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("M_SSO_BRANCH_pkey");

            entity.ToTable("M_SSO_BRANCH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(1000)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Code)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("CODE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("CURRENT_USER")
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .HasColumnName("FAX");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("1")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Moo).HasColumnName("MOO");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.SubDistrictId).HasColumnName("SUB_DISTRICT_ID");
            entity.Property(e => e.Tel)
                .HasMaxLength(150)
                .HasColumnName("TEL");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("CURRENT_USER")
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("UPDATED_DATE");
            entity.Property(e => e.ZipCode).HasColumnName("ZIP_CODE");
        });

        modelBuilder.Entity<PositionM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("POSITION_M_pkey");

            entity.ToTable("POSITION_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("1")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<PrefixM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Prefix_M_pkey");

            entity.ToTable("PREFIX_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("1")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<ProvinceM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PROVINCE_M_pkey");

            entity.ToTable("PROVINCE_M");

            entity.HasIndex(e => e.Code, "PROVINCE_CODE_FK").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("NAME_EN");
            entity.Property(e => e.NameTh)
                .HasMaxLength(50)
                .HasColumnName("NAME_TH");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<RoleAppM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_APP_M_pkey");

            entity.ToTable("ROLE_APP_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppCode)
                .HasMaxLength(20)
                .HasColumnName("APP_CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasMaxLength(50)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.SystemDesc)
                .HasMaxLength(500)
                .HasColumnName("SYSTEM_DESC");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<RoleGroupListT>(entity =>
        {
            entity.HasKey(e => new { e.RoleGroupMId, e.RoleMenuMId }).HasName("ROLE_GROUP_LIST_T_pkey");

            entity.ToTable("ROLE_GROUP_LIST_T");

            entity.Property(e => e.RoleGroupMId).HasColumnName("ROLE_GROUP_M_ID");
            entity.Property(e => e.RoleMenuMId).HasColumnName("ROLE_MENU_M_ID");
            entity.Property(e => e.CreateBy)
                .HasColumnType("character varying")
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsRoleApprove).HasColumnName("IS_ROLE_APPROVE");
            entity.Property(e => e.IsRoleCancel).HasColumnName("IS_ROLE_CANCEL");
            entity.Property(e => e.IsRoleCreate).HasColumnName("IS_ROLE_CREATE");
            entity.Property(e => e.IsRoleDelete).HasColumnName("IS_ROLE_DELETE");
            entity.Property(e => e.IsRolePrint).HasColumnName("IS_ROLE_PRINT");
            entity.Property(e => e.IsRoleRead).HasColumnName("IS_ROLE_READ");
            entity.Property(e => e.IsRoleUpdate).HasColumnName("IS_ROLE_UPDATE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.UpdateBy)
                .HasColumnType("character varying")
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.RoleGroupM).WithMany(p => p.RoleGroupListTs)
                .HasForeignKey(d => d.RoleGroupMId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ROLE_GROUP_LIST_T_ROLE_GROUP_M_ID_fkey");

            entity.HasOne(d => d.RoleMenuM).WithMany(p => p.RoleGroupListTs)
                .HasForeignKey(d => d.RoleMenuMId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ROLE_GROUP_LIST_T_ROLE_MENU_M_ID_fkey");
        });

        modelBuilder.Entity<RoleGroupM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_GROUP_M_pkey");

            entity.ToTable("ROLE_GROUP_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("NAME");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(20)
                .HasColumnName("ROLE_CODE");
            entity.Property(e => e.RoleDesc)
                .HasMaxLength(500)
                .HasColumnName("ROLE_DESC");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserGroup)
                .HasMaxLength(10)
                .HasColumnName("USER_GROUP");
        });

        modelBuilder.Entity<RoleMenuM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_MENU_M_pkey");

            entity.ToTable("ROLE_MENU_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppCode)
                .HasMaxLength(20)
                .HasColumnName("APP_CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("1")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.MenuCode)
                .HasMaxLength(20)
                .HasColumnName("MENU_CODE");
            entity.Property(e => e.MenuDesc)
                .HasMaxLength(500)
                .HasColumnName("MENU_DESC");
            entity.Property(e => e.MenuName)
                .HasMaxLength(50)
                .HasColumnName("MENU_NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<RoleMenuMapping>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("ROLE_MENU_MAPPING_pkey");

            entity.ToTable("ROLE_MENU_MAPPING");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("USER_ID");
            entity.Property(e => e.RoleGroupId).HasColumnName("ROLE_GROUP_ID");
            entity.Property(e => e.UserGroup)
                .HasMaxLength(10)
                .HasColumnName("USER_GROUP");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.RoleGroup).WithMany(p => p.RoleMenuMappings)
                .HasForeignKey(d => d.RoleGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ROLE_GROUP_ID_fk");
        });

        modelBuilder.Entity<RoleSystemM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_SYSTEM_M_pkey");

            entity.ToTable("ROLE_SYSTEM_M");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasColumnType("character varying")
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.SystemCode)
                .HasColumnType("character varying")
                .HasColumnName("SYSTEM_CODE");
            entity.Property(e => e.SystemDesc)
                .HasColumnType("character varying")
                .HasColumnName("SYSTEM_DESC");
            entity.Property(e => e.UpdateBy)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("character varying")
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<RoleUserMapping>(entity =>
        {
            entity.HasKey(e => new { e.RoleGroupId, e.UserName }).HasName("ROLE_USER_MAPPING_pkey");

            entity.ToTable("ROLE_USER_MAPPING");

            entity.Property(e => e.RoleGroupId).HasColumnName("ROLE_GROUP_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("User_Name");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .HasColumnName("USER_TYPE");

            entity.HasOne(d => d.RoleGroup).WithMany(p => p.RoleUserMappings)
                .HasForeignKey(d => d.RoleGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ROLE_GROUP_ID_FK");
        });

        modelBuilder.Entity<SessionUserT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SESSION_USER_T_pkey");

            entity.ToTable("SESSION_USER_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccessToken)
                .HasMaxLength(50000)
                .HasColumnName("ACCESS_TOKEN");
            entity.Property(e => e.AccessType)
                .HasMaxLength(20)
                .HasColumnName("ACCESS_TYPE");
            entity.Property(e => e.AuditorUserMId).HasColumnName("AUDITOR_USER_M_ID");
            entity.Property(e => e.ChannelLogin)
                .HasMaxLength(50)
                .HasColumnName("CHANNEL_LOGIN");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalUserMId).HasColumnName("HOSPITAL_USER_M_ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.RealmGroup)
                .HasMaxLength(50)
                .HasColumnName("REALM_GROUP");
            entity.Property(e => e.ShortToken)
                .HasMaxLength(50000)
                .HasColumnName("SHORT_TOKEN");
            entity.Property(e => e.SsoUserMId).HasColumnName("SSO_USER_M_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.AuditorUserM).WithMany(p => p.SessionUserTs)
                .HasForeignKey(d => d.AuditorUserMId)
                .HasConstraintName("AUDITOR_USER_M_ID_fk");

            entity.HasOne(d => d.HospitalUserM).WithMany(p => p.SessionUserTs)
                .HasForeignKey(d => d.HospitalUserMId)
                .HasConstraintName("HOSPITAL_USER_M_ID_fk");

            entity.HasOne(d => d.SsoUserM).WithMany(p => p.SessionUserTs)
                .HasForeignKey(d => d.SsoUserMId)
                .HasConstraintName("SSO_USER_M_ID_fk");
        });

        modelBuilder.Entity<SettingM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SETTING_M_pkey");

            entity.ToTable("SETTING_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("true")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Isstatus)
                .HasDefaultValueSql("1")
                .HasColumnName("ISSTATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<SsoOtpT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SSO_OTP_T_pkey");

            entity.ToTable("SSO_OTP_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Mobile)
                .HasMaxLength(10)
                .HasColumnName("MOBILE");
            entity.Property(e => e.OtpCode)
                .HasMaxLength(8)
                .HasColumnName("OTP_CODE");
            entity.Property(e => e.OtpType)
                .HasMaxLength(20)
                .HasColumnName("OTP_TYPE");
            entity.Property(e => e.RealmGroup)
                .HasMaxLength(30)
                .HasColumnName("REALM_GROUP");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<SsoUserM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SSO_USER_M_pkey");

            entity.ToTable("SSO_USER_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(13)
                .HasColumnName("IDENTIFICATION_NUMBER");
            entity.Property(e => e.ImageName).HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath).HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("0")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.SsoBranchCode)
                .HasMaxLength(4)
                .HasColumnName("SSO_BRANCH_CODE");
            entity.Property(e => e.SsoDeptId).HasColumnName("SSO_DEPT_ID");
            entity.Property(e => e.SsoPersonFieldPosition)
                .HasMaxLength(500)
                .HasColumnName("SSO_PERSON_FIELD_POSITION");
            entity.Property(e => e.SsoPositionId).HasColumnName("SSO_POSITION_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .HasColumnName("USER_TYPE");
            entity.Property(e => e.WorkingdeptDescription)
                .HasMaxLength(500)
                .HasColumnName("WORKINGDEPT_DESCRIPTION");

            entity.HasOne(d => d.Group).WithMany(p => p.SsoUserMs)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("SSO_USER_M_GROUP_ID_fkey");
        });

        modelBuilder.Entity<SubdistrictM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SUBDISTRICT_M_pkey");

            entity.ToTable("SUBDISTRICT_M");

            entity.HasIndex(e => e.Code, "SUBDISTRICT_CODE_FK").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(10)
                .HasColumnName("DISTRICT_CODE");
            entity.Property(e => e.Latitude).HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("NAME_EN");
            entity.Property(e => e.NameTh)
                .HasMaxLength(50)
                .HasColumnName("NAME_TH");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.DistrictCodeNavigation).WithMany(p => p.SubdistrictMs)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.DistrictCode)
                .HasConstraintName("SUBDISTRICT_M_DISTRICT_CODE_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
