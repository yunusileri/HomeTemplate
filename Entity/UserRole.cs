using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys
{
    public class UserRole
    {
        public long Id{ get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
