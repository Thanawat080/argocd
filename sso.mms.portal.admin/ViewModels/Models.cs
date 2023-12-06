namespace sso.mms.portal.admin.ViewModels
{
    public class Models
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string accordionID { get; set; }
        public string collapseID { get; set; }
    }
    public class SubMenuModel
    {
        public int Id { get; set; }
        public int MenulistId { get; set; }
        public string Namedetail { get; set; }
        public string ImagePath { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }

        public string MenuCode { get; set; }
        public string Path { get; set; } = "/mainmenu";
        

    }
}
