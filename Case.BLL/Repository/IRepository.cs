using Case.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.BLL.Repository
{
    public interface IRepository<T> where T:BaseClass
    {
        IEnumerable<T> GetAll();
        void InsertAll(List<T> entities);
        void Insert(T entity);

        void Remove(T entity);
        void Update(T entity);
        T GetById(int id);
        string SaveChanges();
        IEnumerable<T> GetAll(int formID);
    }
}
