using AntDesign;
using Microsoft.AspNetCore.Mvc;
using sso.mms.fees.ext.ViewModels.SlideBarLayout;

namespace sso.mms.fees.ext.Data
{
    public class MenuListService
    {

        public MenuListService()
        {

        }

        public List<MenuList> MenuList = new List<MenuList>
          {
            new MenuList {
                Pages = "example",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "/riskExposure/index",Icon="fa-regular fa-paste" },
                    new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "/riskExposure/formula",Icon="fa-solid fa-square-root-variable" },
                    new MenuItems { Title = "รายการขอเบิก" , Link = "/riskExposure/withDrawRequestList",Icon="fa-solid fa-list" },
                    new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/riskExposure/paymentOrdersList",Icon="fa-regular fa-square-check" },
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/riskExposure/disburseMentList",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/riskExposure/reports",Icon="fa-regular fa-folder-closed" }
                }
            },
            new MenuList {
                Pages = "adjRW",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "จัดการวงเงินงบปรมาณ" , Link = "/adjRW/index",Icon="fa-brands fa-bitcoin" },
                    new MenuItems { Title = "กำหนดอัตราค่าบริการ" , Link = "/adjRW/SetServiceRate",Icon="fa-regular fa-square-plus" },
                    new MenuItems { Title = "กำหนดประเภทสถานพยาบาล" , Link = "/adjRW/DetermineTypeHospital" ,Icon="fa-solid fa-hospital"},
                    new MenuItems { Title = "รายการขอเบิก" , Link = "/adjRW/WithdrawalRequestList",Icon="fa-solid fa-list-check"},
                    new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/adjRW/PaymentOrderList" ,Icon="fa-solid fa-square-check"},
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/adjRW/DisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/adjRW/Report", Icon="fa-solid fa-folder-open" }
                }
            },
           new MenuList {
               Pages = "organTransplantation",
               MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "ตรวจสอบสิทธิ์" , Link = "/organTransplantation/index",Icon="fa-brands fa-circle" },
                    new MenuItems { Title = "บันทึกรายการเบิก" , Link = "/organTransplantation/disbursementRecord",Icon="fa-regular fa-user-plus"  },
                    new MenuItems { Title = "รายการที่ส่งเบิก" , Link = "/organTransplantation/disbursementLists",Icon="fa-solid fa-paper-plane" },
                    new MenuItems { Title = "ประวัติรายการเบิกจ่าย" , Link = "/organTransplantation/disbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/organTransplantation/report", Icon="fa-solid fa-folder-open" },
               }
           },
            new MenuList {
                Pages = "disability",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/disability/index",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/disability/report" ,Icon="fa-solid fa-square-check"},
                }
            },
            new MenuList {
                Pages = "promoteHealthExt",
               MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "จองสิทธิ์การตรวจสุขภาพ" , Link = "/promoteHealthExt/index", Icon="fa-regular fa-square-plus" },
                    new MenuItems { Title = "บันทึกรายการตรวจ", Icon ="fa-solid fa-list-check", SubMenus = new List<MenuItems> { new MenuItems { Title = "รายสถานประกอบการ", Link = "/promoteHealthExt/recordChecklistEstablishment" }, new MenuItems { Title = "รายบุคคล", Link = "/promoteHealthExt/recordChecklistIndividual" } } },
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/promoteHealthExt/disbursementHistory", Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "ประวัติใบคำสั่งจ่าย" , Link = "/promoteHealthExt/payOrderHistory", Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/promoteHealthExt/reports",  Icon="fa-regular fa-folder-open"},
                    new MenuItems { Title = "กำหนดค่าอ้างอิงผลการตรวจ" , Link = "/promoteHealthExt/determineReferenceValue", Icon="fa-regular fa-circle-check" },
                    new MenuItems { Title = "กำหนดรายชื่อผู้วินิจฉัย" , Link = "/promoteHealthExt/determinedoctor", Icon="fa-solid fa-user-doctor" }
                }
            },
            new MenuList {
                Pages = "HealthPromoVaccinationExt",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "บันทึกรายการตรวจ",Icon="fa-solid fa-user-plus",
                        SubMenus = new List<MenuItems> {
                            new MenuItems { Title = "ตรวจสอบสิทธิ์", Link = "/HealthPromoVaccinationExt/index", Icon= "fa-solid fa-circle" },
                            new MenuItems { Title = "รายการที่บันทึก", Link = "/HealthPromoVaccinationExt/RecordChecklistSubInfo", Icon = "fa-solid fa-circle" }
                        }
                    },
                    new MenuItems { Title = "รายการฉีดวัคซีนเชิงรุก" , Link = "/HealthPromoVaccinationExt/ProactiveVaccinationList" ,Icon="fa-solid fa-syringe"},
                    new MenuItems { Title = "รายการที่ส่งเบิก" , Link = "/HealthPromoVaccinationExt/WithdrawalsList" ,Icon="fa-solid fa-paper-plane"},
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/HealthPromoVaccinationExt/WithdrawalsListHistory" ,Icon="fa-solid fa-clock-rotate-left"},
                    new MenuItems { Title = "รายงาน" , Link = "/HealthPromoVaccinationExt/HealthPromoReport" ,Icon="fa-regular fa-folder-open"},
                    new MenuItems { Title = "ตั้งค่าข้อมูลวัคซีน" , Link = "/HealthPromoVaccinationExt/HealthPromoSetting" ,Icon="fa-solid fa-gears"}
                }
            },
            new MenuList {
                Pages = "covid19",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/covid19/disbursementHistory", Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/covid19/report", Icon="fa-regular fa-folder-open"},
                }
            },
            new MenuList {
                        Pages = "72Hours",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "บันทึกรายการเบิก" , Link = "/72Hours/Index",Icon="fa-solid fa-book" },
                            new MenuItems { Title = "รายการที่ส่งเบิก" , Link = "/72Hours/SentWithdrawal",Icon="fa-solid fa-paper-plane" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/72Hours/DisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายงาน" , Link = "/72Hours/Report",Icon="fa-regular fa-folder-open" },
                        }
                    },
             new MenuList {
                Pages = "ucep",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/ucep/disbursementHistory", Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/ucep/report", Icon="fa-regular fa-folder-open"},
                }
            },
            new MenuList {
                            Pages = "Methadone",
                            MenuItems = new List<MenuItems> {
                                new MenuItems { Title = "บันทึกรายการเบิก" , Link = "/Methadone/Index", Icon="fa-solid fa-list-check" },
                                new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/Methadone/DisbursementHistory", Icon="fa-solid fa-clock-rotate-left" },
                                new MenuItems { Title = "รายงาน" , Link = "/Methadone/Report", Icon="fa-regular fa-folder-open" },
                            }
                    },
            new MenuList {
                Pages = "medicalSpecialist",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/medicalSpecialist/disbursementHistory", Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/medicalSpecialist/report", Icon="fa-regular fa-folder-open"},
                }
            },
            new MenuList {
                        Pages = "hermodialysis",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/hermodialysis/disbursementHistory" ,Icon="fa-solid fa-clock-rotate-left"},
                            new MenuItems { Title = "รายงาน" , Link = "/hermodialysis/report" , Icon="fa-regular fa-folder-open"},
                           
                        }
                    },
            new MenuList {
                        Pages = "test1",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "" },
                            new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" }
                        }
                    },
            new MenuList {
                        Pages = "test1",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "" },
                            new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" }
                        }
                    },
            new MenuList {
                        Pages = "test1",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "" },
                            new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" }
                        }
                    },
            new MenuList {
                        Pages = "test1",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "" },
                            new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" }
                        }
                    },
            new MenuList {
                        Pages = "test1",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "" },
                            new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" }
                        }
                    },
            new MenuList {
                        Pages = "test1",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "" },
                            new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" }
                        }
                    },
            new MenuList {
                        Pages = "Dental",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "อนุญาตการใช้รถทันตกรรม" , Link = "/Dental/index" },
                            new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/Dental/NotificationList" },
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/Dental/PaymentOrder" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/Dental/DisBurseMentList" },
                            new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/Dental/DisapprovedTranHistory" },
                            new MenuItems { Title = "รายงาน" , Link = "/Dental/Report" }
                        }
                    },
            new MenuList {
                        Pages = "DentalHop",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "บันทึกรถทัตนกรรม" , Link = "/Dental/index" ,Icon="fa-solid fa-truck-fast"},
                            new MenuItems { Title = "บันทึกรายการรักษา" , Link = "/Dental/TreatmentRecord" ,Icon="fa-solid fa-user-plus"},
                            new MenuItems { Title = "รายการส่งเบิก" , Link = "/Dental/remittance" ,Icon="fa-regular fa-paper-plane"},
                            new MenuItems { Title = "ประวัติรายการเบิก" , Link = "/Dental/withdrawalsHistory"  ,Icon="fa-solid fa-clock-rotate-left"},
                            new MenuItems { Title = "รายงาน" , Link = "/Dental/Report" ,Icon="fa-solid fa-folder-open" }
                        }
                    }
        };

        public MenuList GetMenuLists(string pageName)
        {
            var findMenuList = MenuList.FirstOrDefault(x => x.Pages == pageName);
            if (findMenuList != null)
            {
                return findMenuList;
            }
            else
            {
                return new MenuList();
            }
        }
    }
}
