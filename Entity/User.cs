using Newtonsoft.Json;
using Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class User
    {
        public long Id { get; set; }
        public DateTime InsertDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public virtual List<UserRole> Roles { get; set; }
        [NotMapped]
        public virtual string Token { get; set; }
    }
}
