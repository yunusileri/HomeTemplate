using Entity;
using Microsoft.EntityFrameworkCore;
using Sys.Repository.Abstract;
using Sys.Repository.Conctrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys
{
    public class UnitofWork : IUnitofWork
    {
        private readonly MyDbContext _dbContext;
        public UnitofWork(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository => new UserRepository(_dbContext);
        public IRepository<Role> RoleRepository => new Repository<Role>(_dbContext);
        public IRepository<UserRole> UserRoleRepository => new Repository<UserRole>(_dbContext);
   

        public void Dispose()
        {
            _dbContext.SaveChanges();
        }

      
    }
}
