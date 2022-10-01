using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using WinUIEx;
using Run.ViewModels;
using Windows.Storage.Pickers;
using Windows.Storage;
using System;
using Microsoft.Win32;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Run
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TempWindow : WindowEx
    {
        RunBoxViewModel RunViewModel;
        public TempWindow()
        {
            RunViewModel = new();
            this.InitializeComponent();
            RunViewModel.CommandExecuted += ProcessExecution;
            Bindings.Update();
            BitmapImage b = new(new Uri("ms-appx:///Assets/Square44x44Logo.scale-100.png")); // why
            AppFontIcon.Source = b;
        }

        private void ProcessExecution(object sender, bool e)
        {
            if (e)
            {
                Exit();
                RunBox.Foreground = (LinearGradientBrush)App.Current.Resources["AccentLinearGradientBrush"];
            }
            else
            {
                RunBox.Foreground = (LinearGradientBrush)App.Current.Resources["RedLinearGradientBrush"];
                RunBox.Focus(FocusState.Pointer);
                RunShakeAnimation.Start();
            }
        }

        private void RunWindow_Activated(object sender, WindowActivatedEventArgs args) => RunBox.Focus(FocusState.Pointer);

        private void Focus(object sender, RoutedEventArgs e) => RunBox.Focus(FocusState.Pointer);

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow s_window = new SettingsWindow();
            s_window.Activate();
        }

        private void RunBox_Loaded(object sender, RoutedEventArgs e) => RunBox.Focus(FocusState.Pointer);

        //Obsolete
        #region to remove
        public void Exit()
        {
            if (RunViewModel.Settings.PersistAppInBackground)
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

        private void RunBox_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
          //  if(RunBox.FocusState == FocusState.Unfocused)
           //     RunViewModel.RunCommand.Execute(this);
        }
        #endregion
    }
}
