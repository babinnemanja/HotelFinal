using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelAdvisor.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Comment text is required.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Comment title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Comment rating is required")]
        public decimal Rating { get; set; }

       // [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
       // public string DateAddedString { get; set; }
        public DateTime DateAdded { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}