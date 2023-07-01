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
using Microsoft.UI.Input;
using Windows.UI.Input;
using System.Runtime.InteropServices;
using System.Drawing;
using Run.Services;

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
            RunViewModel = new();
            this.InitializeComponent();
            RunViewModel.CommandExecuted += ProcessExecution;
            Bindings.Update();
            BitmapImage b = new(new Uri("ms-appx:///Assets/Square44x44Logo.scale-100.png")); // why
            AppFontIcon.Source = b;
            TrayService.Setup(this);
        }

        private void ProcessExecution(object sender, bool e)
        {
            if (e)
            {
                if(new SettingsService().Exit)
                    Exit();
                RunBox.Foreground = (LinearGradientBrush)App.Current.Resources["AccentLinearGradientBrush"];
            }
            else
            {
                RunBox.Reject();
            }
        }

        private void RunWindow_Activated(object sender, WindowActivatedEventArgs args) => RunBox.Focus(FocusState.Pointer);

        private void Focus(object sender, RoutedEventArgs e) => RunBox.Focus(FocusState.Pointer);

        private void RunBox_Loaded(object sender, RoutedEventArgs e) => RunBox.Focus(FocusState.Pointer);

        private void RunBox_TextSubmitted(object sender, string e) => RunViewModel.RunCommand.Execute(this);

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
            GetCursorPos(out Point point);

            var appX = this.AppWindow.Position.X;
            var appY = this.AppWindow.Position.Y;

            var diffX = point.X - this.AppWindow.Position.X;
            var diffY = point.Y - this.AppWindow.Position.Y;


            var height = this.Height;
            var width = this.Width;

            this.MoveAndResize(point.X - diffX + (e.Delta.Translation.X * GetScale()), point.Y - diffY + (e.Delta.Translation.Y * GetScale()), width, height);
        }

        private double GetScale()
        {
            var progmanWindow = FindWindow("Shell_TrayWnd", null);
            var monitor = MonitorFromWindow(progmanWindow, MONITOR_DEFAULTTOPRIMARY);

            DeviceScaleFactor scale;
            GetScaleFactorForMonitor(monitor, out scale);

            if (scale == DeviceScaleFactor.DEVICE_SCALE_FACTOR_INVALID)
                scale = DeviceScaleFactor.SCALE_100_PERCENT;

            return Convert.ToDouble(scale) / 100;
        }

        public const int MONITOR_DEFAULTTOPRIMARY = 1;
        public const int MONITOR_DEFAULTTONEAREST = 2;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("shcore.dll")]
        public static extern IntPtr GetScaleFactorForMonitor(IntPtr hwnd, out DeviceScaleFactor dwFlags);

        public enum DeviceScaleFactor
        {
            DEVICE_SCALE_FACTOR_INVALID = 0,
            SCALE_100_PERCENT = 100,
            SCALE_120_PERCENT = 120,
            SCALE_125_PERCENT = 125,
            SCALE_140_PERCENT = 140,
            SCALE_150_PERCENT = 150,
            SCALE_160_PERCENT = 160,
            SCALE_175_PERCENT = 175,
            SCALE_180_PERCENT = 180,
            SCALE_200_PERCENT = 200,
            SCALE_225_PERCENT = 225,
            SCALE_250_PERCENT = 250,
            SCALE_300_PERCENT = 300,
            SCALE_350_PERCENT = 350,
            SCALE_400_PERCENT = 400,
            SCALE_450_PERCENT = 450,
            SCALE_500_PERCENT = 500,
        }

        #endregion

        private void RunBox_Expanded(object sender, bool e) => this.Height = 270;

        private void RunBox_Collapsed(object sender, bool e) => this.Height = 180;
    }
}
