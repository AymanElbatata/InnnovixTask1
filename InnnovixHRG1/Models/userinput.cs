using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InnnovixHRG1.Models
{
    public class userinput
    {
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryIDD { get; set; }

        [Required]
        //[Range(0, 2)]
        [Display(Name = "Adult")]
        public string adults { get; set; }

        [Required]
        //[Range(0, 2)]
        [Display(Name = "Child")]
        public string children { get; set; }

        [Required]
        [Display(Name = "Room Rate")]
        public int RoomTypeIDD { get; set; }

        [Required]
        [Display(Name = "Meal Plan")]
        public int MealTypeIDD { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check in-Date")]
        public DateTime checkINdate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check out-Date")]
        public DateTime checkOUTdate { get; set; }



    }
}