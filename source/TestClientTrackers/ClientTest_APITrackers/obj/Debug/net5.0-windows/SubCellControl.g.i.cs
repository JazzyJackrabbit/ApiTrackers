﻿#pragma checksum "..\..\..\SubCellControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "592438BF84FA1A495268E5D2EC1C80BBF82C1F47"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using ClientTest_APITrackers;
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
    /// SubCellControl
    /// </summary>
    public partial class SubCellControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ClientTest_APITrackers.SubCellControl grid_content;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_background;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_edit;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_visible;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider_key;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider_vol;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_delete;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\SubCellControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_idCell;
        
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
            System.Uri resourceLocater = new System.Uri("/ClientTest_APITrackers;V1.0.0.0;component/subcellcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SubCellControl.xaml"
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
            this.grid_content = ((ClientTest_APITrackers.SubCellControl)(target));
            return;
            case 2:
            this.grid_background = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.btn_edit = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\SubCellControl.xaml"
            this.btn_edit.Click += new System.Windows.RoutedEventHandler(this.btn_edit_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.grid_visible = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.slider_key = ((System.Windows.Controls.Slider)(target));
            
            #line 14 "..\..\..\SubCellControl.xaml"
            this.slider_key.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.slider_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.slider_vol = ((System.Windows.Controls.Slider)(target));
            
            #line 15 "..\..\..\SubCellControl.xaml"
            this.slider_vol.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.slider_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_delete = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\SubCellControl.xaml"
            this.btn_delete.Click += new System.Windows.RoutedEventHandler(this.btn_delete_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lbl_idCell = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

