using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.fees.api.Entities.PromoteHealth;

public partial class PromoteHealthContext : DbContext
{
    public PromoteHealthContext()
    {
    }

    public PromoteHealthContext(DbContextOptions<PromoteHealthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AaiHealthAiDescriptionM> AaiHealthAiDescriptionMs { get; set; }

    public virtual DbSet<AaiHealthBudgetYearM> AaiHealthBudgetYearMs { get; set; }

    public virtual DbSet<AaiHealthChecklistD> AaiHealthChecklistDs { get; set; }

    public virtual DbSet<AaiHealthChecklistM> AaiHealthChecklistMs { get; set; }

    public virtual DbSet<AaiHealthCheckupH> AaiHealthCheckupHs { get; set; }

    public virtual DbSet<AaiHealthCheckupListT> AaiHealthCheckupListTs { get; set; }

    public virtual DbSet<AaiHealthCheckupResultT> AaiHealthCheckupResultTs { get; set; }

    public virtual DbSet<AaiHealthMonthBudyearD> AaiHealthMonthBudyearDs { get; set; }

    public virtual DbSet<AaiHealthPayOrderT> AaiHealthPayOrderTs { get; set; }

    public virtual DbSet<AaiHealthReserveH> AaiHealthReserveHs { get; set; }

    public virtual DbSet<AaiHealthSetRefChecklistCfg> AaiHealthSetRefChecklistCfgs { get; set; }

    public virtual DbSet<AaiHealthSetRefDoctorM> AaiHealthSetRefDoctorMs { get; set; }

    public virtual DbSet<AaiHealthSetRefNicknameCfg> AaiHealthSetRefNicknameCfgs { get; set; }

    public virtual DbSet<AaiHealthSetUnderlyingDiseaseM> AaiHealthSetUnderlyingDiseaseMs { get; set; }

    public virtual DbSet<AaiHealthWithdrawalH> AaiHealthWithdrawalHs { get; set; }

    public virtual DbSet<AaiHealthWithdrawalT> AaiHealthWithdrawalTs { get; set; }

    public virtual DbSet<HosHospital> HosHospitals { get; set; }

    public virtual DbSet<ViewAaiHealthCheckupListT> ViewAaiHealthCheckupListTs { get; set; }

    public virtual DbSet<ViewAaiHealthWithdrawalT> ViewAaiHealthWithdrawalTs { get; set; }

    public virtual DbSet<ViewExportListT> ViewExportListTs { get; set; }

    public virtual DbSet<ViewMontbudyear> ViewMontbudyears { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("BTDEV3")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AaiHealthAiDescriptionM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AI_DESCRIPTION_M_PK");

            entity.ToTable("AAI_HEALTH_AI_DESCRIPTION_M");

            entity.Property(e => e.Id)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasPrecision(1)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("IS_STATUS");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiHealthBudgetYearM>(entity =>
        {
            entity.HasKey(e => e.BudgetYear).HasName("AAI_HEALTH_BUDGET_YEAR_M_PK");

            entity.ToTable("AAI_HEALTH_BUDGET_YEAR_M");

            entity.Property(e => e.BudgetYear)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.BudgetYearStatus)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("BUDGET_YEAR_STATUS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiHealthChecklistD>(entity =>
        {
            entity.HasKey(e => e.ChecklistDtId).HasName("SYS_C008039");

            entity.ToTable("AAI_HEALTH_CHECKLIST_D");

            entity.Property(e => e.ChecklistDtId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_DT_ID");
            entity.Property(e => e.ChecklistDtName)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("CHECKLIST_DT_NAME");
            entity.Property(e => e.ChecklistDtStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("CHECKLIST_DT_STATUS");
            entity.Property(e => e.ChecklistId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsOption)
                .HasPrecision(1)

                .HasColumnName("IS_OPTION");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.Checklist).WithMany(p => p.AaiHealthChecklistDs)
                .HasForeignKey(d => d.ChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_CHECKLIST_DT_M_FK1");
        });

        modelBuilder.Entity<AaiHealthChecklistM>(entity =>
        {
            entity.HasKey(e => e.ChecklistId).HasName("SYS_C008033");

            entity.ToTable("AAI_HEALTH_CHECKLIST_M");

            entity.Property(e => e.ChecklistId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.BudgetYear)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.ChecklistCode)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CHECKLIST_CODE");
            entity.Property(e => e.ChecklistName)
                .HasMaxLength(200)
                .IsUnicode(false)

                .HasColumnName("CHECKLIST_NAME");
            entity.Property(e => e.ChecklistPrice)

                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("CHECKLIST_PRICE");
            entity.Property(e => e.ChecklistShortname)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("CHECKLIST_SHORTNAME");
            entity.Property(e => e.ChecklistStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("CHECKLIST_STATUS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsSetRef)
                .HasPrecision(1)

                .HasColumnName("IS_SET_REF");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiHealthCheckupH>(entity =>
        {
            entity.HasKey(e => e.CheckupId).HasName("SYS_C008020");

            entity.ToTable("AAI_HEALTH_CHECKUP_H");

            entity.HasIndex(e => e.CheckupNo, "UNIQUE_CONSTRAINT_CHECKUP_NO").IsUnique();

            entity.Property(e => e.CheckupId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_ID");
            entity.Property(e => e.BudgetYear)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.CheckupDate)

                .HasColumnType("DATE")
                .HasColumnName("CHECKUP_DATE");
            entity.Property(e => e.CheckupNo)
                .HasMaxLength(15)
                .IsUnicode(false)

                .HasColumnName("CHECKUP_NO");
            entity.Property(e => e.ConfirmBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CONFIRM_BY");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DeleteStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .HasDefaultValueSql("'I' ")
                .IsFixedLength()
                .HasColumnName("DELETE_STATUS");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.IsFromReader)
                .HasPrecision(1)

                .HasColumnName("IS_FROM_READER");
            entity.Property(e => e.IsUd)
                .HasPrecision(1)

                .HasColumnName("IS_UD");
            entity.Property(e => e.MonthBudyear)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("MONTH_BUDYEAR");
            entity.Property(e => e.PatientAge)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("PATIENT_AGE");
            entity.Property(e => e.PatientHeight)

                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("PATIENT_HEIGHT");
            entity.Property(e => e.PatientName)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("PATIENT_NAME");
            entity.Property(e => e.PatientPressure)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("PATIENT_PRESSURE");
            entity.Property(e => e.PatientSex)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("PATIENT_SEX");
            entity.Property(e => e.PatientSurname)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("PATIENT_SURNAME");
            entity.Property(e => e.PatientTel)
                .HasMaxLength(30)
                .IsUnicode(false)

                .HasColumnName("PATIENT_TEL");
            entity.Property(e => e.PatientWeight)

                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("PATIENT_WEIGHT");
            entity.Property(e => e.PersonalId)
                .HasMaxLength(13)
                .IsUnicode(false)

                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.ReadBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("READ_BY");
            entity.Property(e => e.ReadDate)

                .HasColumnType("DATE")
                .HasColumnName("READ_DATE");
            entity.Property(e => e.Reason)
                .HasMaxLength(200)
                .IsUnicode(false)

                .HasColumnName("REASON");
            entity.Property(e => e.Remark)
                .HasMaxLength(200)
                .IsUnicode(false)

                .HasColumnName("REMARK");
            entity.Property(e => e.ReserveId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("RESERVE_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UseStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("USE_STATUS");

            entity.HasOne(d => d.Reserve).WithMany(p => p.AaiHealthCheckupHs)
                .HasForeignKey(d => d.ReserveId)
                .HasConstraintName("AAI_HEA_CHECKUP_H_T_FK1");
        });

        modelBuilder.Entity<AaiHealthCheckupListT>(entity =>
        {
            entity.HasKey(e => e.CheckupListId).HasName("SYS_C008045");

            entity.ToTable("AAI_HEALTH_CHECKUP_LIST_T");

            entity.Property(e => e.CheckupListId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_LIST_ID");
            entity.Property(e => e.ChecklistId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.CheckupId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsChecked)
                .HasPrecision(1)

                .HasColumnName("IS_CHECKED");
            entity.Property(e => e.IsExport)
                .IsRequired()
                .HasPrecision(1)

                .HasDefaultValueSql("0 ")
                .HasColumnName("IS_EXPORT");
            entity.Property(e => e.ResultStatus)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("RESULT_STATUS");
            entity.Property(e => e.SetRefDoctorId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("SET_REF_DOCTOR_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.Checklist).WithMany(p => p.AaiHealthCheckupListTs)
                .HasForeignKey(d => d.ChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEALTH_CHECKUP_LIST_T_FK1");

            entity.HasOne(d => d.Checkup).WithMany(p => p.AaiHealthCheckupListTs)
                .HasForeignKey(d => d.CheckupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEALTH_CHECKUP_LIST_T_FK2");

            entity.HasOne(d => d.SetRefDoctor).WithMany(p => p.AaiHealthCheckupListTs)
                .HasForeignKey(d => d.SetRefDoctorId)
                .HasConstraintName("AAI_HEALTH_CHECKUP_LIST_T_FK3");
        });

        modelBuilder.Entity<AaiHealthCheckupResultT>(entity =>
        {
            entity.HasKey(e => e.CheckupResultId).HasName("SYS_C008086");

            entity.ToTable("AAI_HEALTH_CHECKUP_RESULT_T");

            entity.Property(e => e.CheckupResultId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_RESULT_ID");
            entity.Property(e => e.ChecklistDtId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_DT_ID");
            entity.Property(e => e.CheckupListId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_LIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsNormal)
                .HasPrecision(1)

                .HasColumnName("IS_NORMAL");
            entity.Property(e => e.RefValue)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("REF_VALUE");
            entity.Property(e => e.ResultCheckValue)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("RESULT_CHECK_VALUE");
            entity.Property(e => e.Suggession)
                .HasMaxLength(200)
                .IsUnicode(false)

                .HasColumnName("SUGGESSION");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.ChecklistDt).WithMany(p => p.AaiHealthCheckupResultTs)
                .HasForeignKey(d => d.ChecklistDtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_CHECKUP_RESULT_T_FK1");

            entity.HasOne(d => d.CheckupList).WithMany(p => p.AaiHealthCheckupResultTs)
                .HasForeignKey(d => d.CheckupListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_CHECKUP_RESULT_T_FK2");
        });

        modelBuilder.Entity<AaiHealthMonthBudyearD>(entity =>
        {
            entity.HasKey(e => new { e.MonthBudyear, e.BudgetYear }).HasName("AAI_HEALTH_MONTH_BUDYEAR_D_PK");

            entity.ToTable("AAI_HEALTH_MONTH_BUDYEAR_D");

            entity.Property(e => e.MonthBudyear)
                .HasPrecision(4)
                .HasColumnName("MONTH_BUDYEAR");
            entity.Property(e => e.BudgetYear)
                .HasPrecision(4)
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiHealthPayOrderT>(entity =>
        {
            entity.HasKey(e => e.PayOrderId).HasName("SYS_C008078");

            entity.ToTable("AAI_HEALTH_PAY_ORDER_T");

            entity.Property(e => e.PayOrderId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
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
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.PayAmount)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("PAY_AMOUNT");
            entity.Property(e => e.PayOrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("PAY_ORDER_NO");
            entity.Property(e => e.PayOrderRisk)
                .IsRequired()
                .HasPrecision(1)

                .HasDefaultValueSql("0 ")
                .HasColumnName("PAY_ORDER_RISK");
            entity.Property(e => e.PayOrderSetNo)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("PAY_ORDER_SET_NO");
            entity.Property(e => e.PayOrderStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("PAY_ORDER_STATUS");
            entity.Property(e => e.SignBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("SIGN_BY");
            entity.Property(e => e.SignDate)
                .HasPrecision(6)

                .HasColumnName("SIGN_DATE");
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
        });

        modelBuilder.Entity<AaiHealthReserveH>(entity =>
        {
            entity.HasKey(e => e.ReserveId).HasName("RESERVE_M_PK");

            entity.ToTable("AAI_HEALTH_RESERVE_H");

            entity.Property(e => e.ReserveId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("RESERVE_ID");
            entity.Property(e => e.CancleBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CANCLE_BY");
            entity.Property(e => e.CancleDate)

                .HasColumnType("DATE")
                .HasColumnName("CANCLE_DATE");
            entity.Property(e => e.CompanyAccNo)
                .HasMaxLength(10)
                .IsUnicode(false)

                .HasColumnName("COMPANY_ACC_NO");
            entity.Property(e => e.CompanyAddr)
                .HasMaxLength(200)
                .IsUnicode(false)

                .HasColumnName("COMPANY_ADDR");
            entity.Property(e => e.CompanyBranch)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("COMPANY_BRANCH");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.CompanyTaxNo)
                .HasMaxLength(13)
                .IsUnicode(false)

                .HasColumnName("COMPANY_TAX_NO");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DeleteBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("DELETE_BY");
            entity.Property(e => e.DeleteDate)

                .HasColumnType("DATE")
                .HasColumnName("DELETE_DATE");
            entity.Property(e => e.DeleteStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .HasDefaultValueSql("'A' ")
                .IsFixedLength()
                .HasColumnName("DELETE_STATUS");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(5)
                .IsUnicode(false)

                .HasColumnName("DOC_STATUS");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.HrEmail)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("HR_EMAIL");
            entity.Property(e => e.HrName)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("HR_NAME");
            entity.Property(e => e.HrPhone)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("HR_PHONE");
            entity.Property(e => e.ReserveEndDate)

                .HasColumnType("DATE")
                .HasColumnName("RESERVE_END_DATE");
            entity.Property(e => e.ReserveStartDate)

                .HasColumnType("DATE")
                .HasColumnName("RESERVE_START_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiHealthSetRefChecklistCfg>(entity =>
        {
            entity.HasKey(e => e.SetRefId).HasName("SYS_C008056");

            entity.ToTable("AAI_HEALTH_SET_REF_CHECKLIST_CFG");

            entity.Property(e => e.SetRefId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("SET_REF_ID");
            entity.Property(e => e.ChecklistDtId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_DT_ID");
            entity.Property(e => e.ChecklistId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DeleteStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("DELETE_STATUS");
            entity.Property(e => e.EndAge)
                .HasPrecision(12)

                .HasColumnName("END_AGE");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.SetRefValue)
                .HasMaxLength(50)
                .IsUnicode(false)

                .HasColumnName("SET_REF_VALUE");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("SEX");
            entity.Property(e => e.StartAge)
                .HasPrecision(12)

                .HasColumnName("START_AGE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.ChecklistDt).WithMany(p => p.AaiHealthSetRefChecklistCfgs)
                .HasForeignKey(d => d.ChecklistDtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_SET_REF_CHECKLIST_CFG_FK1");

            entity.HasOne(d => d.Checklist).WithMany(p => p.AaiHealthSetRefChecklistCfgs)
                .HasForeignKey(d => d.ChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_SET_REF_CHECKLIST_CFG_FK2");
        });

        modelBuilder.Entity<AaiHealthSetRefDoctorM>(entity =>
        {
            entity.HasKey(e => e.SetRefDoctorId).HasName("SYS_C008062");

            entity.ToTable("AAI_HEALTH_SET_REF_DOCTOR_M");

            entity.Property(e => e.SetRefDoctorId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("SET_REF_DOCTOR_ID");
            entity.Property(e => e.ChecklistId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)

                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DeleteStatus)
                .HasMaxLength(1)
                .IsUnicode(false)

                .HasDefaultValueSql("'A' ")
                .IsFixedLength()
                .HasColumnName("DELETE_STATUS");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("DOCTOR_NAME");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)

                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.Checklist).WithMany(p => p.AaiHealthSetRefDoctorMs)
                .HasForeignKey(d => d.ChecklistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEALTH_SET_REF_DOCTOR_M_FK1");
        });

        modelBuilder.Entity<AaiHealthSetRefNicknameCfg>(entity =>
        {
            entity.HasKey(e => e.SetRefNnId).HasName("AAI_HEALTH_SET_REF_NICKNAME_CFG_PK");

            entity.ToTable("AAI_HEALTH_SET_REF_NICKNAME_CFG");

            entity.Property(e => e.SetRefNnId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("SET_REF_NN_ID");
            entity.Property(e => e.ChecklistDId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_D_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)

                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.SetRefName)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("SET_REF_NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)

                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiHealthSetUnderlyingDiseaseM>(entity =>
        {
            entity.HasKey(e => e.UdId).HasName("SYS_C008026");

            entity.ToTable("AAI_HEALTH_SET_UNDERLYING_DISEASE_M");

            entity.Property(e => e.UdId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("UD_ID");
            entity.Property(e => e.CheckupId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsCc)
                .HasPrecision(1)

                .HasColumnName("IS_CC");
            entity.Property(e => e.IsCkd)
                .HasPrecision(1)

                .HasColumnName("IS_CKD");
            entity.Property(e => e.IsCopd)
                .HasPrecision(1)

                .HasColumnName("IS_COPD");
            entity.Property(e => e.IsCvds)
                .HasPrecision(1)

                .HasColumnName("IS_CVDS");
            entity.Property(e => e.IsDb)
                .HasPrecision(1)

                .HasColumnName("IS_DB");
            entity.Property(e => e.IsDps)
                .HasPrecision(1)

                .HasColumnName("IS_DPS");
            entity.Property(e => e.IsEps)
                .HasPrecision(1)

                .HasColumnName("IS_EPS");
            entity.Property(e => e.IsHpldm)
                .HasPrecision(1)

                .HasColumnName("IS_HPLDM");
            entity.Property(e => e.IsHpts)
                .HasPrecision(1)

                .HasColumnName("IS_HPTS");
            entity.Property(e => e.IsObst)
                .HasPrecision(1)

                .HasColumnName("IS_OBST");
            entity.Property(e => e.IsOther)
                .HasPrecision(1)

                .HasColumnName("IS_OTHER");
            entity.Property(e => e.IsTlsm)
                .HasPrecision(1)

                .HasColumnName("IS_TLSM");
            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .IsUnicode(false)

                .HasColumnName("REMARK");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.Checkup).WithMany(p => p.AaiHealthSetUnderlyingDiseaseMs)
                .HasForeignKey(d => d.CheckupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_UNDERLYING_DISEASE_T_FK1");
        });

        modelBuilder.Entity<AaiHealthWithdrawalH>(entity =>
        {
            entity.HasKey(e => e.WithdrawalHId).HasName("AAI_HEALTH_WITHDRAWAL_H_PK");

            entity.ToTable("AAI_HEALTH_WITHDRAWAL_H");

            entity.Property(e => e.WithdrawalHId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("WITHDRAWAL_H_ID");
            entity.Property(e => e.CheckupId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)

                .HasColumnType("DATE")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.JobId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("JOB_ID");
            entity.Property(e => e.PidManual)
                .HasPrecision(1)

                .HasColumnName("PID_MANUAL");
            entity.Property(e => e.Proactive)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("PROACTIVE");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("STATUS");
            entity.Property(e => e.SuggestionDesc)
                .HasMaxLength(250)
                .IsUnicode(false)

                .HasColumnName("SUGGESTION_DESC");
            entity.Property(e => e.SuggestionStatus)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("SUGGESTION_STATUS");
            entity.Property(e => e.TreatmentDate)

                .HasColumnType("DATE")
                .HasColumnName("TREATMENT_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)

                .HasColumnType("DATE")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.WithdrawalDoc)
                .HasMaxLength(255)
                .IsUnicode(false)

                .HasColumnName("WITHDRAWAL_DOC");
            entity.Property(e => e.WithdrawalNo)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("WITHDRAWAL_NO");
        });

        modelBuilder.Entity<AaiHealthWithdrawalT>(entity =>
        {
            entity.HasKey(e => e.WithdrawalId).HasName("SYS_C008069");

            entity.ToTable("AAI_HEALTH_WITHDRAWAL_T");

            entity.Property(e => e.WithdrawalId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(20)")
                .HasColumnName("WITHDRAWAL_ID");
            entity.Property(e => e.CheckupListId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_LIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)

                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)

                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.PayOrderId)

                .HasColumnType("NUMBER(20)")
                .HasColumnName("PAY_ORDER_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)

                .IsFixedLength()
                .HasColumnName("STATUS");
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

            entity.HasOne(d => d.CheckupList).WithMany(p => p.AaiHealthWithdrawalTs)
                .HasForeignKey(d => d.CheckupListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_HEA_WITHDRAWAL_T_FK1");
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

        modelBuilder.Entity<ViewAaiHealthCheckupListT>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_AAI_HEALTH_CHECKUP_LIST_T");

            entity.Property(e => e.BudgetYear)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.ChecklistCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CHECKLIST_CODE");
            entity.Property(e => e.ChecklistId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.ChecklistName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CHECKLIST_NAME");
            entity.Property(e => e.ChecklistPrice)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("CHECKLIST_PRICE");
            entity.Property(e => e.CheckupDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECKUP_DATE");
            entity.Property(e => e.CheckupDateH)
                .HasColumnType("DATE")
                .HasColumnName("CHECKUP_DATE_H");
            entity.Property(e => e.CheckupId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_ID");
            entity.Property(e => e.CheckupListId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_LIST_ID");
            entity.Property(e => e.DeleteStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DELETE_STATUS");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.IsChecked)
                .HasPrecision(1)
                .HasColumnName("IS_CHECKED");
            entity.Property(e => e.IsExport)
                .HasPrecision(1)
                .HasColumnName("IS_EXPORT");
            entity.Property(e => e.IsFromReader)
                .HasPrecision(1)
                .HasColumnName("IS_FROM_READER");
            entity.Property(e => e.MonthBudyear)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MONTH_BUDYEAR");
            entity.Property(e => e.PatientName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PATIENT_NAME");
            entity.Property(e => e.PatientSex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PATIENT_SEX");
            entity.Property(e => e.PatientSurname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PATIENT_SURNAME");
            entity.Property(e => e.PatientTel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PATIENT_TEL");
            entity.Property(e => e.PersonalId)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.ReadDate)
                .HasColumnType("DATE")
                .HasColumnName("READ_DATE");
            entity.Property(e => e.Reason)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("REASON");
            entity.Property(e => e.ReserveId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("RESERVE_ID");
            entity.Property(e => e.ReserveIdH)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("RESERVE_ID_H");
            entity.Property(e => e.UseStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("USE_STATUS");
        });

        modelBuilder.Entity<ViewAaiHealthWithdrawalT>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_AAI_HEALTH_WITHDRAWAL_T");

            entity.Property(e => e.ChecklistCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CHECKLIST_CODE");
            entity.Property(e => e.ChecklistId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKLIST_ID");
            entity.Property(e => e.ChecklistName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CHECKLIST_NAME");
            entity.Property(e => e.ChecklistPrice)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("CHECKLIST_PRICE");
            entity.Property(e => e.CheckupListId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_LIST_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATUS");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.WithdrawalId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("WITHDRAWAL_ID");
            entity.Property(e => e.WithdrawalNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("WITHDRAWAL_NO");
        });

        modelBuilder.Entity<ViewExportListT>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_EXPORT_LIST_T");

            entity.Property(e => e.CheckupId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_ID");
            entity.Property(e => e.CheckupListId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("CHECKUP_LIST_ID");
            entity.Property(e => e.MainHos)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MAIN_HOS");
            entity.Property(e => e.PersonalId)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.PidManual)
                .HasColumnType("NUMBER")
                .HasColumnName("PID_MANUAL");
            entity.Property(e => e.Proactive)
                .HasColumnType("NUMBER")
                .HasColumnName("PROACTIVE");
            entity.Property(e => e.TreatmentDate)
                .HasColumnType("DATE")
                .HasColumnName("TREATMENT_DATE");
        });

        modelBuilder.Entity<ViewMontbudyear>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_MONTBUDYEAR");

            entity.Property(e => e.BudgetYear)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.MonthBudyear)
                .HasPrecision(4)
                .HasColumnName("MONTH_BUDYEAR");
        });
        modelBuilder.HasSequence("ADB_M_DISTRICT_SEQ", "BTDEV2");
        modelBuilder.HasSequence("ADB_M_PROVINCE_SEQ", "BTDEV2");
        modelBuilder.HasSequence("ADB_M_SUBDISTRICT_SEQ", "BTDEV2");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
