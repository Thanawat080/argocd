//using Microsoft.AspNetCore.Components;
//using sso.mms.portal.ext.ViewModels;
//using System.Collections.Generic;
//using System.Security.Cryptography.X509Certificates;

//namespace sso.mms.portal.ext.Shared
//{

    
//    public List<ImageAssetDetailList> ImageListDetail = new list<ImageAssetDetailList>;
//    public List<ImageAssetMainDetailList> ImageAssetMainDetailList = new List<ImageAssetMainDetailList> ;





//    ImageListDetail = new List<ImageAssetDetailList>()
//            {
//             new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-hospital",
//                DetailImagePath = "ข้อมูลสถานพยาบาล",

//            },
//            new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-file-invoice",
//                DetailImagePath = "สถานะการเบิกจ่าย",

//            },
//            new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-address-book",
//                DetailImagePath = "บัตรรับรองการขึ้นทะเบียน",

//            },
//            new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-comments",
//                DetailImagePath = "Online Chat",

//            },
//            new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-bell",
//                DetailImagePath = "การแจ้งเตือน",

//            },
//            new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-user-gear",
//                DetailImagePath = "การจัดการผู้ใช้งาน",
//            },
//            new ImageAssetDetailList()
//    {
//        ImagePath = "fa-solid fa-clipboard-list",
//                DetailImagePath = "จำนวนผู้ประกันที่เลือกสถานพยาบาล",

//            }
//};


//ImageAssetMainDetailList = new List<ImageAssetMainDetailList>()
//            {
//              new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/1.png",
//                DetailImagePath = "ตามภาระเสี่ยง",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/2.png",
//                DetailImagePath = "AdJRW >= 2",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/3.png",
//                DetailImagePath = "ODS",
//                Height = "70px",
//                Width = "70px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/4.png",
//                DetailImagePath = "ORS",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/5.png",
//                DetailImagePath = "Packageโรคหัวใจและหลอดเลือด",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/6.png",
//                DetailImagePath = "ปลูกถ่ายอวัยวะ",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/7.png",
//                DetailImagePath = "ฮีโมฟีเลีย",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/8.png",
//                DetailImagePath = "ส่งเสริมสุขภาพป้องกันโรค",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/9.png",
//                DetailImagePath = "Lab HIV",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/10.png",
//                DetailImagePath = "วัคซีน",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/11.png",
//                DetailImagePath = "สารเมทาโดน",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/12.png",
//                DetailImagePath = "รักษาเฉพาะทาง และทำหมัน",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/13.png",
//                DetailImagePath = "ทุพพลภาพ",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/14.png",
//                DetailImagePath = "ฟอกเลือดด้วยเครื่องไตเทียม",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/15.png",
//                DetailImagePath = "เจ็บป่วยฉุกเฉิน วิกฤต UCEP",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/16.png",
//                DetailImagePath = " เจ็บป่วยฉุกเฉิน72 ชม.",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/17.png",
//                DetailImagePath = "บำรุงรักษาอวัยวะสภากาชาดไทย",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/18.png",
//                DetailImagePath = "COVID-19",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/19.png",
//                DetailImagePath = "ทันตกรรม",
//                Height = "75px",
//                Width = "75px"

//            },
//            new ImageAssetMainDetailList() {
//                ImagePath = "Data/Assets/Images/imagemainmenu/20.png",
//                DetailImagePath = "การฟื้นฟูด้านอาชีพ และการแพทย์",
//                Height = "75px",
//                Width = "75px"

//            },

//            };


