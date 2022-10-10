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

        public string AppTitle
        {
            get => (string)Settings.Values["Title"];
            set => Settings.Values["Title"] = value;
        }

        public bool TempAppTheme // temporary, true = default, false = other theme
        {
            get => (bool)Settings.Values["TempAppTheme"];
            set
            {
                Settings.Values["TempAppTheme"] = value;
                try
                {
                    if ((bool)value)
                    {
                        App.Current.m_window.Close();
                        App.Current.LaunchNewMain();
                    }
                    else
                    {
                        App.Current.m_window.Close();
                        App.Current.LaunchNewGlow();
                    }
                }
                catch
                {

                }
            }
        }

        public SettingsService()
        {
            if (SystemInformation.Instance.IsFirstRun)
            {
                PersistAppInBackground = true;
                AppTitle = "Run by FireCube (Not by microsoft)";
                TempAppTheme = true;
            }
            else if(SystemInformation.Instance.IsAppUpdated)
            {
                AppTitle = "Run by FireCube (Not by microsoft)";
                TempAppTheme = true;
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
