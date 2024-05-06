using Core.DataAccess;
using h2dYatırım.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace h2dYatirim.Infrastructure.Entities
{
    public interface IUserDal: IEntityRepository<User>
    {
    }


}
