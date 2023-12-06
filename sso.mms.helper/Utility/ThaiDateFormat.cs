using System.Globalization;

namespace sso.mms.helper.Utility
{
    public static class ThaiDateFormat
    {
        public static string formattedDate;

        public static string FormattedDate(DateTime? getDate)
        {
            DateTime? date = getDate;
            CultureInfo? thaiCulture = new CultureInfo("th-TH");
            formattedDate = date?.ToString("d MMMM yyyy", thaiCulture);
            return formattedDate;
        }
        public static string FormatShotMonth(DateTime? getDate)
        {
            DateTime? date = getDate;
            CultureInfo? thaiCulture = new CultureInfo("th-TH");
            formattedDate = date?.ToString("d MMM yyyy", thaiCulture);
            return formattedDate; 
        }
    }
}
