using Entity;
using Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IQueryable<User> GetAllUser();
        Task<User> GetUser(long Id);

        User GenerateToken(User user); 
         Task<User> Authenticate(string username, string password);
    }


}
