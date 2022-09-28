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
    public partial class SettingsService
    {
        ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;

        public bool PersistAppInBackground
        {
            get => (bool)Settings.Values["Hide"];
            set
            {
                Settings.Values["Hide"] = value;
              /*  if((bool)value)
                    RegistryHelper.DisableDefault();
                else
                    RegistryHelper.EnableDefault();*/
            }
        }

        public SettingsService()
        {
            if (SystemInformation.Instance.IsFirstRun)
            {
                PersistAppInBackground = true;
            }
        }

        [RelayCommand]
        public void LaunchSettings()
        {
            SettingsWindow s_window = new SettingsWindow();
            s_window.Activate();
        }
    }
}
