using H.NotifyIcon.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using WinUIEx;

namespace Run.Services
{
    public static class TrayService
    {
        private static WindowEx Run;
        private static H.NotifyIcon.Core.TrayIcon trayIcon = new()
        {
            Icon =
           new Bitmap(
               @$"{Package.Current.InstalledPath}\Assets\Square44x44Logo.scale-100.png"
           ).GetHicon(),
            Visibility = IconVisibility.Visible,
        };

        public static void Setup(WindowEx run)
        {
            if (!new SettingsService().Tray)
                return;
            Run = run;

            trayIcon.Create();
            trayIcon.MessageWindow.MouseEventReceived += MessageWindow_MouseEventReceived;
        }

        public static void Recreate()
        {
            trayIcon.Show();
            trayIcon.MessageWindow.MouseEventReceived += MessageWindow_MouseEventReceived;
        }

        public static void Remove()
        {
            trayIcon.MessageWindow.MouseEventReceived -= MessageWindow_MouseEventReceived;
            trayIcon.Hide();
        }

        private static void MessageWindow_MouseEventReceived(object sender, MessageWindow.MouseEventReceivedEventArgs e)
        {
            if (!new SettingsService().Tray)
            {
                trayIcon.MessageWindow.MouseEventReceived -= MessageWindow_MouseEventReceived;
                trayIcon.Hide();
                return;
            }
            if (e.MouseEvent == MouseEvent.IconLeftMouseDown)
            {
                Run.Show();
                Run.SetForegroundWindow();
                Run.BringToFront();
            }
            else if (e.MouseEvent == MouseEvent.IconRightMouseDown)
            {
                SettingsWindow s_window = new SettingsWindow();
                s_window.Activate();
            }
        }
    }
}
