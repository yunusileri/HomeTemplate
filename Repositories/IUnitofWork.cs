using Sys.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys
{
    public interface IUnitofWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
  
    }
}
