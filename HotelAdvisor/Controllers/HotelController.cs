using HotelAdvisor.Managers;
using HotelAdvisor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelAdvisor.Controllers
{
    public class HotelController : Controller
    {
        // GET: Hotel
        public ActionResult Index()
        {
            HotelManager manager = new HotelManager();
            List<HotelDetailsViewModel> model = manager.GetHotels();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(HotelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = string.Empty;

                if (model != null && model.Image.ContentLength > 0)
                {
                    // extract only the fielname
                    fileName = Path.GetFileName(model.Image.FileName);
                    // store the file inside ~/Content/HotelImages folder
                    var path = Path.Combine(Server.MapPath("~/Content/HotelImages"), fileName);
                    model.Image.SaveAs(path);
                }

                HotelManager manager = new HotelManager();
                Hotel hotel = new Hotel();
                hotel.Name = model.Name;
                hotel.Description = model.Description;
                hotel.City = model.City;
                hotel.Address = model.Address;
                hotel.HouseNumber = model.HouseNumber;
                hotel.IsActive = model.IsActive;
                hotel.Image = "/Content/HotelImages/" + fileName;
                manager.Create(hotel);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            HotelManager manager = new HotelManager();
            Hotel model = manager.Find(id);

            HotelViewModel hotel = new HotelViewModel();
            hotel.Id = model.Id;
            hotel.Name = model.Name;
            hotel.Description = model.Description;
            hotel.City = model.City;
            hotel.Address = model.Address;
            hotel.HouseNumber = model.HouseNumber;
            hotel.IsActive = model.IsActive;
            hotel.ImagePath = model.Image;

            return View(hotel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(HotelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = string.Empty;

                if (model.Image != null)
                {
                    // extract only the fielname
                    fileName = Path.GetFileName(model.Image.FileName);
                    // store the file inside ~/Content/HotelImages folder
                    var path = Path.Combine(Server.MapPath("~/Content/HotelImages"), fileName);
                    model.Image.SaveAs(path);
                }

                HotelManager manager = new HotelManager();
                Hotel hotel = new Hotel();
                hotel.Id = model.Id;
                hotel.Name = model.Name;
                hotel.Description = model.Description;
                hotel.City = model.City;
                hotel.Address = model.Address;
                hotel.HouseNumber = model.HouseNumber;
                hotel.IsActive = model.IsActive;
                hotel.Image = string.IsNullOrEmpty(fileName) ? null : "/Content/HotelImages/" + fileName; 

                manager.Edit(hotel);

                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult Details(int id)
        {
            HotelManager manager = new HotelManager();
            HotelDetailsViewModel model = manager.GetHotelDetails(id);
            return View(model);
        }
    }
}