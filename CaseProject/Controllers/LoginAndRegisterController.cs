using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public LoginAndRegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerUser)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser()
                {
                    UserName = registerUser.Username,


                };


                var result = await userManager.CreateAsync(newUser, registerUser.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(registerUser);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginUser.Username);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }
    }
}
