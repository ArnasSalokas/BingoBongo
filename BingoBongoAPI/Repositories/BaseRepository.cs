using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using BingoBongoAPI.Repositories.Contracts;
using BingoBongoAPI.Entities;

namespace BingoBongoAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Protected Properties

        protected readonly BingoBongoContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        #endregion

        #region Constructors

        public BaseRepository(BingoBongoContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #endregion

        #region Get

        public virtual async Task<T> GetByKey(object key)
        {
            var entity = await _dbSet.FindAsync(key);

            return entity;
        }

        #endregion

        #region Add

        public virtual async Task Add(T entity)
        {
            if (entity == null) return;

            try
            {
                _dbSet.Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Update

        public virtual async Task Update(T entity)
        {
            if (entity == null) return;

            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Delete

        public virtual async Task Delete(object key)
        {
            var entity = await _dbSet.FindAsync(key);

            if (entity == null) return;

            try
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}