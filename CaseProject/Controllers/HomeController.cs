using Case.BLL.Services;
using Case.DAL.Context;
using Case.Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    
    public class HomeController : Controller
    {       
        private readonly IFormService formService;
        private readonly ProjectContext context;
        private readonly IFieldService fieldService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IFormService formService,ProjectContext context, IFieldService fieldService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.formService = formService;
            this.context = context;
            this.fieldService = fieldService;
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

        [Authorize(Policy = "RequireLoggedIn")]
        [HttpGet("/forms/{formId}")]
        public IActionResult Forms(int formId)
        {

            var formName = formService.FormGetById(formId).Name;
            var formDescription = formService.FormGetById(formId).Description;

            var getAllFieldsByFormID = fieldService.GetAllFields(formId);

            ViewBag.formName = formName;
            ViewBag.formDescription = formDescription;
            return View(getAllFieldsByFormID);
        }


        [HttpPost]
        public JsonResult CreateFrom([FromBody] Form form)
        {
            formService.CreateForm(form);

            return Json(new { success = true, message = "Form başarıyla kaydedildi.", redirectTo = Url.Action("Index", "Home") });
        }

        [Authorize(Policy = "RequireLoggedIn")]
        [HttpGet]
        public IActionResult AddFields(int id)
        {
            ViewBag.Name = formService.FormGetById(id).Name;
            return View(id);
        }

        [HttpPost]
        public JsonResult AddFields([FromBody] List<Field> fields)
        {
            if (ModelState.IsValid)
            {
                fieldService.InsertFields(fields);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "The submitted data is invalid." });
            }
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
