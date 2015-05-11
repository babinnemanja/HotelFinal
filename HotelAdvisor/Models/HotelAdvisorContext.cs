using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelAdvisor.Models
{
    public class HotelAdvisorContext : IdentityDbContext<ApplicationUser>
    {
        public HotelAdvisorContext() : base("DefaultConnection")
        {
        }

        public static HotelAdvisorContext Create()
        {
            return new HotelAdvisorContext();
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}