using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Username Girilmedi!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre Girilmedi!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre (Tekrar) Girilmedi!")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor!")]
        public string ConfirmPassword { get; set; }
    }
}
