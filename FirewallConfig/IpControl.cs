using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;
using _3M_Firewall.Models;
using _3M_Firewall.Helpers;

namespace _3M_Firewall.FirewallConfig
{
    public class IpControl
    {
        const string guidFWPolicy2 = "{E2B3C97F-6AE1-41AC-817A-F6F92166D7DD}";
        const string guidRWRule = "{2C5BC43E-3369-4C33-AB0C-BE9469677AF4}";


        public void Setup(RuleModel ruleModel, IpModel ipModel)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            Type typeFWRule = Type.GetTypeFromCLSID(new Guid(guidRWRule));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            INetFwRule newRule = (INetFwRule)Activator.CreateInstance(typeFWRule);


            newRule.Name = ruleModel.RuleName.ToString();

            string text = ipModel.IpAddrName;

            newRule.Description = string.Concat("The Firewall is blocking the website : ", text);

            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;

            string[] list_ip = new string[1];
            list_ip[0] = ipModel.IpAddrName;

            string[] IpList = NSLookup.NSLookupAttr(list_ip);
            string a = IpList[0];
            for (int i = 1; i < IpList.Length; i++)
            {
                a = a + "," + IpList[i];
            }

            newRule.RemoteAddresses = a;

            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;

            newRule.Enabled = true;

            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            fwPolicy2.Rules.Add(newRule);

        }
                       
    }
}
