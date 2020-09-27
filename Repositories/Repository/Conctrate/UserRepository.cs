using Entity;
using Microsoft.EntityFrameworkCore;
using Sys.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository.Conctrate
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(MyDbContext ctx) : base(ctx) { }


        public async Task DeleteAllUser() => await _context.Database.ExecuteSqlRawAsync("delete from User");
    }
}
