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

namespace _3M_Firewall
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        MessageHelper messageHelper = new MessageHelper();
        Helper helper = new Helper();

        public LogIn()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click_ForgotPassword(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.ShowDialog();
        }

        private void Hyperlink_Click_CreateNewSession(object sender, RoutedEventArgs e)
        {
            NewSession newSession = new NewSession();
            newSession.ShowDialog();
        }

        private void Hyperlink_Click_ViewAllRules(object sender, RoutedEventArgs e)
        {
            RuleList ruleList = new RuleList();
            ruleList.ShowDialog();
        }

        private void enterSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                messageHelper.noUsernameEntered();
            }
            else
            {
                if (string.IsNullOrEmpty(passwordTextBox.Password.ToString()))
                {
                    messageHelper.noPasswordEntered();

                }
                else
                {
                    List<UserModel> users = helper.GetAllUsers();

                    bool usernameChecked = false;
                    bool passwordChecked = false;

                    foreach (UserModel user in users)
                    {
                        if (user.Username == usernameTextBox.Text)
                        {
                            usernameChecked = true;

                            if (user.Password == passwordTextBox.Password.ToString())
                            {
                                helper.StoreSessionID(user);

                                passwordChecked = true;

                                messageHelper.logInSuccessful();

                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();

                                
                            }
                        }
                    }

                    if (usernameChecked)
                    {
                        if (!passwordChecked)
                        {
                            messageHelper.wrongPassword();
                        }
                    }
                    else
                    {
                        messageHelper.wrongUsername();
                    }
                }
            }
        }
    }
}

