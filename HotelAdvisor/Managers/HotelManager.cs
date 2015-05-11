﻿using HotelAdvisor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HotelAdvisor.Managers
{
    public class HotelManager
    {

        private Hotel hotel;
        public HotelManager() { }

        public void Create(Hotel hotel)
        {
            using (var context = new HotelAdvisorContext())
            {
                context.Hotels.Add(hotel);
                context.SaveChanges();
            }
        }

        public void Edit(Hotel newHotel)
        {
            using (var dbContext = new HotelAdvisorContext())
            {
                this.hotel = dbContext.Hotels.Find(newHotel.Id);
                this.Populate(newHotel);
                dbContext.Entry(hotel).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }

        }

        public Hotel Find(int id)
        {
            using (var dbContext = new HotelAdvisorContext())
            {
                this.hotel = dbContext.Hotels.Find(id);
            }

            return hotel;
        }

        public List<HotelDetailsViewModel> GetHotels()
        {

            List<HotelDetailsViewModel> hotels = new List<HotelDetailsViewModel>();

            using (var dbContext = new HotelAdvisorContext())
            {
                List<Hotel> hotelList = dbContext.Hotels.ToList();

                foreach (var hotel in hotelList)
                    hotels.Add(
                        new HotelDetailsViewModel()
                        {
                            HotelName = hotel.Name,
                            HotelId = hotel.Id,
                            Description = hotel.Description,
                            Address = hotel.Address,
                            City = hotel.City,
                            HouseNumber = hotel.HouseNumber,
                            AverageRating = hotel.Comments.Count() == 0 ? 0 : hotel.Comments.Average(c => c.Rating)
                            //TotalReviews = hotel.Comments.Count(),
                            //Comments = hotel.Comments.Select(co => new CommentViewModel() 
                            //{ 
                            //    UserName = co.User.UserName,
                            //    Text = co.Text,
                            //    DateAdded = co.DateAdded,
                            //    Rating = co.Rating,
                            //    Id = co.Id
                            //}).ToList()

                        }
                        );
            }

            return hotels;
        }


        public HotelDetailsViewModel GetHotelDetails(int hotelId)
        {
            HotelDetailsViewModel hotelDetails = new HotelDetailsViewModel();

            using (var context = new HotelAdvisorContext())
            {
                hotelDetails = context.Hotels.Where(h => h.Id == hotelId).Include(h => h.Comments).Select(
                    s => new HotelDetailsViewModel()
                    {
                        HotelId = s.Id,
                        HotelName = s.Name,
                        City = s.City,
                        Description = s.Description,
                        Address = s.Address,
                        Image = s.Image,
                        TotalReveiws = s.Comments.Count(),
                        AverageRating = (s.Comments.Count() == 0) ? 0 : s.Comments.Average(c => c.Rating),
                        Comments = s.Comments.ToList()
                    }).FirstOrDefault();
            }

            return hotelDetails;
        }

        private void Populate(Hotel newHotel)
        {
            this.hotel.Id = newHotel.Id;
            this.hotel.Name = newHotel.Name;
            this.hotel.Address = newHotel.Address;
            this.hotel.Description = newHotel.Description;
            this.hotel.Image = newHotel.Image;
            this.hotel.City = newHotel.City;
            this.hotel.HouseNumber = newHotel.HouseNumber;
            this.hotel.IsActive = newHotel.IsActive;
        }


    }
}