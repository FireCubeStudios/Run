﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Run.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Run
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsWindow : WindowEx
    {
        public SettingsService Settings = App.Current.Services.GetService<SettingsService>();

        public SettingsWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            if (Settings.PersistAppInBackground)
                Keyboard.Visibility = Visibility.Visible;
            else
                Keyboard.Visibility = Visibility.Collapsed;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if(((ToggleSwitch)sender).IsOn)
            {
                Keyboard.Visibility = Visibility.Visible;
            }
            else
            {
                Keyboard.Visibility = Visibility.Collapsed;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Exit();

        private void Title_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  Settings.AppTitle = ((ComboBox)sender).SelectedValue as string;
        }
    }
}
