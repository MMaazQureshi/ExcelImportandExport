using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingWorldProject.Models;
using TrackingWorldProject.ViewModel;

namespace TrackingWorldProject.Controllers
{
    public class HomeController : Controller
    { FleetContext context;
        public HomeController()
        {
            context = new FleetContext();
        }
        // GET: Home
        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(VehicleViewModel vehicleModel)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Active = vehicleModel.Active;
            vehicle.ChasisNo = vehicleModel.ChasisNo;
            vehicle.Color = vehicleModel.Color;
            vehicle.DateOfPurchase = DateTime.Parse(vehicleModel.DateOfPurchase);
            vehicle.EngineNo = vehicleModel.EngineNo;
            vehicle.Make = vehicleModel.Make;
            vehicle.Model = vehicleModel.Model;
            vehicle.RegNo = vehicleModel.RegNo;
            context.Vehicles.Add(vehicle);
            context.SaveChanges();
            return View("Upload");
        }
        //[Route("Home/index")]
        public ActionResult Upload()
        {
            var DBrecords = context.Vehicles.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;


                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }

                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    reader.Close();

                    return View(result.Tables[0]);
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }
        [HttpPost]
        public void CreateDatabaseIfNotExists()
        {
            using (var dbContext = new FleetContext())
            {
                if (!dbContext.Database.Exists())
                    dbContext.Database.Create();
            }
        }
        public PartialViewResult GetDbRecords()
        {
            var records = context.Vehicles.ToList();
            List<VehicleViewModel> models = new List<VehicleViewModel>();
            foreach (var vehicle in records)
            {
                VehicleViewModel VM = new VehicleViewModel()
                {
                    VehicleId=vehicle.VehicleId,
                    Active = vehicle.Active,
               ChasisNo = vehicle.ChasisNo,
               Color = vehicle.Color,
                DateOfPurchase = vehicle.DateOfPurchase.ToShortDateString(),
                EngineNo = vehicle.EngineNo,
                Make = vehicle.Make,
                Model = vehicle.Model,
                RegNo = vehicle.RegNo,
            };
                models.Add(VM);
            }
            return PartialView(models);
        }
        
        public ActionResult Edit(long id)
        {
            //var VehicleId = long.Parse(Vehicleid);
            var vehicle = context.Vehicles.Where(x => x.VehicleId == id).FirstOrDefault();
            VehicleViewModel VM = new VehicleViewModel()
            {
                VehicleId = vehicle.VehicleId,
                Active = vehicle.Active,
                ChasisNo = vehicle.ChasisNo,
                Color = vehicle.Color,
                DateOfPurchase = vehicle.DateOfPurchase.ToShortDateString(),
                EngineNo = vehicle.EngineNo,
                Make = vehicle.Make,
                Model = vehicle.Model,
                RegNo = vehicle.RegNo,
            };
            return View(VM);
        }
        [HttpPost]
        public ActionResult Edit(VehicleViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = context.Vehicles.Where(x => x.VehicleId == vehicleModel.VehicleId).First();
                vehicle.Active = vehicleModel.Active;
                vehicle.ChasisNo = vehicleModel.ChasisNo;
                vehicle.Color = vehicleModel.Color;
                vehicle.DateOfPurchase =DateTime.Parse(vehicleModel.DateOfPurchase);
                vehicle.EngineNo = vehicleModel.EngineNo;
                vehicle.Make = vehicleModel.Make;
                vehicle.Model = vehicleModel.Model;
                vehicle.RegNo = vehicleModel.RegNo;
                context.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("Upload");

        }
        public ActionResult InsertVehicles(VehicleViewModel[] vehiclesArray)
        {
            foreach(var vehicleModel in vehiclesArray)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Active = vehicleModel.Active;
                vehicle.ChasisNo = vehicleModel.ChasisNo;
                vehicle.Color = vehicleModel.Color;
                vehicle.DateOfPurchase = DateTime.Parse(vehicleModel.DateOfPurchase);
                vehicle.EngineNo = vehicleModel.EngineNo;
                vehicle.Make = vehicleModel.Make;
                vehicle.Model = vehicleModel.Model;
                vehicle.RegNo = vehicleModel.RegNo;
                context.Vehicles.Add(vehicle);
                context.SaveChanges();

            }
            return View("Upload", null);
        }
        
        public ActionResult Delete(long id)
        {
            var vehicle = context.Vehicles.Where(x => x.VehicleId == id).FirstOrDefault();
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
            return RedirectToAction("Upload");
        }
    }
}