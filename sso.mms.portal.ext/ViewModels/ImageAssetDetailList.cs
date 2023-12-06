namespace sso.mms.portal.ext.ViewModels
{
    public class ImageAssetDetailList
    {
        public string? ImagePath { get; set; }
        public string? DetailImagePath { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? Path { get; set; } = "";
        public string? MenuCode { get; set; }


    }

    public class ImageAssetMainDetailList
    {
        public string? ImagePath { get; set; }
        public string? DetailImagePath { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? Path { get; set; } 
        public string? MenuCode { get; set;}
    }
    public class NewsDetailList
    {
        public string? Title { get; set; }
        public int? Id { get; set; }

    }
}
