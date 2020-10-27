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
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Email is not a valid email addressa.")]
        public string Email { get; set; }
        //Campo contraseña
        [Display(Name = "Password")]
        [DataType(DataType.Password)]//Oculta la contraseña
        [Required(ErrorMessage = "Password field is required.")]
        [StringLength(100, ErrorMessage = "The number of characters in {0} must be at least {2}.", MinimumLength = 8)]
        public string Password { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
