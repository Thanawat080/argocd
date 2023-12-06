﻿using Microsoft.AspNetCore.Components.Forms;

namespace sso.mms.portal.admin.ViewModels
{
    public class RequestBannerT
    {
        public int Id { get; set; }

        public string? BannerName { get; set; }

        public string? Url { get; set; }

        public int? StatusAnnounce { get; set; }

        public string? UploadImagePath { get; set; }

        public bool? IsActive { get; set; }

        public int? IsStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? UpdateBy { get; set; }

        public string? UploadImageName { get; set; }

        public string? UploadFileName { get; set; }

        public string? UploadFilePath { get; set; }

        public IBrowserFile File { get; set; }
    }
}
