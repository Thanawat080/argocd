namespace sso.mms.fees.ext.ViewModels.SlideBarLayout
{

    public class MenuList
    {
        public string? Pages { get; set; }
        public List<MenuItems>? MenuItems { get; set; }
        public List<MenuItems>? SubMenus { get; set; }

    }

    public class MenuItems
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Icon { get; set; }
        public List<MenuItems>? SubMenus { get; set; }
        public List<MenuItems>? listmenu { get; set; }
    }
}
