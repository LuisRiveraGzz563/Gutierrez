using GutierrezAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GutierrezAPI.Repositories
{
    public class Repository<T>(GutierrezdbContext context) : IRepository<T> where T : class
    {
        public DbSet<T> GetAll()
        {
            return context.Set<T>();
        }
        public T? Get(int id)
        {
            return context.Find<T>(id);
        }
        public bool Insert(T entity)
        {
            context.Add(entity);
            int cambios = context.SaveChanges();
            //si hay algun cambio en la base de datos regresara true
            return cambios > 0;
        }
        public bool Update(T entity)
        {
            context.Update(entity);
            int cambios = context.SaveChanges();
            return cambios > 0;
        }
        public bool Delete(T entity)
        {
            context.Remove(entity);
            int cambios = context.SaveChanges();
            return cambios > 0;
        }
    }
}
