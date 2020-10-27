using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Areas.Users.Models
{
    public class InputModelLogin
    {
        [Required(ErrorMessage = "The C.I. field is required.")]
        public string CI { set; get; }
        [Required(ErrorMessage = "The name field is required.")]
        public string Name { set; get; }
        [Required(ErrorMessage = "The surname field is required.")]
        public string LastName { set; get; }
        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string Email { set; get; }
        [Required(ErrorMessage = "The address field is required.")]
        public string Direction { set; get; }
        [Required(ErrorMessage = "Phone field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "The telephone format entered is not valid.")]
        public string Phone { set; get; }
        [Required(ErrorMessage = "The date field is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { set; get; }
        public bool Credit { set; get; }
        public byte[] Image { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
