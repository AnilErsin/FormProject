using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Username Girilmedi!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre Girilmedi!")]
        public string Password { get; set; }
    }
}
