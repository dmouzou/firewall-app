using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3M_Firewall.Models
{
    public class PortModel
    {

        /// <summary>
        /// port name like http, dns
        /// </summary>
        public string? PortName { get; set; }

        /// <summary>
        /// port number like 80, 443
        /// </summary>
        public ushort PortNumber { get; set; }

        /// <summary>
        /// id corresponding to this rule
        /// </summary>
        public int ruleID { get; set; }

    }
}
