using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3M_Firewall.Helpers
{
    public class MessageHelper
    {
        MessageBoxButton messageBoxButtonOK = MessageBoxButton.OK;
        MessageBoxButton messageBoxButtonYesNo = MessageBoxButton.YesNo;

        internal void noIPAddressEntered()
        {
            MessageBox.Show("No IP Address has been entered." +
                "\nPlease enter a web address and try again.", "No Address",
                messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void invalidIPAddress()
        {
            MessageBox.Show("The IP Address entered is invalid." +
                "\nPlease enter a correct web address and try again.", "Invalid Address",
                messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void appPathNotEntered()
        {
            MessageBox.Show("Application path has not been entered." +
                "\nPlease click the ... button and select an application.", "No application selected",
                messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void checkSecurityKey()
        {
            MessageBox.Show("There is a problem with your security key.\n" +
                "Check it and try again.", "Problem with security key", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void securityKeyTooShort()
        {
            MessageBox.Show("Security key should be of 6 characters.", "Security key is too short", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void wrongSecurityKey()
        {
            MessageBox.Show("Wrong security key. Try again.", "Wrong security key", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void usernameNotLongEnough()
        {
            MessageBox.Show("Username should be at least 4 characters.", "Username not long enough", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void passwordNotLongEnough()
        {
            MessageBox.Show("Password should be at least 8 characters.", "Password not long enough", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void ruleCreatedSuccessfully()
        {
            MessageBox.Show("The rule has been successfully created and is now enabled." +
                    " If you wish to disable it, go to the rule list, select the rule and click on the 'Disable' button.", "Rule created",
                    messageBoxButtonOK, MessageBoxImage.Information);
        }

        internal void passwordChanged()
        {
            MessageBox.Show("Your password has been successfully changed.", "Password changed",
                    messageBoxButtonOK, MessageBoxImage.Information);
        }

        internal void passwordNotSame()
        {
            MessageBox.Show("Passwords are not the same. Try again.", "Passwords not the same", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void noUsernameEntered()
        {
            MessageBox.Show("Enter your username to continue.", "No username", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void noPasswordEntered()
        {
            MessageBox.Show("Enter your password to continue.", "No password", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void noRuleSelected()
        {
            MessageBox.Show("You have not selected a rule. \n" +
                "If there are no rules set, follow instructions and click on 'Add rule' button above to create a new rule.",
                "No rule selected", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal string newSessionSaved()
        {
            Random res = new Random();
            string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXZ0123456789";
            int size = 6;
            string randomstring = "";

            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);
                randomstring = randomstring + str[x];
            }

            MessageBox.Show("Your new session has been saved successfully. \n\n" +
                "Your security key is : " + randomstring + "\n\nPlease do not forget it, otherwise you won't be able to recover your session."
                , "Session saved", messageBoxButtonOK, MessageBoxImage.Information);

            return randomstring;
        }

        internal void noRuleSet()
        {
            MessageBox.Show("You have not set any rules yet. \n" +
                "Follow instructions and click on 'Add rule' button above to create a new rule.",
                "No rules created", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal void logInSuccessful()
        {
            MessageBox.Show("Congratulations. You have logged in successfully.", "Log in successful", messageBoxButtonOK, MessageBoxImage.Information);
        }

        internal void wrongPassword()
        {
            MessageBox.Show("Wrong Password. Try again.", "Wrong password", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal MessageBoxResult deleteRuleOrNot()
        {
            string message = "Are you sure you want to delete this rule? This action is irriversible.";
            string title = "Delete Rule";
            return MessageBox.Show(message, title, messageBoxButtonYesNo, MessageBoxImage.Warning);
        }

        internal void wrongUsername()
        {
            MessageBox.Show("Wrong username. Try again.", "Wrong username", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal MessageBoxResult deleteAllRulesOrNot()
        {
            string message = "Are you sure you want to delete all rules? This action is irriversible.";
            string title = "Delete All Rules";
            return MessageBox.Show(message, title, messageBoxButtonYesNo, MessageBoxImage.Warning);
        }

        internal MessageBoxResult blockAllTraffic()
        {
            string message = "Are you sure you want to block all traffic? You won't have access to internet services.";
            string title = "Block All Traffic";
            return MessageBox.Show(message, title, messageBoxButtonYesNo, MessageBoxImage.Warning);
        }

        internal void ruleSavedSuccessfully()
        {
            MessageBox.Show("The rule's new information have been saved successfully.", "Rule saved",
                    messageBoxButtonOK, MessageBoxImage.Information);
        }

        internal MessageBoxResult activateRuleOrNot()
        {
            string message = "Do you wish to enable this rule?";
            string title = "Activate Rule";
            return MessageBox.Show(message, title, messageBoxButtonYesNo, MessageBoxImage.Question);
        }

        internal void nameBoxEmpty()
        {
            MessageBox.Show("You must enter a name for the rule to continue.",
                "No name entered", messageBoxButtonOK, MessageBoxImage.Error);
        }

        internal MessageBoxResult quitOrNot()
        {
            return MessageBox.Show("Do you really want to quit?", "Quit app", messageBoxButtonYesNo, MessageBoxImage.Question);
        }
    }
}
