namespace sso.mms.login.ViewModels.Master
{
    public class Province
    {
        public int id { get; set; }
        public string? code { get; set; }    
        public string? name_th{ get; set; }
        public string? name_en{ get; set; }
        public DateTime? createDate{ get; set; }
        public string createBy { get; set; }
        public DateTime? updateDate { get; set; }
        public string? updateBy { get; set; }
    }

    public class District
    {
        public int id { get; set; }
        public string? code { get; set; }
        public string? name_th { get; set; }
        public string? name_en { get; set; }
        public string? province_code { get; set; }
        public DateTime? createDate { get; set; }
        public string? createBy { get; set; }
        public DateTime? updateDate { get; set; }
        public string? updateBy { get; set; }
    }

    public class SubDistrict
    {
        public int id { get; set; }
        public string? code { get; set; }
        public string? name_th { get; set; }
        public string? name_en { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set;} 
        public DateTime? createDate { get; set; }
        public string? createBy { get; set; }
        public DateTime? updateDate { get; set; }
        public string? updateBy { get; set; }
    }
}
