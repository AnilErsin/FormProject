using Case.BLL.Services;
using Case.DAL.Context;
using Case.Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {       
        private readonly IFormService formService;
        private readonly ProjectContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IFormService formService,ProjectContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.formService = formService;
            this.context = context;
        }

        [Authorize(Policy = "RequireLoggedIn")]
        public IActionResult Index()
        {
            var form = formService.GetAllForm().AsEnumerable();
            return View(form);
        }
        public IActionResult Search(string searchTerm, bool exactMatch = false)
        {
            var forms = context.Forms.AsQueryable();

            if (exactMatch)
            {
                forms = forms.Where(f => f.Name == searchTerm);
            }
            else
            {
                forms = forms.Where(f => f.Name.Contains(searchTerm));
            }

            var result = forms.ToList();

            return View("Index", result);
        }


        [HttpPost]
        public JsonResult CreateFrom([FromBody] Form form)
        {
            formService.CreateForm(form);

            return Json(new { success = true, message = "Form başarıyla kaydedildi.", redirectTo = Url.Action("Index", "Home") });
        }

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
                        return RedirectToAction("Index","Home");
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
