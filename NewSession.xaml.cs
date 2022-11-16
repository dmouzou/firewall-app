using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using _3M_Firewall.Models;
using _3M_Firewall.Helpers;

namespace _3M_Firewall
{
    /// <summary>
    /// Interaction logic for NewSession.xaml
    /// </summary>
    public partial class NewSession : Window
    {
        MessageHelper messageHelper = new MessageHelper();

        Helper helper = new Helper();

        public NewSession()
        {
            
            InitializeComponent();

        }

        private void saveSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newUsernameTextBox.Text) || newUsernameTextBox.Text.Length < 4)
            {
                messageHelper.usernameNotLongEnough();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(newPasswordBox.Password.ToString()) || newPasswordBox.Password.ToString().Length < 8)
                {
                    messageHelper.passwordNotLongEnough();
                    newPasswordBox.Password = "";
                    confirmPasswordBox.Password = "";
                }
                else
                {
                    if (confirmPasswordBox.Password.ToString() != newPasswordBox.Password.ToString())
                    {
                        messageHelper.passwordNotSame();
                        confirmPasswordBox.Password = "";
                    }
                    else
                    {
                        UserModel user = new UserModel();
                        user.Username = newUsernameTextBox.Text;
                        user.Password = newPasswordBox.Password.ToString();
                        user.SecurityKey = messageHelper.newSessionSaved();

                        helper.CreateNewSession(user);

                        this.Close();
                    }
                }
            }
        }
    }
}
