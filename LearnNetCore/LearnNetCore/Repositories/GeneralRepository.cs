using LearnNetCore.Base;
using LearnNetCore.Context;
using LearnNetCore.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Repositories
{
    public class GeneralRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, BaseModel
        where TContext : MyContext
    {
        private MyContext _myContext;
        public GeneralRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public async Task<int> Create(TEntity entity)
        {
            entity.CreateDate = DateTimeOffset.Now;
            entity.isDelete = false;
            await _myContext.Set<TEntity>().AddAsync(entity);
            return await _myContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var item = await GetById(Id);
            if(item == null)
            {
                return 0;
            }
            item.DeleteDate = DateTimeOffset.Now;
            item.isDelete = true;
            _myContext.Entry(item).State = EntityState.Modified;
            return await _myContext.SaveChangesAsync();

        }

        public async Task<List<TEntity>> GetAll()
        {
            var getAll = await _myContext.Set<TEntity>().ToListAsync();
            if (!getAll.Count().Equals(0))
            {
                return getAll;
            }
            return null;

        }

        public async Task<TEntity> GetById(int id)
        {
            var getId = await _myContext.Set<TEntity>().FindAsync(id);
            if (getId != null)
            {
                return getId;
            }
            return null;
        }

        public async Task<int> Update(TEntity entity)
        {
            _myContext.Entry(entity).State = EntityState.Modified;
            return await _myContext.SaveChangesAsync();
        }
    }
}
