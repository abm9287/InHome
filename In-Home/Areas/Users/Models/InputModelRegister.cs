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
        //Campo nombre
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        //Campo apellido
        [Required(ErrorMessage = "Surname field is requiredo")]
        public string LastName { get; set; }
        //Campo CI
        [Required(ErrorMessage = "The Identity Card field is required")]
        public string CI { get; set; }
        //Campo  Número de teléfono
        [Required(ErrorMessage = "The Phone Number field is required ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "The phone format entered is not valid.")]
        public string PhoneNumber { get; set; }
        //Campo Email
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string Email { get; set; }
        //Campo contraseña
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password field is required.")]
        [StringLength(100, ErrorMessage = "The number of characters in {0} must be at least {2}.", MinimumLength = 8)]
        public string Password { get; set; }
       
        //Campo rol
        public string Role { get; set; }

        //Propiedades
        public string ID { get; set; }
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
