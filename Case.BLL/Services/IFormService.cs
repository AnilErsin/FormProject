using Case.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.BLL.Services
{
    public interface IFormService
    {
        IEnumerable<Form> GetAllForm();
        void CreateForm(Form form);
        void RemoveForm(Form form);
        void UpdateForm(Form form);
        Form FormGetById(int id);
    }
}
