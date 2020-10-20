using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage ="El campo Nombre es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="El campo Apellido es obligatorio")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo Cédula de Identidad es obligatorio")]
        public string CI { get; set; }
        [Required(ErrorMessage = "El campo Número de teléfono es obligatorio ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "El formato teléfono ingresado no es válido.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "El campo del Correo Electrónico es obligatorio")]
        public string Email { get; set; }
        [Display(Name = "Contaseña")]
        [Required(ErrorMessage = "El campo Contraseña en obligatorio.")]
        [StringLength(100, ErrorMessage = "El número de carácteres de {0} debe ser al menos {2}.", MinimumLength = 8)]
        public string Password { get; set; }
        //[Required]
        public string Role { get; set; }
    }
}
