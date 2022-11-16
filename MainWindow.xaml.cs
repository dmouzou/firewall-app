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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _3M_Firewall.Helpers;
using _3M_Firewall.Models;
using _3M_Firewall.FirewallConfig;
using System.ComponentModel;

namespace _3M_Firewall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Helper helper = new Helper();
        MessageHelper messageHelper = new MessageHelper();

        string[] lines = System.IO.File.ReadAllLines("UserFile.txt");

        private List<RuleModel> types = new List<RuleModel>();
        private List<PortModel> ports = new List<PortModel>();

        public MainWindow()
        {
            InitializeComponent();

            TypeOfRules();

            PortsList();

            DropDownList();

            LoadListBox();

            showNullValues();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            deleteBlockAllTrafficRule();
        }


        public void LoadListBox()
        {
            ruleListView.ItemsSource = null;
            UserModel user = new UserModel();
            user.SessionID = int.Parse(lines[0]);
            List<RuleModel> rules = helper.GetSessionRules(user);
            ruleListView.ItemsSource = rules;
            ruleListView.DisplayMemberPath = "RuleName";
        }


        private void TypeOfRules()
        {
            types.Add(new RuleModel { RuleType = "IP Blocking" });
            types.Add(new RuleModel { RuleType = "Port Blocking" });
            types.Add(new RuleModel { RuleType = "Application Blocking" });

        }


        public void PortsList()
        {
            ports.Add(new PortModel { PortName = "HTTP -- 80", PortNumber = 80 });
            ports.Add(new PortModel { PortName = "DNS -- 53", PortNumber = 53 });

        }


        private void DropDownList()
        {
            typeComboBox.ItemsSource = null;

            typeComboBox.ItemsSource = types;
            typeComboBox.DisplayMemberPath = "RuleType";

            portComboBox.ItemsSource = null;

            portComboBox.ItemsSource = ports;
            portComboBox.DisplayMemberPath = "PortName";

        }


        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            RuleModel rule = new RuleModel();

            rule.SessionID = int.Parse(lines[0]);

            List<RuleModel> allrules = helper.GetAllRules();

            rule.RuleName = ruleNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(ruleNameTextBox.Text))
            {
                int lastRuleID = helper.GetLastEntry() + 1;
                rule.RuleName = string.Concat("Rule ", lastRuleID.ToString());
            }
            else
            {
                foreach (RuleModel r in allrules)
                {
                    if (ruleNameTextBox.Text.Equals(r.RuleName))
                    {
                        rule.RuleName = string.Concat(ruleNameTextBox.Text, " (1)");
                    }
                }
            }

            rule.RuleType = typeComboBox.Text;

            rule.RuleStatus = "Enabled";

            if (typeComboBox.SelectedIndex == 0)
            {
                rule.RuleType = "IP Blocking";

                IpModel ip = new IpModel();
                ip.IpAddrName = webAddressTextBox.Text;

                string[] list_ip = new string[1];
                list_ip[0] = ip.IpAddrName;

                if (string.IsNullOrWhiteSpace(webAddressTextBox.Text))
                {
                    messageHelper.noIPAddressEntered();
                }
                else if (NSLookup.NSLookupAttr(list_ip) == null)
                {
                    messageHelper.invalidIPAddress();

                    webAddressTextBox.Text = "";
                }
                else
                {
                    helper.CreateIpRule(rule, ip);

                    IpControl ipControl = new IpControl();
                    ipControl.Setup(rule, ip);

                    LoadListBox();

                    messageHelper.ruleCreatedSuccessfully();

                    ruleNameTextBox.Text = "";
                    webAddressTextBox.Text = "";
                    appPathTextBox.Text = "";
                }

            }

            else if (typeComboBox.SelectedIndex == 1)
            {
                PortModel portModel = new PortModel();
                if (portComboBox.SelectedIndex == 0)
                {
                    portModel.PortName = "HTTP";
                    portModel.PortNumber = 80;
                }
                else if (portComboBox.SelectedIndex == 1)
                {
                    portModel.PortName = "DNS";
                    portModel.PortNumber = 53;
                }

                helper.CreatePortRule(rule, portModel);

                PortControl portControl = new PortControl();
                portControl.Setup(rule, portModel);

                LoadListBox();

                messageHelper.ruleCreatedSuccessfully();

                ruleNameTextBox.Text = "";
                webAddressTextBox.Text = "";
                appPathTextBox.Text = "";

            }

            else if (typeComboBox.SelectedIndex == 2)
            {
                if (string.IsNullOrWhiteSpace(appPathTextBox.Text))
                {
                    messageHelper.appPathNotEntered();
                }

                else
                {
                    AppModel appModel = new AppModel();
                    appModel.AppPathName = appPathTextBox.Text;

                    helper.CreateAppRule(rule, appModel);

                    AppControl appControl = new AppControl();
                    appControl.Setup(rule, appModel);

                    LoadListBox();

                    messageHelper.ruleCreatedSuccessfully();

                    ruleNameTextBox.Text = "";
                    webAddressTextBox.Text = "";
                    appPathTextBox.Text = "";
                }
            }
        }


        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeComboBox.SelectedIndex == 0)
            {
                webAddressTextBlock.IsEnabled = true;
                webAddressTextBox.IsEnabled = true;
                portComboBox.IsEnabled = false;
                portTextBlock.IsEnabled = false;
                portTextBlock.Opacity = 50;
                appTextBlock.IsEnabled = false;
                appTextBlock.Opacity = 50;
                appPathTextBox.IsEnabled = false;
                browseButton.IsEnabled = false;

            }

            else if (typeComboBox.SelectedIndex == 1)
            {
                webAddressTextBlock.IsEnabled = false;
                webAddressTextBlock.Opacity = 50;
                webAddressTextBox.IsEnabled = false;
                portComboBox.IsEnabled = true;
                portTextBlock.IsEnabled = true;
                appTextBlock.IsEnabled = false;
                appTextBlock.Opacity = 50;
                appPathTextBox.IsEnabled = false;
                browseButton.IsEnabled = false;
            }

            else if (typeComboBox.SelectedIndex == 2)
            {
                webAddressTextBlock.IsEnabled = false;
                webAddressTextBlock.Opacity = 50;
                webAddressTextBox.IsEnabled = false;
                portComboBox.IsEnabled = false;
                portTextBlock.IsEnabled = false;
                portTextBlock.Opacity = 50;
                appTextBlock.IsEnabled = true;
                appPathTextBox.IsEnabled = true;
                browseButton.IsEnabled = true;
            }
        }


        private void Open_File()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.DefaultExt = "exe";
            openFileDialog.DereferenceLinks = true;
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Exe Files (*.exe)|*.exe|All files|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Select the program";
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == true)
            {
                appPathTextBox.Text = openFileDialog.FileName;
            }
        }


        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            Open_File();
        }


        private void blockAllTrafficCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            List<RuleModel> rules = helper.GetAllRules();

            deleteBlockAllTrafficRule();

            if (messageHelper.blockAllTraffic() == MessageBoxResult.Yes)
            {
                RuleModel rule = new RuleModel();
                rule.RuleName = "All Traffic Blocked";
                rule.RuleStatus = "Enabled";
                rule.RuleType = "Block All Traffic";
                rule.SessionID = int.Parse(lines[0]);

                viewRuleBorder.Visibility = Visibility.Hidden;
                ruleListGroupBox.Visibility = Visibility.Hidden;
                ruleInfoGroupBox.Visibility = Visibility.Hidden;
                modifyRuleButon.Visibility = Visibility.Hidden;
                deleteRuleButton.Visibility = Visibility.Hidden;
                disableRuleButton.Visibility = Visibility.Hidden;
                deleteAllRulesButton.Visibility = Visibility.Hidden;
                refreshListButton.Visibility = Visibility.Hidden;

                trafficBlockedTextBlock.Visibility = Visibility.Visible;

                AllRulesControl allRulesControl = new AllRulesControl();
                allRulesControl.BlockAllTraffic(rule);

                helper.CreateBlockAllTrafficRule(rule);

                ruleNameTextBox.IsEnabled = false;
                ruleNameTextBox.Opacity = 50;
                typeComboBox.IsEnabled = false;
                typeComboBox.Opacity = 50;
                webAddressTextBox.IsEnabled = false;
                webAddressTextBox.Opacity = 50;
                addRuleButton.IsEnabled = false;
                addRuleButton.Opacity = 50;
                blockAllTrafficExceptButton.IsEnabled = false;
                blockAllTrafficExceptButton.Opacity = 50;

            }

            else
            {
                blockAllTrafficCheckBox.IsChecked = false;
            }
        }
    

        private void blockAllTrafficCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            List<RuleModel> rules = helper.GetAllRules();

            foreach (RuleModel rule in rules)
            {
                if (rule.RuleName == "All Traffic Blocked")
                {
                    trafficBlockedTextBlock.Visibility = Visibility.Hidden;

                    viewRuleBorder.Visibility = Visibility.Visible;
                    ruleListGroupBox.Visibility = Visibility.Visible;
                    ruleInfoGroupBox.Visibility = Visibility.Visible;
                    modifyRuleButon.Visibility = Visibility.Visible;
                    deleteRuleButton.Visibility = Visibility.Visible;
                    disableRuleButton.Visibility = Visibility.Visible;
                    deleteAllRulesButton.Visibility = Visibility.Visible;
                    refreshListButton.Visibility = Visibility.Visible;

                    AllRulesControl allRulesControl = new AllRulesControl();
                    allRulesControl.DeleteBlockTrafficRule(rule);

                    helper.DeleteBlockAllTrafficRule(rule);

                    ruleNameTextBox.IsEnabled = true;
                    ruleNameTextBox.Opacity = 100;
                    typeComboBox.IsEnabled = true;
                    typeComboBox.Opacity = 100;
                    webAddressTextBox.IsEnabled = true;
                    webAddressTextBox.Opacity = 100;
                    addRuleButton.IsEnabled = true;
                    addRuleButton.Opacity = 100;
                    blockAllTrafficExceptButton.IsEnabled = true;
                    blockAllTrafficExceptButton.Opacity = 100;

                }
            }
        }


        public void showNullValues()
        {
            if (ruleListView.SelectedItems.Count == 0)
            {
                typeValueTextBlock.Text = "<empty>";
                contentValueTextBlock.Text = "<empty>";
                statusValueTextBlock.Text = "<empty>";
                statusValueTextBlock.Foreground = Brushes.Black;
            }
        }


        public void showRuleContent(RuleModel ruleModel)
        {
            if (ruleModel.RuleType == "IP Blocking")
            {
                var content = helper.GetIpRule_One(ruleModel);
                if (content != null)
                {
                    contentValueTextBlock.Text = string.Concat("The Firewall is blocking the website : ", content.ToString());
                }
            }

            else if (ruleModel.RuleType == "Port Blocking")
            {
                var content = helper.GetPortRule_One(ruleModel);
                if (content != null)
                {
                    contentValueTextBlock.Text = string.Concat("The Firewall is blocking the ", content.ToString(), " port.");
                }
            }

            else if (ruleModel.RuleType == "Application Blocking")
            {
                var content = helper.GetAppRule_One(ruleModel);
                if (content != null)
                {
                    contentValueTextBlock.Text = string.Concat("The Firewall is blocking the application with path : ", content.ToString());
                }
            }

            else if (ruleModel.RuleType == "Block All Traffic")
            {
                contentValueTextBlock.Text = "The Firewall is blocking all traffic. You don't have access to the internet.";
            }
        }


        private void ruleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RuleModel ruleModel = (RuleModel)ruleListView.SelectedItem;

            if (ruleModel != null)
            {
                showRuleContent(ruleModel);
                typeValueTextBlock.Text = ruleModel.RuleType;
                statusValueTextBlock.Text = ruleModel.RuleStatus;

                if (ruleModel.RuleStatus == "Enabled")
                {
                    disableRuleButton.Content = "Disable rule";
                    statusValueTextBlock.Foreground = Brushes.Green;
                }
                else
                {
                    disableRuleButton.Content = "Enable rule";
                    statusValueTextBlock.Foreground = Brushes.Red;
                }

            }

            showNullValues();
        }


        private void deleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            RuleModel ruleModel = (RuleModel)ruleListView.SelectedItem;
            RuleModel rule = new RuleModel();
            AllRulesControl allRulesControl = new AllRulesControl();

            if (ruleModel != null)
            {
                if (messageHelper.deleteRuleOrNot() == MessageBoxResult.Yes)
                {
                    List<RuleModel> rules = helper.GetAllRules();
                    rules.Remove(ruleModel);

                    rule.RuleName = ruleModel.RuleName;

                    if (ruleModel.RuleType == "IP Blocking")
                    {
                        allRulesControl.Delete(rule);
                        helper.DeleteIpRule(rule);
                    }
                    else if (ruleModel.RuleType == "Port Blocking")
                    {
                        allRulesControl.Delete(rule);
                        helper.DeletePortRule(rule);
                    }
                    else if (ruleModel.RuleType == "Application Blocking")
                    {
                        allRulesControl.Delete(rule);
                        helper.DeleteAppRule(rule);
                    }

                    else if (ruleModel.RuleType == "Block All Traffic")
                    {
                        allRulesControl.DeleteBlockTrafficRule(rule);
                        helper.DeleteBlockAllTrafficRule(rule);
                    }

                    LoadListBox();
                }
            }

            else
            {
                messageHelper.noRuleSelected();
            }


        }


        private void disableRuleButton_Click(object sender, RoutedEventArgs e)
        {
            RuleModel rule = (RuleModel)ruleListView.SelectedItem;

            AllRulesControl allRulesControl = new AllRulesControl();

            if (rule != null)
            {

                if (disableRuleButton.Content.ToString() == "Disable rule")
                {
                    disableRuleButton.Content = "Enable rule";
                    rule.RuleStatus = "Disabled";
                    statusValueTextBlock.Text = rule.RuleStatus.ToString();
                    statusValueTextBlock.Foreground = Brushes.Red;

                    if (rule.RuleType == "IP Blocking")
                    {
                        allRulesControl.Disable(rule);
                    }
                    else if (rule.RuleType == "Port Blocking")
                    {
                        allRulesControl.Disable(rule);
                    }
                    else if (rule.RuleType == "Application Blocking")
                    {
                        allRulesControl.Disable(rule);
                    }

                    helper.ChangeRuleStatus(rule);
                }

                else if (disableRuleButton.Content.ToString() == "Enable rule")
                {
                    disableRuleButton.Content = "Disable rule";
                    rule.RuleStatus = "Enabled";
                    statusValueTextBlock.Text = rule.RuleStatus.ToString();
                    statusValueTextBlock.Foreground = Brushes.Green;

                    if (rule.RuleType == "IP Blocking")
                    {
                        allRulesControl.Enable(rule);
                    }
                    else if (rule.RuleType == "Port Blocking")
                    {
                        allRulesControl.Enable(rule);
                    }
                    else if (rule.RuleType == "Application Blocking")
                    {
                        allRulesControl.Enable(rule);
                    }
                    else if (rule.RuleType == "Block All Traffic")
                    {
                        allRulesControl.EnableBlockAllTrafficRule(rule);
                    }

                    helper.ChangeRuleStatus(rule);
                }

            }
            else
            {
                messageHelper.noRuleSelected();
            }


        }



        private void deleteAllRulesButton_Click(object sender, RoutedEventArgs e)
        {
            UserModel user = new UserModel();
            user.SessionID = int.Parse(lines[0]);

            List<RuleModel> rules = helper.GetSessionRules(user);

            if (ruleListView.ItemsSource != null)
            {
                if (messageHelper.deleteAllRulesOrNot() == MessageBoxResult.Yes)
                {
                    AllRulesControl allRulesControl = new AllRulesControl();
                    allRulesControl.DeleteAllRules(rules);

                    helper.DeleteAllSessionRules(user);
                    ruleListView.ItemsSource = null;
                }
            }

            else
            {
                messageHelper.noRuleSet();
            }
        }


        private void modifyRuleButon_Click(object sender, RoutedEventArgs e)
        {
            RuleModel rule = (RuleModel)ruleListView.SelectedItem;

            if (rule != null)
            {
                helper.StoreRuleInfo(rule);
                ModifyRule modifyRule = new ModifyRule();
                modifyRule.ShowDialog();
            }

            else
            {
                messageHelper.noRuleSelected();
            }


        }

               
        private void Hyperlink_Click_RefreshList(object sender, RoutedEventArgs e)
        {
            LoadListBox();
        }

        private void leaveSessionButton_Click(object sender, RoutedEventArgs e)
        {
            deleteBlockAllTrafficRule();

            LogIn logIn = new LogIn();
            logIn.Show();

            this.Close();
        }


        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            if (messageHelper.quitOrNot() == MessageBoxResult.Yes)
            {
                deleteBlockAllTrafficRule();

                this.Close();
            }
        }

        private void deleteBlockAllTrafficRule()
        {
            List<RuleModel> rules = helper.GetAllRules();

            foreach (RuleModel r in rules)
            {
                if (r.RuleName == "All Traffic Blocked")
                {
                    helper.DeleteBlockAllTrafficRule(r);

                    AllRulesControl allRulesControl = new AllRulesControl();
                    allRulesControl.DeleteBlockTrafficRule(r);
                }

            }
        }
    }
}


