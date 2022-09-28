using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Run.Services;
using Run.ViewModels;
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
    public partial class BaseWindow : WindowEx
    {
        public SettingsService Settings = App.Current.Services.GetService<SettingsService>();
        public RunBoxViewModel RunViewModel;

        public BaseWindow()
        {
            RunViewModel = new();
            this.InitializeComponent();
        }

        public void Exit()
        {
            if (Settings.PersistAppInBackground)
                this.Hide();
            else
                this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Exit();

        // Titlebar dragging without titlebar workaround
        private void AppTitleBar_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var appX = this.AppWindow.Position.X;
            var appY = this.AppWindow.Position.Y;

            var height = this.Height;
            var width = this.Width;

            this.MoveAndResize(appX + e.Position.X, appY + e.Position.Y, width, height);
        }
    }
}
