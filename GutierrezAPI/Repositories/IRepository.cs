using Microsoft.EntityFrameworkCore;

namespace GutierrezAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        bool Delete(T entity);
        T? Get(int id);
        DbSet<T> GetAll();
        bool Insert(T entity);
        bool Update(T entity);
    }
}