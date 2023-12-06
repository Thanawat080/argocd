using sso.mms.fees.api.Interface.PromoteHealth.Base;
using sso.mms.fees.api.Entities.PromoteHealth;
using sso.mms.fees.api.ViewModels.PromoteHealth;
using System.Collections.Generic;

namespace sso.mms.fees.api.Services.PromoteHealth.Base
{
    public class AlreadyCheckListServices : IAlreadyCheckListBaseServices
    {
        private readonly PromoteHealthContext db;

        public AlreadyCheckListServices(PromoteHealthContext DbContext)
        {
            db = DbContext;
        }
        public async Task<apiview850> api850(string? personid)
        {
            try
            {
                // Initialize our list
                List<bool> mylist = new List<bool>(new bool[] { true, false });

                List<string> mylistName = new List<string>(new string[] { "กชกร", "กชอินทร์", "ชนนน", "ฉันทิสา", "ชญาภา", "จิระเดช", "จิรฐา", "จักรดุลย์", "คมกริช", "คฑาวุธ", "ขจรเกียรติ" });
                List<string> mylistSName = new List<string>(new string[] { "ศิลปการ", "วรโชติ", "นฤวัตปกรณ์", " ปิติโชค", "ธนากุล", "อมรสิริ", "ธนานนท์", "กุลโชติ", "คุณเจริญ", "อัครวิทย", "เดชากุล" });
                List<Int32> mylistAge = new List<Int32>(new Int32[] { 16, 22, 36, 48, 56, 72 });
                List<string> mylistGender = new List<string>(new string[] { "F", "M" });
                Random R = new Random();

                // get random number from 0 to 2. 
                int someRandomNumber = R.Next(0, mylist.Count());

                int someSName = R.Next(0, mylistSName.Count());
                int someName = R.Next(0, mylistName.Count());
                int someAge = R.Next(0, mylistAge.Count());
                int someGender = R.Next(0, mylistGender.Count());
                apiview850 res = new apiview850();
                res.age = mylistAge.ElementAt(someAge);
                res.Sex = mylistGender.ElementAt(someGender);
                res.Name = mylistName.ElementAt(someName);
                res.Sname = mylistSName.ElementAt(someSName);
                res.status = mylist.ElementAt(someRandomNumber);
                res.personalId = personid;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Already_CheckList(string PERSONAL_ID, string? YearNow, string ChecklistCode)
        {
            try
            {
                var res = 0;
                if (YearNow != null)
                {
                    res = db.ViewAaiHealthCheckupListTs.Where(x => x.ChecklistCode == ChecklistCode && x.BudgetYear == YearNow && x.PersonalId == PERSONAL_ID && x.UseStatus == "R" && x.IsChecked == true).Count();
                }
                else
                {
                    res = db.ViewAaiHealthCheckupListTs.Where(x => x.ChecklistCode == ChecklistCode && x.PersonalId == PERSONAL_ID && x.UseStatus == "R" && x.IsChecked == true).ToList().Count();
                }
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> Already_CheckList2(string PERSONAL_ID, string? YearNow, string ChecklistCode, int Y)
        {
            try
            {
                DateTime DateBegin = new DateTime(Convert.ToInt32(YearNow) - Y, 1, 1);
                DateTime DateEnd = new DateTime();
                DateEnd = DateTime.Now;
                var res = 0;
                res = db.ViewAaiHealthCheckupListTs.Where(x => x.ChecklistCode == ChecklistCode && x.PersonalId == PERSONAL_ID && (x.CheckupDate >= DateBegin && x.CheckupDate <= DateEnd) && x.UseStatus == "R" && x.IsChecked == true).Count();
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
