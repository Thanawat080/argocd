using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sso.mms.helper.PortalModel;

public partial class PortalDbContext : DbContext
{
    public PortalDbContext()
    {
    }

    public PortalDbContext(DbContextOptions<PortalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnnounceT> AnnounceTs { get; set; }

    public virtual DbSet<BannerT> BannerTs { get; set; }

    public virtual DbSet<CardM> CardMs { get; set; }

    public virtual DbSet<CardT> CardTs { get; set; }

    public virtual DbSet<CertificateT> CertificateTs { get; set; }

    public virtual DbSet<ChatLog> ChatLogs { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<ChatRoomM> ChatRoomMs { get; set; }

    public virtual DbSet<ChatT> ChatTs { get; set; }

    public virtual DbSet<DataUsesHistoryLog> DataUsesHistoryLogs { get; set; }

    public virtual DbSet<DownloadLog> DownloadLogs { get; set; }

    public virtual DbSet<EditDataLog> EditDataLogs { get; set; }

    public virtual DbSet<HospitalM> HospitalMs { get; set; }

    public virtual DbSet<NewsM> NewsMs { get; set; }

    public virtual DbSet<NewsT> NewsTs { get; set; }

    public virtual DbSet<NewsTagList> NewsTagLists { get; set; }

    public virtual DbSet<NotificationLog> NotificationLogs { get; set; }

    public virtual DbSet<NotificationM> NotificationMs { get; set; }

    public virtual DbSet<NotificationT> NotificationTs { get; set; }

    public virtual DbSet<SettingOpendataT> SettingOpendataTs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnnounceT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ANNOUNCE_T_pkey");

            entity.ToTable("ANNOUNCE_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActiveStatus).HasColumnName("ACTIVE_STATUS");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("END_DATE");
            entity.Property(e => e.ImageFile)
                .HasMaxLength(255)
                .HasColumnName("IMAGE_FILE");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<BannerT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BANNER_T_pkey");

            entity.ToTable("BANNER_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BannerName)
                .HasMaxLength(250)
                .HasColumnName("BANNER_NAME");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.StatusAnnounce).HasColumnName("STATUS_ANNOUNCE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UploadFileName)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_FILE_NAME");
            entity.Property(e => e.UploadFilePath)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_FILE_PATH");
            entity.Property(e => e.UploadImageName)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_IMAGE_NAME");
            entity.Property(e => e.UploadImagePath)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_IMAGE_PATH");
            entity.Property(e => e.Url)
                .HasMaxLength(356)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<CardM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CARD_M_pkey");

            entity.ToTable("CARD_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("NAME");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasColumnName("REMARK");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<CardT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CARD_T_pkey");

            entity.ToTable("CARD_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CardMId).HasColumnName("CARD_M_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalId).HasColumnName("HOSPITAL_ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UploadFile).HasColumnName("UPLOAD_FILE");
            entity.Property(e => e.UploadPath).HasColumnName("UPLOAD_PATH");

            entity.HasOne(d => d.CardM).WithMany(p => p.CardTs)
                .HasForeignKey(d => d.CardMId)
                .HasConstraintName("CARD_T_CARD_M_ID_fkey");
        });

        modelBuilder.Entity<CertificateT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Certificate_pkey");

            entity.ToTable("CERTIFICATE_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalMCode)
                .HasMaxLength(9)
                .HasColumnName("HOSPITAL_M_CODE");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UploadImagePath)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_IMAGE_PATH");
        });

        modelBuilder.Entity<ChatLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CHAT_LOG_pkey");

            entity.ToTable("CHAT_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChatTId).HasColumnName("CHAT_T_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserType).HasColumnName("USER_TYPE");

            entity.HasOne(d => d.ChatT).WithMany(p => p.ChatLogs)
                .HasForeignKey(d => d.ChatTId)
                .HasConstraintName("CHAT_T_ID_fk");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CHAT_ROOM_pkey");

            entity.ToTable("CHAT_ROOM");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChatRoomMId).HasColumnName("CHAT_ROOM_M_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HospitalMId).HasColumnName("HOSPITAL_M_ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.ChatRoomM).WithMany(p => p.ChatRooms)
                .HasForeignKey(d => d.ChatRoomMId)
                .HasConstraintName("CHAT_ROOM_M_ID_fk");

            entity.HasOne(d => d.HospitalM).WithMany(p => p.ChatRooms)
                .HasForeignKey(d => d.HospitalMId)
                .HasConstraintName("HOSPITAL_M_ID_fk");
        });

        modelBuilder.Entity<ChatRoomM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CHAT_ROOM_M_pkey");

            entity.ToTable("CHAT_ROOM_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CodeHos)
                .HasMaxLength(10)
                .HasColumnName("CODE_HOS");
            entity.Property(e => e.CodeSso)
                .HasMaxLength(10)
                .HasColumnName("CODE_SSO");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<ChatT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CHAT_T_pkey");

            entity.ToTable("CHAT_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChatRoomId).HasColumnName("CHAT_ROOM_ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsRead).HasColumnName("IS_READ");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.RefChatId).HasColumnName("REF_CHAT_ID");
            entity.Property(e => e.SenderName)
                .HasMaxLength(250)
                .HasColumnName("SENDER_NAME");
            entity.Property(e => e.TextMessage)
                .HasMaxLength(250)
                .HasColumnName("TEXT_MESSAGE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UploadFileName).HasColumnName("UPLOAD_FILE_NAME");
            entity.Property(e => e.UploadFilePath).HasColumnName("UPLOAD_FILE_PATH");
            entity.Property(e => e.UploadImageName).HasColumnName("UPLOAD_IMAGE_NAME");
            entity.Property(e => e.UploadImagePath).HasColumnName("UPLOAD_IMAGE_PATH");
            entity.Property(e => e.UserType).HasColumnName("USER_TYPE");

            entity.HasOne(d => d.ChatRoom).WithMany(p => p.ChatTs)
                .HasForeignKey(d => d.ChatRoomId)
                .HasConstraintName("CHAT_ROOM_ID_fk");
        });

        modelBuilder.Entity<DataUsesHistoryLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DATA_USES_HISTORY_LOG_pkey");

            entity.ToTable("DATA_USES_HISTORY_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DocName)
                .HasMaxLength(255)
                .HasColumnName("DOC_NAME");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .HasColumnName("DOC_TYPE");
            entity.Property(e => e.EditDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("EDIT_DATE");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .HasColumnName("USER_NAME");
        });

        modelBuilder.Entity<DownloadLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DOWNLOAD_LOG_pkey");

            entity.ToTable("DOWNLOAD_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DocName)
                .HasMaxLength(255)
                .HasColumnName("DOC_NAME");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .HasColumnName("DOC_TYPE");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .HasColumnName("USER_NAME");
        });

        modelBuilder.Entity<EditDataLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EDIT_DATA_LOG_pkey");

            entity.ToTable("EDIT_DATA_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.DocName)
                .HasMaxLength(255)
                .HasColumnName("DOC_NAME");
            entity.Property(e => e.DocType)
                .HasMaxLength(50)
                .HasColumnName("DOC_TYPE");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.PrefixMCode)
                .HasMaxLength(10)
                .HasColumnName("PREFIX_M_CODE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<HospitalM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HOSPITAL_M_pkey");

            entity.ToTable("HOSPITAL_M");

            entity.HasIndex(e => e.Code, "index_hospital_m_code");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(25)
                .HasColumnName("CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.HosType)
                .HasMaxLength(10)
                .HasColumnName("HOS_TYPE");
            entity.Property(e => e.ImageName)
                .HasColumnType("character varying")
                .HasColumnName("IMAGE_NAME");
            entity.Property(e => e.ImagePath)
                .HasColumnType("character varying")
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<NewsM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NEWS_M_pkey");

            entity.ToTable("NEWS_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.ImageFileM).HasColumnName("IMAGE_FILE_M");
            entity.Property(e => e.ImagePathM).HasColumnName("IMAGE_PATH_M");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("NAME");
            entity.Property(e => e.NewsType).HasColumnName("NEWS_TYPE");
            entity.Property(e => e.Remark).HasColumnName("REMARK");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<NewsT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NEWS_T_pkey");

            entity.ToTable("NEWS_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasColumnName("CONTENT");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("END_DATE");
            entity.Property(e => e.ImageFileM).HasColumnName("IMAGE_FILE_M");
            entity.Property(e => e.ImagePathM).HasColumnName("IMAGE_PATH_M");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.NewsMId).HasColumnName("NEWS_M_ID");
            entity.Property(e => e.PinStatus).HasColumnName("PIN_STATUS");
            entity.Property(e => e.PrivilegePrivate).HasColumnName("PRIVILEGE_PRIVATE");
            entity.Property(e => e.PrivilegePublic).HasColumnName("PRIVILEGE_PUBLIC");
            entity.Property(e => e.PrivilegeSso).HasColumnName("PRIVILEGE_SSO");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UploadFile).HasColumnName("UPLOAD_FILE");
            entity.Property(e => e.UploadPath).HasColumnName("UPLOAD_PATH");

            entity.HasOne(d => d.NewsM).WithMany(p => p.NewsTs)
                .HasForeignKey(d => d.NewsMId)
                .HasConstraintName("NEWS_M_ID_fk");
        });

        modelBuilder.Entity<NewsTagList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NEWS_TAG_LIST_pkey");

            entity.ToTable("NEWS_TAG_LIST");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasColumnType("character varying")
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.NewsTId).HasColumnName("NEWS_T_ID");
            entity.Property(e => e.TagName)
                .HasMaxLength(500)
                .HasColumnName("TAG_NAME");
            entity.Property(e => e.UpdateBy)
                .HasColumnType("character varying")
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");

            entity.HasOne(d => d.NewsT).WithMany(p => p.NewsTagLists)
                .HasForeignKey(d => d.NewsTId)
                .HasConstraintName("NEWS_T_ID_fk");
        });

        modelBuilder.Entity<NotificationLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NOTIFICATION_LOG_pkey");

            entity.ToTable("NOTIFICATION_LOG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.NotiTId).HasColumnName("NOTI_T_ID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UserType).HasColumnName("USER_TYPE");

            entity.HasOne(d => d.NotiT).WithMany(p => p.NotificationLogs)
                .HasForeignKey(d => d.NotiTId)
                .HasConstraintName("NOTIFICATION_LOG_NOTI_T_ID_fkey");
        });

        modelBuilder.Entity<NotificationM>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NOTIFICATION_M_pkey");

            entity.ToTable("NOTIFICATION_M");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppCode)
                .HasMaxLength(30)
                .HasColumnName("APP_CODE");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("NAME");
            entity.Property(e => e.NotiCode)
                .HasMaxLength(4)
                .HasColumnName("NOTI_CODE");
            entity.Property(e => e.Remark)
                .HasMaxLength(250)
                .HasColumnName("REMARK");
            entity.Property(e => e.Sequence).HasColumnName("SEQUENCE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
        });

        modelBuilder.Entity<NotificationT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NOTIFICATION_T_pkey");

            entity.ToTable("NOTIFICATION_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppCode)
                .HasMaxLength(30)
                .HasColumnName("APP_CODE");
            entity.Property(e => e.Content).HasColumnName("CONTENT");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.IdRef)
                .HasMaxLength(10)
                .HasColumnName("ID_REF");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.NewTId).HasColumnName("NEW_T_ID");
            entity.Property(e => e.NotiMId).HasColumnName("NOTI_M_ID");
            entity.Property(e => e.NotifyOption)
                .HasMaxLength(5)
                .HasColumnName("NOTIFY_OPTION");
            entity.Property(e => e.OrgCode)
                .HasMaxLength(20)
                .HasColumnName("ORG_CODE");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(20)
                .HasColumnName("ROLE_CODE");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Url)
                .HasMaxLength(225)
                .HasColumnName("URL");
            entity.Property(e => e.UrlText)
                .HasMaxLength(225)
                .HasColumnName("URL_TEXT");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserType)
                .HasMaxLength(5)
                .HasColumnName("USER_TYPE");

            entity.HasOne(d => d.NewT).WithMany(p => p.NotificationTs)
                .HasForeignKey(d => d.NewTId)
                .HasConstraintName("NEW_T_ID_fk");

            entity.HasOne(d => d.NotiM).WithMany(p => p.NotificationTs)
                .HasForeignKey(d => d.NotiMId)
                .HasConstraintName("NOTIFICATION_T_NOTI_M_ID_fkey");
        });

        modelBuilder.Entity<SettingOpendataT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SETTING_OPENDATA_T_pkey");

            entity.ToTable("SETTING_OPENDATA_T");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("CREATE_BY");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Detail)
                .HasMaxLength(250)
                .HasColumnName("DETAIL");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsStatus).HasColumnName("IS_STATUS");
            entity.Property(e => e.ShowStatus).HasColumnName("SHOW_STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATE_BY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UploadFileName)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_FILE_NAME");
            entity.Property(e => e.UploadFilePath)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_FILE_PATH");
            entity.Property(e => e.UploadImageName)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_IMAGE_NAME");
            entity.Property(e => e.UploadImagePath)
                .HasMaxLength(255)
                .HasColumnName("UPLOAD_IMAGE_PATH");
            entity.Property(e => e.Url)
                .HasMaxLength(356)
                .HasColumnName("URL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
