using h2dYatırım.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace h2dYatırım.DataAccess
{
    public class ShareCertificateDal
    {
        public List<ShareCertificate> GetAll(Expression<Func<ShareCertificate, bool>> filter = null)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                return filter == null
                    ? context.Set<ShareCertificate>().ToList()
                    : context.Set<ShareCertificate>().Where(filter).ToList();
            }
        }
        public void Add(ShareCertificate entity)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public ShareCertificate Get(Expression<Func<ShareCertificate, bool>> filter)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                return context.Set<ShareCertificate>().SingleOrDefault(filter);
            }
        }

        public void Delete(ShareCertificate entity)
        {
            using (h2dYatirimDBContext context = new h2dYatirimDBContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(ShareCertificate entity)
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
