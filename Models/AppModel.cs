using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3M_Firewall.Models
{
    public class AppModel
    {
        /// <summary>
        /// application name to be selected
        /// </summary>
        public string? AppPathName { get; set; }

        /// <summary>
        /// id corresponding to this rule
        /// </summary>
        public int ruleID { get; set; }
    }
}
