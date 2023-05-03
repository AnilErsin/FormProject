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

    [Authorize(Policy = "RequireLoggedIn")]
    public class HomeController : Controller
    {       
        private readonly IFormService formService;
        private readonly ProjectContext context;
        private readonly IFieldService fieldService;

        public HomeController(IFormService formService,ProjectContext context, IFieldService fieldService)
        {
           
            this.formService = formService;
            this.context = context;
            this.fieldService = fieldService;
        }

       
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
   
    }
}
