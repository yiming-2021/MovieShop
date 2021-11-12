using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserLoginRequestModel
    {
        [Required(ErrorMessage ="Email cannot be empty")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
