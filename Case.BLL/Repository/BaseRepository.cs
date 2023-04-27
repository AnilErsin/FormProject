using Case.DAL.Context;
using Case.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.BLL.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseClass
    {
        private ProjectContext _context;
        private DbSet<T> _entites;
        public BaseRepository(ProjectContext context)
        {
            _context = context;
            _entites = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entites.AsEnumerable();
        }

        public IEnumerable<T> GetAll(int formID)
        {
            return _entites.ToList();
        }

        public T GetById(int id)
        {
            return _entites.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                _entites.Add(entity);
                SaveChanges();

            }
        }

        public void InsertAll(List<T> entities)
        {
            _entites.AddRange(entities);
            SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity != null)
            {
                _entites.Remove(entity);
                SaveChanges();
            }
        }

        public string SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return "İşlem Başarıyla Gerçekleşti";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
                SaveChanges();
            }
        }
    }
}
