using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname field is requiredo")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Identity Card field is required")]
        public string CI { get; set; }
        [Required(ErrorMessage = "The Phone Number field is required ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "The phone format entered is not valid.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email field is required")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password field is required.")]
        [StringLength(100, ErrorMessage = "The number of characters in {0} must be at least {2}.", MinimumLength = 8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        public string Role { get; set; }

        //Propiedades
        public string ID { get; set; }
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [TempData]
        public string  ErrorMessage { get; set; }
    }
}
