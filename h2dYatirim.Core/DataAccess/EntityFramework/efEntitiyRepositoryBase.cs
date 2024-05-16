using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.DataAccess.EntityFramework
{
    public class efEntitiyRepositoryBase<TEntitiy, TContext> :IEntityRepository<TEntitiy>
        where TEntitiy :class,IEntity,new()
        where TContext :DbContext,new()
    {
        public void Add(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntitiy Get(Expression<Func<TEntitiy, bool>> filter, string? includes = null)
        {
            using (TContext context = new TContext())
            {
                var query = context.Set<TEntitiy>().AsQueryable();

                
                if (!string.IsNullOrEmpty(includes))
                {
                    var includeList = includes.Split(',').Select(include => include.Trim());
                    foreach (var include in includeList)
                    {
                        query = query.Include(include);
                    }
                }

                return query.SingleOrDefault(filter);
            }
        }

        public List<TEntitiy> GetAll(Expression<Func<TEntitiy, bool>> filter = null, string? includes = null)
        {
            using (TContext context = new TContext())
            {
                var query = filter == null
                ? context.Set<TEntitiy>().AsQueryable()
                : context.Set<TEntitiy>().Where(filter);

                if (!string.IsNullOrEmpty(includes))
                {
                    var includeList = includes.Split(',').Select(include => include.Trim());
                    foreach (var include in includeList)
                    {
                        query = query.Include(include);
                    }
                }

                return query.ToList();
            }
        }

        public void Update(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
