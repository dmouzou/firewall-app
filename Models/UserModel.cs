using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3M_Firewall.Models
{
    internal class UserModel
    {
        public int SessionID { get; set; }

        public string? Username { get; set; }    

        public string? Password { get; set; }

        public string? SecurityKey { get; set; }
    }
}
