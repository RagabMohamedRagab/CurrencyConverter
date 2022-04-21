using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class RegisterDTO
    {
       
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Compare("Email", ErrorMessage = "Not Equal Origin Email")]
        [DataType(DataType.EmailAddress)]
       
        public string ConfirmEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Not Equal Origin Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.PhoneNumber)]
      
        [Compare("Phone", ErrorMessage = "Not Equal Origin Phone")]
        public string ConfirmPhone { get; set; }
    }
}




