using Case.BLL.Repository;
using Case.DAL.Context;
using Case.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.BLL.Services
{
    public class FormService : IFormService
    {
        private readonly IRepository<Form> formRepository;
        private readonly ProjectContext context;

        public FormService(IRepository<Form> formRepository,ProjectContext context)
        {
            this.formRepository = formRepository;
            this.context = context;
        }
        public void CreateForm(Form form)
        {
            formRepository.Insert(form);
        }

        public Form FormGetById(int id)
        {
            return formRepository.GetById(id);
        }

        public IEnumerable<Form> GetAllForm()
        {
            return formRepository.GetAll().ToList();
        }

        public void RemoveForm(Form form)
        {
           formRepository.Remove(form);
        }

        public void UpdateForm(Form form)
        {
           formRepository.Update(form);
        }
    }
}
