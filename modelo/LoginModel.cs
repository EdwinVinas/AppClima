using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
