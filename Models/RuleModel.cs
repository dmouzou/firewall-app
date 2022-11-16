using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3M_Firewall.Models
{

    public class RuleModel
    {

        public int RuleID { get; set; }
        /// <summary>
        /// represents the ip addresses list we need to block
        /// </summary>

        /// <summary>
        /// name of the rule
        /// </summary>
        public string? RuleName { get; set; }

        /// <summary>
        /// status of the rule (enabled or disabled) -- a rule could overwrite another rule
        /// </summary>
        public string? RuleStatus { get; set; }

        /// <summary>
        /// rule type (ip, port, app)
        /// </summary>
        public string? RuleType { get; set; }

        public int SessionID { get; set; }


    }
}
