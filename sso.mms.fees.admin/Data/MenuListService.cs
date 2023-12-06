using AntDesign;
using Blazorise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sso.mms.fees.admin.ViewModels.SlideBarLayout;
using sso.mms.helper.Configs;
using System.Collections.Generic;

namespace sso.mms.fees.admin.Data
{
    public class MenuListService
    {

        public MenuListService()
        {

        }

        public List<MenuList> MenuList = new List<MenuList>
          {
            new MenuList {
                Pages = "riskExposure",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "กำหนดคะแนนโรคเรื้อรัง" , Link = "/riskExposure/index",Icon="fa-regular fa-paste" },
                    new MenuItems { Title = "กำหนดสูตรการคำนวณ" , Link = "/riskExposure/formula",Icon="fa-solid fa-square-root-variable" },
                    new MenuItems { Title = "รายการขอเบิก" , Link = "/riskExposure/withDrawRequestList",Icon="fa-solid fa-list" },
                    new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/riskExposure/paymentOrdersList",Icon="fa-regular fa-square-check" },
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/riskExposure/disburseMentList",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/riskExposure/reports",Icon="fa-regular fa-folder-closed" },
                    new MenuItems { Title = "ผลการประมวลด้วย AI" , Link = "/riskExposure/progressAI",Icon="fa-regular fa-tasks" }
                }
            },
            new MenuList {
                Pages = "adjRW",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "จัดการวงเงินงบปรมาณ" , Link = "/adjRW/index",Icon="fa-solid fa-dollar-sign" },
                    new MenuItems { Title = "กำหนดอัตราค่าบริการ" , Link = "/adjRW/SetServiceRate",Icon="fa-regular fa-square-plus" },
                    new MenuItems { Title = "กำหนดประเภทสถานพยาบาล" , Link = "/adjRW/DetermineTypeHospital" ,Icon="fa-regular fa-hospital"},
                    new MenuItems { Title = "รายการขอเบิก" , Link = "/adjRW/WithdrawalRequestList",Icon="fa-solid fa-list-check"},
                    new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/adjRW/PaymentOrders" ,Icon="fa-regular fa-square-check"},
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/adjRW/DisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/adjRW/Report", Icon="fa-regular fa-folder-open" },
                    new MenuItems { Title = "ผลการประมวลด้วย AI" , Link = "/adjRW/progressAI",Icon="fa-regular fa-tasks" }

                }
            },
           new MenuList {
               Pages = "organTransplantation",
               MenuItems = new List<MenuItems> {
               new MenuItems { Title = "รายการรับแจ้ง" , Link = "/organTransplantation/index",Icon="fa-brands fa-bitcoin" },
               new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/organTransplantation/createPaymentOrder",Icon="fa-regular fa-square-plus"  },
               new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/organTransplantation/disbursementHistory",Icon="fa-solid fa-hospital" },
               new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/organTransplantation/unapprovalLists",Icon="fa-solid fa-square-check" },
               new MenuItems { Title = "รายงาน" , Link = "/organTransplantation/report", Icon="fa-solid fa-folder-open" },
               new MenuItems { Title = "กำหนดอัตราจ่าย" , Link = "/organTransplantation/payrate", Icon="fa-solid fa-clock-rotate-left" },
               }
           },
            new MenuList {
                Pages = "disability",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "นำเข้าข้อมูล" , Link = "/disability/index",Icon="fa-brands fa-bitcoin" },
                    new MenuItems { Title = "งานวินิจฉัย" , Link = "/disability/diagnosis",Icon="fa-regular fa-square-plus" },
                    new MenuItems { Title = "ขอเอกสารเพิ่มเติม" , Link = "/disability/requestDocuments" ,Icon="fa-solid fa-hospital"},
                    new MenuItems { Title = "งานสั่งจ่าย" , Link = "/disability/workOrders",Icon="fa-solid fa-list-check"},
                    new MenuItems { Title = "ประวัติการเบิก" , Link = "/disability/withdrawalHistory" ,Icon="fa-solid fa-square-check"},
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/disability/reportPay",Icon="fa-solid fa-clock-rotate-left" },
                }
            },
            new MenuList {
                Pages = "promoteHealth",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "กำหนดรายการตรวจสุขภาพ" , Link = "/promoteHealth/index",Icon="fa-solid fa-dollar-sign" },
                    new MenuItems { Title = "รายการขอเบิก" , Link = "/promoteHealth/withdrawalRequestList",Icon="fa-solid fa-list-check" },
                    new MenuItems { Title = "รายการใบขอเบิก" , Link = "/promoteHealth/paymentOrdersList",Icon="fa-regular fa-square-check"},
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/promoteHealth/disbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/promoteHealth/reports", Icon="fa-regular fa-folder-open" },
                    new MenuItems { Title = "ผลการประมวลด้วย AI" , Link = "/promoteHealth/reportsAI", Icon="fa-regular fa-folder-open" }
                }
            },
           new MenuList {
                Pages = "HealthPromoVaccination",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/HealthPromoVaccination/index",Icon="fa-solid fa-dollar-sign" },
                    new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/HealthPromoVaccination/CreatePaymentOrderlist",Icon="fa-solid fa-list-check" },
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/HealthPromoVaccination/DisbursementHistory",Icon="fa-regular fa-square-check"},
                    new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/HealthPromoVaccination/DisapprovedTransactionHistory",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "รายงาน" , Link = "/HealthPromoVaccination/Report", Icon="fa-regular fa-folder-open" }
                }
            },
            new MenuList {
                Pages = "organMaintenance",
                MenuItems = new List<MenuItems> {
                    new MenuItems { Title = "บันทึกค่าบำรุงรักษาอวัยวะ" , Link = "/organMaintenance/index",Icon="fa-solid fa-lungs" },
                    new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/organMaintenance/Notificationlist",Icon="fa-solid fa-list-check" },
                    new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/organMaintenance/createPaymentOrder",Icon="fa-solid fa-file-circle-plus" },
                    new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/organMaintenance/DisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                    new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/organMaintenance/DisapprovedDisbursements" ,Icon="fa-solid fa-file-circle-exclamation"},
                    new MenuItems { Title = "รายงาน" , Link = "/organMaintenance/Report" ,Icon="fa-regular fa-folder-open"},
                    new MenuItems { Title = "กำหนดอัตราจ่าย" , Link = "/organMaintenance/SetRate" ,Icon="fa-solid fa-gears"}
                }
            },
            new MenuList {
                        Pages = "UCEP",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/UCEP/index",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/UCEP/createpaymentorder",Icon="fa-solid fa-file-circle-plus" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/UCEP/disbursementhistory",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/UCEP/disapprovedtranhistory",Icon="fa-solid fa-file-circle-exclamation" },
                            new MenuItems { Title = "รายงาน" , Link = "",Icon="fa-regular fa-folder-open" },
                            //new MenuItems { Title = "กำหนดอัตราจ่าย" , Link = "" ,Icon="fa-solid fa-gears"}
                        }
                    },
            new MenuList {
                        Pages = "72Hours",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "สืบค้นข้อมูลผู้ประกันตน" , Link = "/72Hours/index" ,Icon="fa-solid fa-magnifying-glass" },
                            new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/72Hours/NotificationList",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/72Hours/CreatePaymentOrder",Icon="fa-regular fa-square-check" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/72Hours/DisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายการไม่อนุมัติ/รอผลอุทธรณ์" , Link = "/72Hours/DisapprovedDisbursements" ,Icon="fa-regular fa-rectangle-list"},
                            new MenuItems { Title = "รายงาน" , Link = "/72Hours/Report",Icon="fa-regular fa-folder-open" },
                            new MenuItems { Title = "กำหนดอัตราจ่าย" , Link = "/72Hours/SetTheRayRate",Icon="fa-solid fa-gear" }
                        }
                    },
            new MenuList {
                            Pages = "pocketBook",
                            MenuItems = new List<MenuItems> {
                                new MenuItems { Title = "ข้อมูลผู้ประกันตน" , Link = "/pocketBook/index",Icon="fa-solid fa-list-check" },
                                new MenuItems { Title = "รายงาน" , Link = "/pocketBook/report" },

                            }
                    },
            new MenuList {
                            Pages = "covid19",
                            MenuItems = new List<MenuItems> {
                                new MenuItems { Title = "สืบค้นข้อมูล", Icon ="fa-solid fa-magnifying-glass",
                                    SubMenus = new List<MenuItems>
                                    { new MenuItems { Title = "ผู้ประกันตน", Link = "/covid19/SearchInformation" },
                                      new MenuItems { Title = "สถานพยาบาล", Link = "/covid19/SearchInformationHos" } } },
                                new MenuItems { Title = "รายการรับแจ้ง" , Link = "/covid19",Icon="fa-solid fa-list-check" },
                                new MenuItems { Title = "สร้างใบคำสั่งจ่าย" , Link = "/covid19/createPaymentOrder" ,Icon="fa-solid fa-file-circle-plus"},
                                new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/covid19/disbursementHistory" ,Icon="fa-solid fa-clock-rotate-left"},
                                new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/covid19/disapprovedHistory" ,Icon="fa-solid fa-file-circle-exclamation"},
                                new MenuItems { Title = "รายงาน" , Link = "/covid19/reportsCovid" ,Icon="fa-regular fa-folder-open" }
                            }
                    },
            new MenuList {
                        Pages = "cardiovascularDisease",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "บันทึกรายการขอเบิก" , Link = "/cardiovascularDisease/index",Icon="fa-solid fa-list"  },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/cardiovascularDisease/PaymentOrders",Icon="fa-regular fa-square-check"  },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/cardiovascularDisease/CardioDisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายการที่ไม่ผ่าน Pre-Audit" , Link = "/cardiovascularDisease/CardioNotPassAudit",Icon="fa-solid fa-file-circle-exclamation" },
                            new MenuItems { Title = "รายงาน" , Link = "/cardiovascularDisease/CardioReport",Icon="fa-regular fa-folder-closed"  },
                            new MenuItems { Title = "กำหนดอัตราค่าบริการ" , Link = "/cardiovascularDisease/CardioRateFee",Icon="fa-regular fa-paste" },
                            new MenuItems { Title = "กำหนดอัตราค่าบริการ adjrw package โรคหัวใจ" , Link = "/cardiovascularDisease/CardioRateFeeAdj",Icon="fa-regular fa-paste" }
                        }
                    },
           new MenuList {
                        Pages = "ODS",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดอัตราค่าบริการ" , Link = "/ods/index",Icon="fa-solid fa-notes-medical" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "/ods/withdrawalrequests",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/ods/paymentorders" ,Icon="fa-regular fa-square-check"},
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/ods/disbursementhistory" ,Icon="fa-solid fa-clock-rotate-left"},
                            new MenuItems { Title = "รายการที่ไม่ผ่าน Pre-Audit" , Link = "/ods/unprocesseddisbursements",Icon="fa-regular fa-rectangle-list" },
                            new MenuItems { Title = "รายงาน" , Link = "/ods/report" ,Icon="fa-regular fa-folder-open"},
                            new MenuItems { Title = "สถานพยาบาลตามสิทธิ์ ODS" , Link = "/ods/rightsofhospital" ,Icon="fa-solid fa-gear" }
                        }
                    },
          new MenuList {
                        Pages = "Hemodialysis",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "สืบค้นข้อมูล", Icon ="fa-solid fa-magnifying-glass",
                                    SubMenus = new List<MenuItems>
                                    { new MenuItems { Title = "ผู้ประกันตน", Link = "/Hemodialysis/SearchInsuredPerson" },
                                      new MenuItems { Title = "สถานพยาบาล", Link = "/Hemodialysis/SearchHospital" }
                                    }
                            },
                            new MenuItems { Title = "ตรวจสอบใบขอเบิก" , Link = "/Hemodialysis/CheckRequisition" ,Icon="fa-solid fa-arrow-up"},
                            new MenuItems { Title = "รายการรับแจ้ง" , Link = "/Hemodialysis/RequestingWithdrawal",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/Hemodialysis/CreateOrderPayment",Icon="fa-regular fa-square-check" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/Hemodialysis/WithdrawalHistory",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายการไม่มีสิทธิ" , Link = "/Hemodialysis/NoWithdrawalHistory" ,Icon="fa-solid fa-file-circle-xmark"  },
                            new MenuItems { Title = "รายงาน" , Link = "/Hemodialysis/Report" ,Icon="fa-regular fa-folder-open" }
                        }
                    },
            new MenuList {
                        Pages = "Methadone",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "รายการขอเบิก" , Link = "/Methadone/AdminIndex",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/Methadone/AdminPaymentOrdersList" , Icon = "fa-solid fa-file-invoice-dollar" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/Methadone/AdminDisbursementHistory" , Icon = "fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายงาน" , Link = "/Methadone/AdminReport" , Icon = "fa-regular fa-folder-open" },
                        }
                    },
            new MenuList {
                        Pages = "ORS",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "บันทึกรายการขอเบิก" , Link = "/ors/index" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/ors/paymentorders" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/ors/disbursementhistory" },
                            new MenuItems { Title = "รายการที่ไม่ผ่าน Pre-Audit" , Link = "/ors/unprocesseddisbursements" },
                            new MenuItems { Title = "รายงาน" , Link = "/ors/report" },
                        }
                    },
            new MenuList {
                        Pages = "FactorDrug",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "รายการขอเบิก" , Link = "/factorDrug/index",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "รายการใบคำสั่งจ่าย" , Link = "/factorDrug/paymentorders",Icon="fa-solid fa-file-invoice-dollar" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/factorDrug/historyDisbursements",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายการที่ไม่ผ่าน Pre-Audit" , Link = "/factorDrug/listNotPassPreAudit",Icon="" },
                            new MenuItems { Title = "รายงาน" , Link = "/factorDrug/report",Icon="fa-regular fa-folder-open" },
                        }
                    },
            new MenuList {
                        Pages = "organMaintenance",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "บันทึกค่าบำรุงรักษาอวัยวะ" , Link = "/organMaintenance/index" },
                            new MenuItems { Title = "รายการรับแจ้ง" , Link = "" },
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/organMaintenance/createPaymentOrder" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "" },
                            new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "" },
                            new MenuItems { Title = "รายงาน" , Link = "" },
                            new MenuItems { Title = "กำหนดอัตราจ่าย" , Link = "" }
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
                            new MenuItems { Title = "สืบค้นข้อมูลผู้ประกันตน" , Link = "/Dental/SearchInsuredInformation" ,Icon="fa-solid fa-magnifying-glass"},
                            new MenuItems { Title = "อนุญาตการใช้รถทันตกรรม" , Link = "/Dental/index" ,Icon="fa-solid fa-truck-fast"},
                            new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/Dental/NotificationList",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/Dental/PaymentOrder" ,Icon="fa-regular fa-square-check"},
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/Dental/DisBurseMentList",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/Dental/DisapprovedTranHistory",Icon="fa-solid fa-file-circle-xmark" },
                            new MenuItems { Title = "รายงาน" , Link = "/Dental/Report" ,Icon="fa-solid fa-folder-open"}
                        }
                    },
            new MenuList {
                        Pages = "DentalCentral",
                        MenuItems = new List<MenuItems> {

                            new MenuItems { Title = "อนุญาตการใช้รถทันตกรรม" , Link = "/DentalCentral/index", Icon="fa-solid fa-truck-fast"},
                            new MenuItems { Title = "รายการรับเเจ้ง" , Link = "/DentalCentral/NotificationList" ,Icon="fa-solid fa-list-check"},
                            new MenuItems { Title = "สร้างคำสั่งจ่าย" , Link = "/DentalCentral/PaymentOrderList",Icon="fa-regular fa-square-check" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/DentalCentral/DisBurseMent" ,Icon="fa-solid fa-clock-rotate-left"},
                            new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/DentalCentral/DisapprovedTranHistory",Icon="fa-solid fa-file-circle-xmark"  },
                            new MenuItems { Title = "รายงาน" , Link = "/DentalCentral/Report" ,Icon="fa-solid fa-folder-open"}
                        }
                    },
            new MenuList {
                        Pages = "DentalCentralSerach",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "สืบค้นข้อมูลผู้ประกันตน" , Link = "/DentalCentralSerach/index",Icon="fa-solid fa-magnifying-glass" },
                            new MenuItems { Title = "เลือกหน่วยปฏิบัติงาน" , Link = "/DentalCentralSerach/selectunit" ,Icon = "fa-solid fa-location-dot"},

                        }
                    },
            new MenuList {
                        Pages = "labhiv",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "กำหนดอัตราการจ่าย" , Link = "/labhiv/SetThePayRate",Icon="fa-solid fa-truck-fast" },
                            new MenuItems { Title = "รายการขอเบิก" , Link = "/labhiv/WithDrawal",Icon="fa-solid fa-list-check" },
                            new MenuItems { Title = "ใบคำสั่งจ่าย" , Link = "/labhiv/OrderList",Icon="fa-solid fa-square-check" },
                            new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/labhiv/DisbursementHistory",Icon="fa-solid fa-clock-rotate-left" },
                            new MenuItems { Title = "รายงาน" , Link = "/labhiv/Report",Icon="fa-solid fa-folder-open" },
                        }
                    },
             new MenuList {
                            Pages = "medicalSpecialist",
                            MenuItems = new List<MenuItems> {
                                new MenuItems { Title = "สืบค้นข้อมูล", Icon ="fa-solid fa-magnifying-glass",
                                    SubMenus = new List<MenuItems>
                                    { new MenuItems { Title = "ผู้ประกันตน", Link = "/medicalSpecialist/SearchInformationMedical" },
                                      new MenuItems { Title = "สถานพยาบาล", Link = "/medicalSpecialist/SearchInformationHos" } } },
                                new MenuItems { Title = "ตรวจสอบใบขอเบิก" , Link = "/medicalSpecialist/CheckTheRequisitionForm",Icon="fa-solid fa-list-check" },
                                new MenuItems { Title = "รายการรับแจ้ง" , Link = "/medicalSpecialist/NotificationList",Icon="fa-solid fa-list-check" },
                                new MenuItems { Title = "สร้างใบคำสั่งจ่าย" , Link = "/medicalSpecialist/createPaymentOrder" ,Icon="fa-solid fa-file-circle-plus"},
                                new MenuItems { Title = "ประวัติการเบิกจ่าย" , Link = "/medicalSpecialist/disbursementHistory" ,Icon="fa-solid fa-clock-rotate-left"},
                                new MenuItems { Title = "ประวัติรายการที่ไม่อนุมัติ" , Link = "/medicalSpecialist/disapprovedHistory" ,Icon="fa-solid fa-file-circle-exclamation"},
                                new MenuItems { Title = "รายงาน" , Link = "/medicalSpecialist/reportMedicalSpeciaList" ,Icon="fa-regular fa-folder-open" }
                            }
                    },
              new MenuList {
                        Pages = "restore",
                        MenuItems = new List<MenuItems> {
                            new MenuItems { Title = "สืบค้นข้อมูลผู้ประกันตน" , Link = "/restore/SearchInformation", Icon="fa-solid fa-magnifying-glass" },
                            new MenuItems { Title = "ข้อมูลผู้ประกันตน" , Link = "/restore/Information",Icon="fa-solid fa-list-check" },

                            new MenuItems { Title = "รายงาน" , Link = "/restore/Report",Icon="fa-solid fa-folder-open" },
                        }
                    },
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
