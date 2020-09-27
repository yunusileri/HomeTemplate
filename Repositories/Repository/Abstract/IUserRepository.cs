using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task DeleteAllUser();
    }
}
