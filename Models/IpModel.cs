using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3M_Firewall.Models
{
    public class IpModel
    {
        /// <summary>
        /// name of ip address to block
        /// </summary>
        public string? IpAddrName { get; set; }

        /// <summary>
        /// id corresponding to this rule
        /// </summary>
        public int ruleID { get; set; }
    }
}
