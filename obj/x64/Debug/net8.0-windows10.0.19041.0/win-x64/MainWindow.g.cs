﻿#pragma checksum "C:\Users\klonerovsky\VS projects\OkayuLoader\OkayuLoader\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2F115E3CAB1DD58615A6867D190519E7544B1AB13941CB2E75C35A3BC9DA56B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OkayuLoader
{
    partial class MainWindow : 
        global::Microsoft.UI.Xaml.Window, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainWindow.xaml line 22
                {
                    this.AppTitleBar = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 3: // MainWindow.xaml line 41
                {
                    this.NavigationView = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.NavigationView>(target);
                    ((global::Microsoft.UI.Xaml.Controls.NavigationView)this.NavigationView).SelectionChanged += this.NavigationViewBehavior;
                    ((global::Microsoft.UI.Xaml.Controls.NavigationView)this.NavigationView).Loaded += this.NavigationViewInit;
                }
                break;
            case 4: // MainWindow.xaml line 56
                {
                    this.NvTabHome = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.NavigationViewItem>(target);
                }
                break;
            case 5: // MainWindow.xaml line 59
                {
                    this.NvTabSettings = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.NavigationViewItem>(target);
                }
                break;
            case 6: // MainWindow.xaml line 61
                {
                    this.contentFrame = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Frame>(target);
                }
                break;
            case 7: // MainWindow.xaml line 24
                {
                    this.LeftPaddingColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 8: // MainWindow.xaml line 26
                {
                    this.RightPaddingColumn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ColumnDefinition>(target);
                }
                break;
            case 9: // MainWindow.xaml line 28
                {
                    this.TitleBarIcon = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Image>(target);
                }
                break;
            case 10: // MainWindow.xaml line 33
                {
                    this.TitleBarTextBlock = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2404")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

