﻿#pragma checksum "..\..\..\..\Visual\CreatePlayer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A0767619EB884D5534E80D82FB1AB0E49E56F5CD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
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


namespace Basket.Visual {
    
    
    /// <summary>
    /// CreatePlayer
    /// </summary>
    public partial class CreatePlayer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 219 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PlayerName;
        
        #line default
        #line hidden
        
        
        #line 222 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LastName;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CityComboBox;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePickerQuery;
        
        #line default
        #line hidden
        
        
        #line 239 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SecondNameLabel;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SecondLastNameLabel;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumberLabel;
        
        #line default
        #line hidden
        
        
        #line 248 "..\..\..\..\Visual\CreatePlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TeamComboBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Basket;component/visual/createplayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Visual\CreatePlayer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PlayerName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.LastName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.CityComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.DatePickerQuery = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.SecondNameLabel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.SecondLastNameLabel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.NumberLabel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.TeamComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            
            #line 257 "..\..\..\..\Visual\CreatePlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 258 "..\..\..\..\Visual\CreatePlayer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

