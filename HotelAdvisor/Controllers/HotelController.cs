using HotelAdvisor.Managers;
using HotelAdvisor.Models;
using System;
using System.Collections.Generic;
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
                HotelManager manager = new HotelManager();
                Hotel hotel = new Hotel();
                hotel.Name = model.Name;
                hotel.Description = model.Description;
                hotel.City = model.City;
                hotel.Address = model.Address;
                hotel.HouseNumber = model.HouseNumber;
                hotel.IsActive = model.IsActive;
                hotel.Image = model.Image;
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

            return View(hotel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(HotelViewModel model)
        {
            if (ModelState.IsValid)
            {

                HotelManager manager = new HotelManager();
                Hotel hotel = new Hotel();
                hotel.Id = model.Id;
                hotel.Name = model.Name;
                hotel.Description = model.Description;
                hotel.City = model.City;
                hotel.Address = model.Address;
                hotel.HouseNumber = model.HouseNumber;
                hotel.IsActive = model.IsActive;
                hotel.Image = hotel.Image;

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