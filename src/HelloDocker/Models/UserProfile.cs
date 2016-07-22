using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerASPNETCore.Models
{
    public class UserProfile
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
