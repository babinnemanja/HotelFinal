using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvisor.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Please, enter the name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name="House number")]
        public int HouseNumber { get; set; }

        public string City { get; set; }

        public string Image { get; set; }

        [Display(Name="Is active")]
        public bool IsActive { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}