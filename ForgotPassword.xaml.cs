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

namespace _3M_Firewall
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        MessageHelper messageHelper = new MessageHelper();

        Helper helper = new Helper();

        public ForgotPassword()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }


        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkedTextBlock.Visibility == Visibility.Hidden)
            {
                messageHelper.checkSecurityKey();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(changePasswordBox.Password.ToString()) || changePasswordBox.Password.ToString().Length < 8)
                {
                    messageHelper.passwordNotLongEnough();
                    changePasswordBox.Password = "";
                }
                else
                {
                    if (confirmPasswordBox.Password.ToString() != changePasswordBox.Password.ToString())
                    {
                        messageHelper.passwordNotSame();
                        confirmPasswordBox.Password = "";
                    }
                    else
                    {
                        UserModel user = new UserModel();
                        user.SecurityKey = securityKeyTextBox.Text;
                        user.Password = confirmPasswordBox.Password.ToString();

                        helper.ChangeUserPassword(user);

                        messageHelper.passwordChanged();

                        this.Close();
                    }
                }
            }

        }

        private void checkKeyButton_Click(object sender, RoutedEventArgs e)
        {
            bool securityKeyChecked = false;

            if (string.IsNullOrWhiteSpace(securityKeyTextBox.Text) || securityKeyTextBox.Text.Length < 6)
            {
                messageHelper.securityKeyTooShort();
            }
            else
            {
                List<UserModel> users = helper.GetAllUsers();

                foreach (UserModel user in users)
                {
                    if (securityKeyTextBox.Text.Equals(user.SecurityKey))
                    {
                        changePasswordBox.IsEnabled = true;
                        confirmPasswordBox.IsEnabled = true;

                        securityKeyChecked = true;
                        break;
                    }

                }

                if (securityKeyChecked)
                {
                    securityKeyTextBox.IsEnabled = false;
                    checkKeyButton.IsEnabled = false;
                    checkedTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    messageHelper.wrongSecurityKey();
                }
            }
        }
    }
}
