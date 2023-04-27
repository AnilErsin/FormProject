using Case.BLL.Repository;
using Case.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.BLL.Services
{
    public class FieldService : IFieldService
    {
        private readonly IRepository<Field> fieldRepository;

        public FieldService(IRepository<Field> fieldRepository)
        {
            this.fieldRepository = fieldRepository;
        }
        public void CreateField(Field field)
        {
            fieldRepository.Insert(field);
        }

        public Field FieldGetById(int id)
        {
            return fieldRepository.GetById(id);
        }

        public IEnumerable<Field> GetAllForm()
        {
            return fieldRepository.GetAll().ToList();
        }

        public void RemoveField(Field field)
        {
            fieldRepository.Remove(field);
        }

        public void UpdateField(Field field)
        {
            fieldRepository.Update(field);
        }
    }
}
