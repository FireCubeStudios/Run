using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using WinUIEx;
using Run.ViewModels;
using Windows.Storage.Pickers;
using Windows.Storage;
using System;
using Microsoft.Win32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Run
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
    {
        RunBoxViewModel RunViewModel;
        public MainWindow()
        {
            RunViewModel = new(this);
            this.InitializeComponent();
            Bindings.Update();
        }

        // Titlebar dragging without titlebar workaround
        private void AppTitleBar_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var appX = this.AppWindow.Position.X;
            var appY = this.AppWindow.Position.Y;

            var height = this.Height;
            var width = this.Width;

            this.MoveAndResize(appX + e.Position.X, appY + e.Position.Y, width, height);
        }

        private void RunWindow_Activated(object sender, WindowActivatedEventArgs args) => RunBox.Focus(FocusState.Pointer);

        private void Focus(object sender, RoutedEventArgs e) => RunBox.Focus(FocusState.Pointer);

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow s_window = new SettingsWindow();
            s_window.Activate();
        }

        private void RunBox_Loaded(object sender, RoutedEventArgs e) => RunBox.Focus(FocusState.Pointer);
    }
}
