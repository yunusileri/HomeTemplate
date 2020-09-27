using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public static class SeedData
    {
        public static void BuildDataBase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<MyDbContext>();
                _context.Database.Migrate();

                if (!_context.Role.Any())
                {
                    _context.Role.Add(new Role() { Name = "Admin" });
                    _context.Role.Add(new Role() { Name = "Custom" });
                    _context.User.Add(new Entity.User() { Name = "yunusi", Email = "yunusi@sys.com.tr", PasswordHash = "1" });
                    _context.UserRole.Add(new UserRole() { UserId = 1, RoleId = 2 });
                    _context.SaveChanges();
                }

            }
        }
    }
}
