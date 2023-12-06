using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.fees.api.Entities.Ods;

public partial class OdsContext : DbContext
{
    public OdsContext()
    {
    }

    public OdsContext(DbContextOptions<OdsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AaiOdsBudgetYearM> AaiOdsBudgetYearMs { get; set; }

    public virtual DbSet<AaiOdsPayOrderT> AaiOdsPayOrderTs { get; set; }

    public virtual DbSet<AaiOdsRateM> AaiOdsRateMs { get; set; }

    public virtual DbSet<AaiOdsWithdrawalT> AaiOdsWithdrawalTs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=aai_ods;Password=aai_ods;Data Source=192.168.10.69:1521/orcl;persist security info=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("AAI_ODS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AaiOdsBudgetYearM>(entity =>
        {
            entity.HasKey(e => e.BudgetYear).HasName("AAI_ODS_BUDGET_YEAR_M_PK");

            entity.ToTable("AAI_ODS_BUDGET_YEAR_M");

            entity.Property(e => e.BudgetYear)
                .HasPrecision(4)
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.BudgetYearStatus)
                .HasPrecision(1)
                .HasColumnName("BUDGET_YEAR_STATUS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<AaiOdsPayOrderT>(entity =>
        {
            entity.HasKey(e => e.PayOrderId).HasName("AAI_ODS_PAY_ORDER_T_PK");

            entity.ToTable("AAI_ODS_PAY_ORDER_T");

            entity.Property(e => e.PayOrderId)
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
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.PayOrderAmount)
                .HasColumnType("NUMBER(20,2)")
                .HasColumnName("PAY_ORDER_AMOUNT");
            entity.Property(e => e.PayOrderNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PAY_ORDER_NO");
            entity.Property(e => e.PayOrderSetNo)
                .HasMaxLength(255)
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
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("NULL ")
                .HasColumnName("WITHDRAWAL_NO");

            entity.HasOne(d => d.WithdrawalNoNavigation).WithMany(p => p.AaiOdsPayOrderTs)
                .HasPrincipalKey(p => p.WithdrawalNo)
                .HasForeignKey(d => d.WithdrawalNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_ODS_PAY_ORDER_T_FK1");
        });

        modelBuilder.Entity<AaiOdsRateM>(entity =>
        {
            entity.HasKey(e => e.RateodsId).HasName("AAI_ODS_RATE_M_PK");

            entity.ToTable("AAI_ODS_RATE_M");

            entity.Property(e => e.RateodsId)
                .HasPrecision(4)
                .HasColumnName("RATEODS_ID");
            entity.Property(e => e.BudgetYear)
                .HasPrecision(4)
                .HasColumnName("BUDGET_YEAR");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Rateods)
                .HasPrecision(4)
                .HasColumnName("RATEODS");
            entity.Property(e => e.RateodsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("RATEODS_ACTIVE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.BudgetYearNavigation).WithMany(p => p.AaiOdsRateMs)
                .HasForeignKey(d => d.BudgetYear)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AAI_ODS_RATE_M_FK1");
        });

        modelBuilder.Entity<AaiOdsWithdrawalT>(entity =>
        {
            entity.HasKey(e => e.WithdrawalId).HasName("AAI_ODS_WITHDRAWAL_T_PK");

            entity.ToTable("AAI_ODS_WITHDRAWAL_T");

            entity.HasIndex(e => e.WithdrawalNo, "AAI_ODS_WITHDRAWAL_T_UK1").IsUnique();

            entity.Property(e => e.WithdrawalId)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("WITHDRAWAL_ID");
            entity.Property(e => e.AdbId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ADB_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("HOSPITAL_CODE");
            entity.Property(e => e.IsPreaudit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("IS_PREAUDIT");
            entity.Property(e => e.PeriodDate)
                .HasPrecision(6)
                .HasColumnName("PERIOD_DATE");
            entity.Property(e => e.RateodsNow)
                .HasPrecision(4)
                .HasColumnName("RATEODS_NOW");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATUS");
            entity.Property(e => e.TranId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TRAN_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasPrecision(6)
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.WithdrawalDoc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("WITHDRAWAL_DOC");
            entity.Property(e => e.WithdrawalNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("WITHDRAWAL_NO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
