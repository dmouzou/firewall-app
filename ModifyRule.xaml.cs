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
using _3M_Firewall.Models;
using _3M_Firewall.Helpers;
using _3M_Firewall.FirewallConfig;

namespace _3M_Firewall
{
    /// <summary>
    /// Interaction logic for ModifyRule.xaml
    /// </summary>
    public partial class ModifyRule : Window
    {
        Helper helper = new Helper();
        MessageHelper messageHelper = new MessageHelper();

        string[] lines = System.IO.File.ReadAllLines("RuleFile.txt");

        private List<PortModel> ports = new List<PortModel>();

        public ModifyRule()
        {
            InitializeComponent();

            PortsList();

            LoadWindowContent();
                        
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        private void LoadWindowContent()
        {
            string ruleSelectedName = lines[1];
            string ruleSelectedType = lines[2];

            newRuleNameTextBox.Text = ruleSelectedName;

            if (ruleSelectedType == "IP Blocking")
            {
                newWebAddressTextBlock.Visibility = Visibility.Visible;
                newWebAddressTextBox.Visibility = Visibility.Visible;
            }

            else if (ruleSelectedType == "Port Blocking")
            {
                newPortTextBlock.Visibility = Visibility.Visible;
                newPortComboBox.Visibility = Visibility.Visible;
                DropDownList();
            }

            else if (ruleSelectedType == "Application Blocking")
            {
                newAppTextBlock.Visibility = Visibility.Visible;
                newAppPathTextBox.Visibility = Visibility.Visible;
                newBrowseButton.Visibility = Visibility.Visible;
            }
        }

        private void PortsList()
        {
            ports.Add(new PortModel { PortName = "HTTP -- 80", PortNumber = 80 });
            ports.Add(new PortModel { PortName = "DNS -- 53", PortNumber = 53 });

        }

        private void DropDownList()
        {
            newPortComboBox.ItemsSource = null;
            newPortComboBox.ItemsSource = ports;
            newPortComboBox.DisplayMemberPath = "PortName";

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
                newAppPathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void newBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Open_File();
        }



        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int ruleSelectedID = int.Parse(lines[0]);
            string oldRuleName = lines[1];

            RuleModel rule = new RuleModel();

            if (!string.IsNullOrWhiteSpace(newRuleNameTextBox.Text))
            {
                rule.RuleID = ruleSelectedID;
                rule.RuleName = newRuleNameTextBox.Text;

                if (newWebAddressTextBlock.Visibility == Visibility.Visible)
                {
                    IpModel ipModel = new IpModel();
                    ipModel.IpAddrName = newWebAddressTextBox.Text;
                    ipModel.ruleID = ruleSelectedID;

                    string[] list_ip = new string[1];
                    list_ip[0] = ipModel.IpAddrName;

                    if (string.IsNullOrWhiteSpace(newWebAddressTextBox.Text))
                    {
                        messageHelper.noIPAddressEntered();
                        newWebAddressTextBox.Text = "";
                    }
                    else if (NSLookup.NSLookupAttr(list_ip) == null)
                    {
                        messageHelper.invalidIPAddress();
                        newWebAddressTextBox.Text = "";
                    }
                    else
                    {
                        helper.ChangeRuleName(rule);
                        helper.ChangeIpAddress(ipModel);

                        AllRulesControl allRulesControl = new AllRulesControl();
                        allRulesControl.ChangeRuleName(rule, oldRuleName);

                        messageHelper.ruleSavedSuccessfully();

                        activateRuleOrNot();

                        this.Close();
                    }

                }

                else if (newPortTextBlock.Visibility == Visibility.Visible)
                {
                    PortModel portModel = new PortModel();
                    if (newPortComboBox.SelectedIndex == 0)
                    {
                        portModel.PortName = "HTTP";
                        portModel.PortNumber = 80;
                    }
                    else if (newPortComboBox.SelectedIndex == 1)
                    {
                        portModel.PortName = "DNS";
                        portModel.PortNumber = 53;
                    }

                    portModel.ruleID = ruleSelectedID;
                    helper.ChangeRuleName(rule);
                    helper.ChangePort(portModel);

                    AllRulesControl allRulesControl = new AllRulesControl();
                    allRulesControl.ChangeRuleName(rule, oldRuleName);

                    messageHelper.ruleSavedSuccessfully();
                    activateRuleOrNot();
                    this.Close();
                }

                else if (newAppTextBlock.Visibility == Visibility.Visible)
                {
                    if (string.IsNullOrWhiteSpace(newAppPathTextBox.Text))
                    {
                        messageHelper.appPathNotEntered();
                        newAppPathTextBox.Text = "";
                    }

                    else
                    {
                        AppModel appModel = new AppModel();
                        appModel.AppPathName = newAppPathTextBox.Text;
                        appModel.ruleID = ruleSelectedID;

                        helper.ChangeRuleName(rule);
                        helper.ChangeAppPath(appModel);

                        AllRulesControl allRulesControl = new AllRulesControl();
                        allRulesControl.ChangeRuleName(rule, oldRuleName);

                        messageHelper.ruleSavedSuccessfully();
                        activateRuleOrNot();               
                        this.Close();


                    }
                }
                                
            }
            else
            {
                messageHelper.nameBoxEmpty();
            }


        }


        private void activateRuleOrNot()
        {
            RuleModel rule = new RuleModel();
            string ruleSelectedStatus = lines[3];
            rule.RuleStatus = ruleSelectedStatus;

            if (rule.RuleStatus == "Disabled")
            {
                if (messageHelper.activateRuleOrNot() == MessageBoxResult.Yes)
                {
                    rule.RuleStatus = "Enabled";
                    helper.ChangeRuleStatus(rule);
                }

            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
