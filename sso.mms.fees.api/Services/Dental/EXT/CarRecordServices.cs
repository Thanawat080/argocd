using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sso.mms.fees.api.Interface.Dental.EXT;
using sso.mms.fees.api.Entities.Dental;
using sso.mms.fees.api.ViewModels.Dental;
using System.Linq;

namespace sso.mms.fees.api.Services.Dental.EXT
{

    public class CarRecordServices : ICarRecord
    {
        private readonly DentalContext db;
        public CarRecordServices(DentalContext DbContext)
        {
            db = DbContext;
        }
        public async Task<List<AaiDentalCarHView>> GetAll()
        {

            try
            {
                return   await db.AaiDentalCarHs
                .Include(p => p.AaiDentalCarDs) // Including the related entity AaiDentalCarDs
                .Select(p => new AaiDentalCarHView
                {
                    DentalCarHId = p.DentalCarHId,
                    HospitalCode = p.HospitalCode,
                    PlaceName = p.PlaceName,
                    ServiceDate = p.ServiceDate,
                    ServiceStartDate = p.ServiceStartDate,
                    ServiceEndDate = p.ServiceEndDate,
                    PlaceProvince = p.PlaceProvince,
                    PlaceDistrict = p.PlaceDistrict,
                    PlaceSubDistrict = p.PlaceSubDistrict,
                    //  CarType = p.AaiDentalCarDs.CarType, // You can select specific properties from the included entity
                    RegisterDoc = p.RegisterDoc,
                    RegisterDocFileName = p.RegisterDocFileName,
                    DantalCarStatus = p.DantalCarStatus,
                    ChangeDesc = p.ChangeDesc,
                    CreateBy = p.CreateBy,
                    CreateDate = p.CreateDate,
                    UpdateBy = p.UpdateBy,
                    UpdateDate = p.UpdateDate,
                    sumCar = p.AaiDentalCarDs.Count,
                    AllLicensePlate = string.Join($",", p.AaiDentalCarDs.Select(d => d.LicensePlate)),
                    AaiDentalCarDs = p.AaiDentalCarDs.ToList(), // You can include the related entity itself if needed
                })
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<string> AddCarRecord(InsertDataViewModel carData)
        {
            
            try
            {
                if (carData.carH != null)
                {
                    AaiDentalCarH data = new AaiDentalCarH()
                    {
                       
                        HospitalCode = carData.carH.HospitalCode,
                        PlaceName = carData.carH.PlaceName,
                        ServiceDate = carData.carH.ServiceDate,
                        ServiceStartDate = carData.carH.ServiceStartDate,
                        ServiceEndDate = carData.carH.ServiceEndDate,
                        PlaceProvince = carData.carH.PlaceProvince,
                        PlaceDistrict = carData.carH.PlaceDistrict,
                        PlaceSubDistrict = carData.carH.PlaceSubDistrict,
                        RegisterDoc = carData.carH.RegisterDoc,
                        RegisterDocFileName = carData.carH.RegisterDocFileName,
                        DantalCarStatus = "0",
                        ChangeDesc = carData.carH.ChangeDesc,
                        CreateBy = carData.carH.CreateBy,
                        CreateDate = DateTime.Now,
                        UpdateBy = carData.carH.UpdateBy,
                        UpdateDate = DateTime.Now,
                        SsoOrgId = carData.carH.SsoOrgId
                    };
                    await db.AaiDentalCarHs.AddAsync(data);
                    await db.SaveChangesAsync();
                    if(carData.carD != null)
                    {
                        List <AaiDentalCarDViewModel> carD = new List<AaiDentalCarDViewModel>();
                       
                        foreach (var item in carData.carD)
                        {
                            AaiDentalCarD car = new AaiDentalCarD()
                            {
                               
                                DentalCarHId = db.AaiDentalCarHs.OrderByDescending(o => o.CreateDate).First().DentalCarHId,
                                LicensePlate = item.LicensePlate,
                                Remark = item.Remark,
                                CreateDate = DateTime.Now,
                                CreateBy = item.CreateBy,
                                UpdateDate = DateTime.Now,
                                UpdateBy = item.UpdateBy,
                                CarType = item.CarType
                            };
                            await db.AaiDentalCarDs.AddAsync(car);
                            await db.SaveChangesAsync();
                        }
                        
                    }

                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> UpdateCarHChangeDes(ChangeDesViewModel carHData)
        {
            try
            {
                var carHdataById =  db.AaiDentalCarHs.FirstOrDefault(w => w.DentalCarHId == carHData.DentalCarHId);
                if(carHdataById != null)
                {
                    carHdataById.ChangeDesc = carHData.ChangeDesc;
                    carHdataById.DantalCarStatus = "8";
                    db.SaveChanges();
                }
                return "success";
            }catch (Exception ex)
            {
                return ex.Message;
            }
           
        }
    }
}
