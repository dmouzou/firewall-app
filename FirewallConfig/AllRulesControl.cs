using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using NetFwTypeLib;
using _3M_Firewall.Models;


namespace _3M_Firewall.FirewallConfig
{
    public class AllRulesControl
    {
        const string guidFWPolicy2 = "{E2B3C97F-6AE1-41AC-817A-F6F92166D7DD}";
        const string guidRWRule = "{2C5BC43E-3369-4C33-AB0C-BE9469677AF4}";


        public void BlockAllTraffic(RuleModel rule)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            Type typeFWRule = Type.GetTypeFromCLSID(new Guid(guidRWRule));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            INetFwRule newRule = (INetFwRule)Activator.CreateInstance(typeFWRule);


            newRule.Name = rule.RuleName.ToString();

            newRule.Description = string.Concat("The Firewall is blocking all inbound traffic");

            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;

            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;

            newRule.Enabled = fwPolicy2.BlockAllInboundTraffic[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN];
            newRule.Enabled = fwPolicy2.BlockAllInboundTraffic[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC];
            newRule.Enabled = fwPolicy2.BlockAllInboundTraffic[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE];

            newRule.Enabled = true;

            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            fwPolicy2.Rules.Add(newRule);

        }

        public void DeleteBlockTrafficRule(RuleModel rule)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            fwPolicy2.Rules.Remove(rule.RuleName);

        }

        public void EnableBlockAllTrafficRule(RuleModel rule)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            if (fwPolicy2.Rules.Item(rule.RuleName).Enabled == false)
            {
                fwPolicy2.Rules.Item(rule.RuleName).Enabled = true;
            }
        }

        

        public void DeleteAllRules(List<RuleModel> rules)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            foreach (RuleModel rule in rules)
            {
                fwPolicy2.Rules.Remove(rule.RuleName);
            }
        }

        public void ChangeRuleName(RuleModel ruleModel, string oldName)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            fwPolicy2.Rules.Item(oldName).Name = ruleModel.RuleName;
            
        }

        public void Delete(RuleModel ruleModel)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            fwPolicy2.Rules.Remove(ruleModel.RuleName);

        }

        public void Enable(RuleModel ruleModel)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            if (fwPolicy2.Rules.Item(ruleModel.RuleName).Enabled == false)
            {
                fwPolicy2.Rules.Item(ruleModel.RuleName).Enabled = true;
            }
        }

        public void Disable(RuleModel ruleModel)
        {
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));

            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            if (fwPolicy2.Rules.Item(ruleModel.RuleName).Enabled == true)
            {
                fwPolicy2.Rules.Item(ruleModel.RuleName).Enabled = false;
            }
        }


    }
}
