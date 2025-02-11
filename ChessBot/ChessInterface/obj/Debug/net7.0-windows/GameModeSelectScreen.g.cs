﻿#pragma checksum "..\..\..\GameModeSelectScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E9761A44A4C0ED998C7765CFA52607EBD9DE7D7B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using ChessInterface;
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


namespace ChessInterface {
    
    
    /// <summary>
    /// GameModeSelectScreen
    /// </summary>
    public partial class GameModeSelectScreen : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\GameModeSelectScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image HumanVSHumanImg;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\GameModeSelectScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image HumanVSAiImg;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\GameModeSelectScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AiVSAiImg;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChessInterface;component/gamemodeselectscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GameModeSelectScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.HumanVSHumanImg = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\..\GameModeSelectScreen.xaml"
            this.HumanVSHumanImg.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.HumanVSHumanImg_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.HumanVSAiImg = ((System.Windows.Controls.Image)(target));
            
            #line 31 "..\..\..\GameModeSelectScreen.xaml"
            this.HumanVSAiImg.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.HumanVSAiImg_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AiVSAiImg = ((System.Windows.Controls.Image)(target));
            
            #line 34 "..\..\..\GameModeSelectScreen.xaml"
            this.AiVSAiImg.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.AiVSAiImg_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

