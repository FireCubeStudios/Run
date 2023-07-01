using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Helpers;
using Run.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Run.Services
{
    public partial class SettingsService : ObservableObject
    {
        private static ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;

        private bool autoPin = (bool)(Settings.Values["Hide"] ?? true);
        public bool PersistAppInBackground
        {
            get => autoPin;
            set
            {
                Settings.Values["Hide"] = value;
                SetProperty(ref autoPin, value); 
            }
        }

        private bool keyboardEnabled = (bool)(Settings.Values["KeyboardEnabled"] ?? true);
        public bool KeyboardEnabled
        {
            get => keyboardEnabled;
            set
            {
                Settings.Values["KeyboardEnabled"] = value;
                SetProperty(ref keyboardEnabled, value);
                if (value)
                    KeyboardHelper.StartHook();
                else
                    KeyboardHelper.StopHook();
            }
        }

        private static bool hasKey = (bool)(Settings.Values["HasKey"] ?? false);
        public static bool HasKey
        {
            get => hasKey;
            set
            {
                Settings.Values["HasKey"] = value;
                hasKey = value;
            }
        }

        private bool tray = (bool)(Settings.Values["Tray"] ?? false);
        public bool Tray
        {
            get => tray;
            set
            {
                Settings.Values["Tray"] = value;
                SetProperty(ref tray, value);
                SetProperty(ref tray, value);
                if (value)
                    TrayService.Recreate();
                else
                    TrayService.Remove();
            }
        }

        private bool exit = (bool)(Settings.Values["Exit"] ?? true);
        public bool Exit
        {
            get => exit;
            set
            {
                Settings.Values["Exit"] = value;
                SetProperty(ref exit, value);
            }
        }

        [RelayCommand]
        public async void LaunchSettings()
        {
            await Task.Delay(500);
            SettingsWindow s_window = new SettingsWindow();
            s_window.Activate();
        }
    }
}
