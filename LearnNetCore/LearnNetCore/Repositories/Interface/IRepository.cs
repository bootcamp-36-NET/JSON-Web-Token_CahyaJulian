using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Repositories.Interface
{
    public interface IRepository<T> where T:class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Create(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int Id); 
    }
}
