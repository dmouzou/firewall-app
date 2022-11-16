using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _3M_Firewall.Helpers;
using _3M_Firewall.Models;
using _3M_Firewall.FirewallConfig;

namespace _3M_Firewall
{
    /// <summary>
    /// Interaction logic for RuleList.xaml
    /// </summary>
    public partial class RuleList : Window
    {
        Helper helper = new Helper();
        MessageHelper messageHelper = new MessageHelper();

        public RuleList()
        {
            InitializeComponent();

            LoadListBox();
        }

        private void LoadListBox()
        {
            ruleListBox.ItemsSource = null;
            List<RuleModel> rules = helper.GetAllRules();
            ruleListBox.ItemsSource = rules;
            ruleListBox.DisplayMemberPath = "RuleName";
        }

        private void sortByNameCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            List<RuleModel> rules = helper.GetAllRulesSorted();
            ruleListBox.ItemsSource = rules;
            ruleListBox.DisplayMemberPath = "RuleName";
        }

        private void sortByNameCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LoadListBox();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Hyperlink_Click_DeleteAllRules(object sender, RoutedEventArgs e)
        {
            if (messageHelper.deleteAllRulesOrNot() == MessageBoxResult.Yes)
            {
                List<RuleModel> rules = helper.GetAllRules();

                AllRulesControl allRulesControl = new AllRulesControl();
                allRulesControl.DeleteAllRules(rules);

                helper.DeleteAllRules();

                LoadListBox();
            }
        }

        private void ruleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ruleListBox.SelectionChanged -= ruleListBox_SelectionChanged;   

            RuleModel rule = (RuleModel)ruleListBox.SelectedItem;

            typeTextBlock.Text = "Type: " + rule.RuleType;
            statusTextBlock.Text = rule.RuleStatus;

            if (rule.RuleStatus == "Enabled")
            {
                statusTextBlock.Foreground = Brushes.Green;
            }
            else
            {
                statusTextBlock.Foreground = Brushes.Red;
            }
        }
    }
}
