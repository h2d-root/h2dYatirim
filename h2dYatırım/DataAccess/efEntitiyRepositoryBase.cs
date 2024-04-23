using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace h2dYatırım.DataAccess
{



    public class efEntitiyRepositoryBase<TEntitiy>
       where TEntitiy : class, new()
    {
        public void Add(TEntitiy entity)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntitiy entity)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntitiy Get(Expression<Func<TEntitiy, bool>> filter)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                return context.Set<TEntitiy>().SingleOrDefault(filter);
            }
        }

        public List<TEntitiy> GetAll(Expression<Func<TEntitiy, bool>> filter = null)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                return filter == null
                    ? context.Set<TEntitiy>().ToList()
                    : context.Set<TEntitiy>().Where(filter).ToList();
            }
        }

        public void Update(TEntitiy entity)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
