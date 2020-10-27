using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Areas.Customers.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "The CI field is required.")]
        public string CI { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname field is requiredo")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Direction field is required")]
        public string Direction { get; set; }

        [Required(ErrorMessage = "Phone field is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "The phone format entered is not valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date field is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public bool Credit { get; set; }
        public byte[] Image { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
