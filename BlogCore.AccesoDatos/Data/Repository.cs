using BlogCore.AccesoDatos.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogCore.AccesoDatos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext dbContext;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            dbContext = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }
       

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties separadas por coma
            if (includeProperties != null)
            {
                foreach (var includePropertie in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePropertie);
                }
            }

            if (orderBy !=null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOnDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties separadas por coma
            if (includeProperties != null)
            {
                foreach (var includePropertie in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePropertie);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityRemove = dbSet.Find(id);
            Remove(entityRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
