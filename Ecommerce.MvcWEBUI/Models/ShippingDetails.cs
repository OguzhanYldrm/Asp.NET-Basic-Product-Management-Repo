using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.MvcWEBUI.Models
{
    public class ShippingDetails
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter address title.")]
        public string AddressTitle { get; set; }
        [Required(ErrorMessage = "Please enter address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a city.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a state.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter your zipcode.")]
        public string Zipcode { get; set; }

    }
}