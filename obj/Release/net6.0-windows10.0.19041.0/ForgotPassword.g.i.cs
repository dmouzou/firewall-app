﻿#pragma checksum "..\..\..\ForgotPassword.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "186BE3AD46453D4CCE4DBC9FE8C56A1B44A1D285"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using _3M_Firewall;


namespace _3M_Firewall {
    
    
    /// <summary>
    /// ForgotPassword
    /// </summary>
    public partial class ForgotPassword : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\ForgotPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox securityKeyTextBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\ForgotPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock checkedTextBlock;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\ForgotPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button checkKeyButton;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\ForgotPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox changePasswordBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\ForgotPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox confirmPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\ForgotPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/3M_Firewall;component/forgotpassword.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ForgotPassword.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.securityKeyTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.checkedTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.checkKeyButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\ForgotPassword.xaml"
            this.checkKeyButton.Click += new System.Windows.RoutedEventHandler(this.checkKeyButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.changePasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.confirmPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.saveButton = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\ForgotPassword.xaml"
            this.saveButton.Click += new System.Windows.RoutedEventHandler(this.saveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

