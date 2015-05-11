using HotelAdvisor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvisor.Managers
{
    public class CommentManager
    {
        public void Create(Comment comment)
        {
            using (var context = new HotelAdvisorContext())
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
        }
    }
}