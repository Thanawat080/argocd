using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.fees.api.Entities.Dental;

public partial class DentalContext : DbContext
{
    public DentalContext()
    {
    }

    public DentalContext(DbContextOptions<DentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AaiDentalCarD> AaiDentalCarDs { get; set; }

    public virtual DbSet<AaiDentalCarH> AaiDentalCarHs { get; set; }

    public virtual DbSet<AaiDentalCheckD> AaiDentalCheckDs { get; set; }

    public virtual DbSet<AaiDentalCheckH> AaiDentalCheckHs { get; set; }

    public virtual DbSet<AaiDentalListM> AaiDentalListMs { get; set; }

    public virtual DbSet<AaiDentalPayOrderRunning> AaiDentalPayOrderRunnings { get; set; }

    public virtual DbSet<AaiDentalPayOrderT> AaiDentalPayOrderTs { get; set; }

    public virtual DbSet<AaiDentalToothTypeM> AaiDentalToothTypeMs { get; set; }

    public virtual DbSet<AaiDentalTreatD> AaiDentalTreatDs { get; set; }

    public virtual DbSet<AaiDentalWithdrawRunning> AaiDentalWithdrawRunnings { get; set; }

    public virtual DbSet<AaiDentalWithdrawT> AaiDentalWithdrawTs { get; set; }

    public virtual DbSet<AaiDentalYearBudget> AaiDentalYearBudgets { get; set; }

    public virtual DbSet<AdbMDistrict> AdbMDistricts { get; set; }

    public virtual DbSet<AdbMProvince> AdbMProvinces { get; set; }

    public virtual DbSet<AdbMSubdistrict> AdbMSubdistricts { get; set; }

    public virtual DbSet<HosHospital> HosHospitals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=aai_dental;Password=aai_dental;Data Source=192.168.10.69:1521/orcl;persist security info=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("AAI_DENTAL")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AaiDentalCarD>(entity =>
        {
            entity.HasKey(e => e.DentalCarDId).HasName("SYS_C009910");

            entity.ToTable("AAI_DENTAL_CAR_D");

            entity.Property(e => e.DentalCarDId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENTAL_CAR_D_ID");
            entity.Property(e => e.CarType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CAR_TYPE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DentalCarHId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENTAL_CAR_H_ID");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("LICENSE_PLATE");
            entity.Property(e => e.Remark)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("REMARK");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.DentalCarH).WithMany(p => p.AaiDentalCarDs)
                .HasForeignKey(d => d.DentalCarHId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_DENTAL_CAR_D_FK1");
        });

        modelBuilder.Entity<AaiDentalCarH>(entity =>
        {
            entity.HasKey(e => e.DentalCarHId).HasName("SYS_C009918");

            entity.ToTable("AAI_DENTAL_CAR_H");

            entity.Property(e => e.DentalCarHId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENTAL_CAR_H_ID");
            entity.Property(e => e.ChangeDesc)
                .HasColumnType("CLOB")
                .HasColumnName("CHANGE_DESC");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DantalCarStatus)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DANTAL_CAR_STATUS");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.PlaceDistrict)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PLACE_DISTRICT");
            entity.Property(e => e.PlaceName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PLACE_NAME");
            entity.Property(e => e.PlaceProvince)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PLACE_PROVINCE");
            entity.Property(e => e.PlaceSubDistrict)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PLACE_SUB_DISTRICT");
            entity.Property(e => e.RegisterDoc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("REGISTER_DOC");
            entity.Property(e => e.RegisterDocFileName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("REGISTER_DOC_FILE_NAME");
            entity.Property(e => e.ServiceDate)
                .HasColumnType("DATE")
                .HasColumnName("SERVICE_DATE");
            entity.Property(e => e.ServiceEndDate)
                .HasColumnType("DATE")
                .HasColumnName("SERVICE_END_DATE");
            entity.Property(e => e.ServiceStartDate)
                .HasColumnType("DATE")
                .HasColumnName("SERVICE_START_DATE");
            entity.Property(e => e.SsoOrgId)
                .HasPrecision(10)
                .HasColumnName("SSO_ORG_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiDentalCheckD>(entity =>
        {
            entity.HasKey(e => e.CheckDId).HasName("SYS_C009927");

            entity.ToTable("AAI_DENTAL_CHECK_D");

            entity.Property(e => e.CheckDId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECK_D_ID");
            entity.Property(e => e.CheckDate)
                .HasPrecision(6)
                .HasColumnName("CHECK_DATE");
            entity.Property(e => e.CheckHId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECK_H_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Decision)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DECISION");
            entity.Property(e => e.DentListId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENT_LIST_ID");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOCTOR_NAME");
            entity.Property(e => e.Expense)
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("EXPENSE");
            entity.Property(e => e.Icd10Id)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ICD10_ID");
            entity.Property(e => e.Icd9Id)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ICD9_ID");
            entity.Property(e => e.SsoPay)
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("SSO_PAY");
            entity.Property(e => e.ToothTypeId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("TOOTH_TYPE_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.CheckH).WithMany(p => p.AaiDentalCheckDs)
                .HasForeignKey(d => d.CheckHId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHECK_H_ID_CHECK_D");

            entity.HasOne(d => d.DentList).WithMany(p => p.AaiDentalCheckDs)
                .HasForeignKey(d => d.DentListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DENT_LIST_ID");

            entity.HasOne(d => d.ToothType).WithMany(p => p.AaiDentalCheckDs)
                .HasForeignKey(d => d.ToothTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TOOTH_TYPE_ID");
        });

        modelBuilder.Entity<AaiDentalCheckH>(entity =>
        {
            entity.HasKey(e => e.CheckHId).HasName("SYS_C009929");

            entity.ToTable("AAI_DENTAL_CHECK_H");

            entity.Property(e => e.CheckHId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECK_H_ID");
            entity.Property(e => e.BalanceMoney)
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("BALANCE_MONEY");
            entity.Property(e => e.BalanceUsed)
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("BALANCE_USED");
            entity.Property(e => e.BirthDate)
                .HasColumnType("DATE")
                .HasColumnName("BIRTH_DATE");
            entity.Property(e => e.CheckDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_DATE");
            entity.Property(e => e.CheckDocu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CHECK_DOCU");
            entity.Property(e => e.CheckStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CHECK_STATUS");
            entity.Property(e => e.ConfirmBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CONFIRM_BY");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DentalCarDId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENTAL_CAR_D_ID");
            entity.Property(e => e.HospitalId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20)")
                .HasColumnName("HOSPITAL_ID");
            entity.Property(e => e.IsFromReader)
                .HasPrecision(1)
                .HasColumnName("IS_FROM_READER");
            entity.Property(e => e.National)
                .HasPrecision(10)
                .HasColumnName("NATIONAL");
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PATIENT_NAME");
            entity.Property(e => e.PersonalId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NO");
            entity.Property(e => e.PortalPort)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PORTAL_PORT");
            entity.Property(e => e.Reason)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("REASON");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SEX");
            entity.Property(e => e.SsoOrgId)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL")
                .HasColumnName("SSO_ORG_ID");
            entity.Property(e => e.SsoStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SSO_STATUS");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.DentalCarD).WithMany(p => p.AaiDentalCheckHs)
                .HasForeignKey(d => d.DentalCarDId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DENTAL_CAR_D_ID");
        });

        modelBuilder.Entity<AaiDentalListM>(entity =>
        {
            entity.HasKey(e => e.DentListId).HasName("SYS_C009931");

            entity.ToTable("AAI_DENTAL_LIST_M");

            entity.Property(e => e.DentListId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENT_LIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DentDetail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DENT_DETAIL");
            entity.Property(e => e.DentFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DENT_FLAG");
            entity.Property(e => e.DentGroup)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DENT_GROUP");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiDentalPayOrderRunning>(entity =>
        {
            entity.HasKey(e => e.PayRunId).HasName("AAI_DENTAL_PAY_ORDER_RUNNING_PK");

            entity.ToTable("AAI_DENTAL_PAY_ORDER_RUNNING");

            entity.Property(e => e.PayRunId)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("PAY_RUN_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.PayRunNumber)
                .HasPrecision(10)
                .HasColumnName("PAY_RUN_NUMBER");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.YearMonth)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("YEAR_MONTH");
        });

        modelBuilder.Entity<AaiDentalPayOrderT>(entity =>
        {
            entity.HasKey(e => e.PayOrderId).HasName("AAI_DENTAL_PAY_ORDER_T_PK");

            entity.ToTable("AAI_DENTAL_PAY_ORDER_T");

            entity.Property(e => e.PayOrderId)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("PAY_ORDER_ID");
            entity.Property(e => e.ApproveBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("APPROVE_BY");
            entity.Property(e => e.ApproveDate)
                .HasPrecision(6)
                .HasColumnName("APPROVE_DATE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DentGroup)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DENT_GROUP");
            entity.Property(e => e.HospitalId)
                .HasPrecision(10)
                .HasColumnName("HOSPITAL_ID");
            entity.Property(e => e.PayOrderAmt)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("PAY_ORDER_AMT");
            entity.Property(e => e.PayOrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAY_ORDER_NO");
            entity.Property(e => e.PayOrderSetNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAY_ORDER_SET_NO");
            entity.Property(e => e.PayOrderStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAY_ORDER_STATUS");
            entity.Property(e => e.PersonalId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.SignBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SIGN_BY");
            entity.Property(e => e.SignDate)
                .HasPrecision(6)
                .HasColumnName("SIGN_DATE");
            entity.Property(e => e.SsoOrgCode)
                .HasPrecision(10)
                .HasColumnName("SSO_ORG_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.WithdrawalNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("WITHDRAWAL_NO");

            entity.HasOne(d => d.WithdrawalNoNavigation).WithMany(p => p.AaiDentalPayOrderTs)
                .HasPrincipalKey(p => p.WithdrawNo)
                .HasForeignKey(d => d.WithdrawalNo)
                .HasConstraintName("AAI_DENTAL_PAY_ORDER_T_FK1");
        });

        modelBuilder.Entity<AaiDentalToothTypeM>(entity =>
        {
            entity.HasKey(e => e.ToothTypeId).HasName("SYS_C009961");

            entity.ToTable("AAI_DENTAL_TOOTH_TYPE_M");

            entity.Property(e => e.ToothTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("TOOTH_TYPE_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.ToothName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TOOTH_NAME");
            entity.Property(e => e.ToothNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TOOTH_NO");
            entity.Property(e => e.ToothType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TOOTH_TYPE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiDentalTreatD>(entity =>
        {
            entity.HasKey(e => e.TreatId).HasName("AAI_DENTAL_TREAT_D_PK");

            entity.ToTable("AAI_DENTAL_TREAT_D");

            entity.Property(e => e.TreatId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20)")
                .HasColumnName("TREAT_ID");
            entity.Property(e => e.CheckHId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECK_H_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DentListId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DENT_LIST_ID");
            entity.Property(e => e.Expense)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("EXPENSE");
            entity.Property(e => e.SsoPay)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("SSO_PAY");
            entity.Property(e => e.ToothItem)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER")
                .HasColumnName("TOOTH_ITEM");
            entity.Property(e => e.ToothNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("TOOTH_NUMBER");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.CheckH).WithMany(p => p.AaiDentalTreatDs)
                .HasForeignKey(d => d.CheckHId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_DENTAL_TREAT_D_FK1");

            entity.HasOne(d => d.DentList).WithMany(p => p.AaiDentalTreatDs)
                .HasForeignKey(d => d.DentListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_DENTAL_TREAT_D_FK2");
        });

        modelBuilder.Entity<AaiDentalWithdrawRunning>(entity =>
        {
            entity.HasKey(e => e.DocuId).HasName("TABLE1_PK");

            entity.ToTable("AAI_DENTAL_WITHDRAW_RUNNING");

            entity.Property(e => e.DocuId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DOCU_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DocuLastNo)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("DOCU_LAST_NO");
            entity.Property(e => e.DocuType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DOCU_TYPE");
            entity.Property(e => e.SsoOrgCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SSO_ORG_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.YearMonth)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("YEAR_MONTH");
        });

        modelBuilder.Entity<AaiDentalWithdrawT>(entity =>
        {
            entity.HasKey(e => e.WithdrawId).HasName("AAI_DENTAL_WITHDRAW_T_PK");

            entity.ToTable("AAI_DENTAL_WITHDRAW_T");

            entity.HasIndex(e => e.WithdrawNo, "AAI_DENTAL_WITHDRAW_T_UK1").IsUnique();

            entity.Property(e => e.WithdrawId)
                .HasPrecision(10)
                .ValueGeneratedNever()
                .HasColumnName("WITHDRAW_ID");
            entity.Property(e => e.CheckDId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECK_D_ID");
            entity.Property(e => e.CheckHId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECK_H_ID");
            entity.Property(e => e.CheckStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CHECK_STATUS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Expense)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("EXPENSE");
            entity.Property(e => e.SsoPay)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("SSO_PAY");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.WithdrawDate)
                .HasPrecision(6)
                .HasColumnName("WITHDRAW_DATE");
            entity.Property(e => e.WithdrawNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("WITHDRAW_NO");

            entity.HasOne(d => d.CheckD).WithMany(p => p.AaiDentalWithdrawTs)
                .HasForeignKey(d => d.CheckDId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_DENTAL_WITHDRAW_T_FK1");

            entity.HasOne(d => d.CheckH).WithMany(p => p.AaiDentalWithdrawTs)
                .HasForeignKey(d => d.CheckHId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_DENTAL_WITHDRAW_T_FK2");
        });

        modelBuilder.Entity<AaiDentalYearBudget>(entity =>
        {
            entity.HasKey(e => e.BudgetId).HasName("SYS_C0010006");

            entity.ToTable("AAI_DENTAL_YEAR_BUDGET");

            entity.Property(e => e.BudgetId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20)")
                .HasColumnName("BUDGET_ID");
            entity.Property(e => e.BudgetAmount)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("BUDGET_AMOUNT");
            entity.Property(e => e.BudgetYear)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NULL")
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .ValueGeneratedOnAdd()
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AdbMDistrict>(entity =>
        {
            entity.HasKey(e => e.DistId).HasName("ADB_M_DISTRICT_PK");

            entity.ToTable("ADB_M_DISTRICT", "BTDEV2");

            entity.Property(e => e.DistId)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL")
                .HasColumnName("DIST_ID");
            entity.Property(e => e.CreateDtm)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DTM");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.DistCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .IsFixedLength()
                .HasColumnName("DIST_CODE");
            entity.Property(e => e.DistName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("DIST_NAME");
            entity.Property(e => e.DistStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .IsFixedLength()
                .HasColumnName("DIST_STATUS");
            entity.Property(e => e.ProvId)
                .HasPrecision(10)
                .ValueGeneratedOnAdd()
                .HasColumnName("PROV_ID");
            entity.Property(e => e.UpdateDtm)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DTM");
            entity.Property(e => e.UpdateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("UPDATE_USER");
        });

        modelBuilder.Entity<AdbMProvince>(entity =>
        {
            entity.HasKey(e => e.ProvId).HasName("ADB_M_PROVINCE_PK");

            entity.ToTable("ADB_M_PROVINCE", "BTDEV2");

            entity.Property(e => e.ProvId)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL")
                .HasColumnName("PROV_ID");
            entity.Property(e => e.CreateDtm)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DTM");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.ProvCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .IsFixedLength()
                .HasColumnName("PROV_CODE");
            entity.Property(e => e.ProvName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("PROV_NAME");
            entity.Property(e => e.ProvStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .IsFixedLength()
                .HasColumnName("PROV_STATUS");
            entity.Property(e => e.UpdateDtm)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DTM");
            entity.Property(e => e.UpdateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("UPDATE_USER");
        });

        modelBuilder.Entity<AdbMSubdistrict>(entity =>
        {
            entity.HasKey(e => e.SubdistId).HasName("ADB_M_SUBDISTRICT_PK");

            entity.ToTable("ADB_M_SUBDISTRICT", "BTDEV2");

            entity.Property(e => e.SubdistId)
                .HasPrecision(10)
                .HasDefaultValueSql("NULL")
                .HasColumnName("SUBDIST_ID");
            entity.Property(e => e.CreateDtm)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DTM");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("CREATE_USER");
            entity.Property(e => e.DistId)
                .HasPrecision(10)
                .ValueGeneratedOnAdd()
                .HasColumnName("DIST_ID");
            entity.Property(e => e.SubdistCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .IsFixedLength()
                .HasColumnName("SUBDIST_CODE");
            entity.Property(e => e.SubdistName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("SUBDIST_NAME");
            entity.Property(e => e.SubdistStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .IsFixedLength()
                .HasColumnName("SUBDIST_STATUS");
            entity.Property(e => e.UpdateDtm)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DTM");
            entity.Property(e => e.UpdateUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("UPDATE_USER");
        });

        modelBuilder.Entity<HosHospital>(entity =>
        {
            entity.HasKey(e => e.HospId).HasName("HOS_HOSPITAL_PK");

            entity.ToTable("HOS_HOSPITAL", "BTDEV2");

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
        modelBuilder.HasSequence("ADB_AAI_PAY_SEQ", "BTDEV2");
        modelBuilder.HasSequence("ADB_AAI_PAY_SEQ1", "BTDEV2");
        modelBuilder.HasSequence("ADB_M_DISTRICT_SEQ", "BTDEV2");
        modelBuilder.HasSequence("ADB_M_PROVINCE_SEQ", "BTDEV2");
        modelBuilder.HasSequence("ADB_M_SUBDISTRICT_SEQ", "BTDEV2");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
