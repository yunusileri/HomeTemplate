using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys
{
    public class MyDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=StokTakip.db"); 
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<UserRole> UserRole{ get; set; }
      
    }
}
