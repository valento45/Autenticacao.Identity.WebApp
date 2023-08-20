using System.ComponentModel.DataAnnotations;

namespace Identity.WebApp.Models
{
    public class RegisterModel
    {
       
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
