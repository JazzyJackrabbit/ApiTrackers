﻿#pragma checksum "..\..\..\..\User_Control\Ctrl_User.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EFC8269687A75B997BAA170B0057846AD936574B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using ClientTest_APITrackers.UserControl;
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


namespace ClientTest_APITrackers {
    
    
    /// <summary>
    /// namespc
    /// </summary>
    public partial class namespc : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_id;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_SELECT;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_permissions;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_mail;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_pseudo;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_passwordHash;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_recoverMails;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_UPDATE;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_DELETE;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_INSERT;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\User_Control\Ctrl_User.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_SELECT_ALL;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientTest_APITrackers;component/user_control/ctrl_user.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\User_Control\Ctrl_User.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tb_id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btn_SELECT = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\..\User_Control\Ctrl_User.xaml"
            this.btn_SELECT.Click += new System.Windows.RoutedEventHandler(this.btn_SELECT_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tb_permissions = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tb_mail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tb_pseudo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tb_passwordHash = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tb_recoverMails = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btn_UPDATE = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\User_Control\Ctrl_User.xaml"
            this.btn_UPDATE.Click += new System.Windows.RoutedEventHandler(this.btn_UPDATE_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btn_DELETE = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\User_Control\Ctrl_User.xaml"
            this.btn_DELETE.Click += new System.Windows.RoutedEventHandler(this.btn_DELETE_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_INSERT = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\User_Control\Ctrl_User.xaml"
            this.btn_INSERT.Click += new System.Windows.RoutedEventHandler(this.btn_INSERT_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btn_SELECT_ALL = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\User_Control\Ctrl_User.xaml"
            this.btn_SELECT_ALL.Click += new System.Windows.RoutedEventHandler(this.btn_SELECT_ALL_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
