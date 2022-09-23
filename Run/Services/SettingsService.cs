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
    public class SettingsService
    {
        ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;

        public bool Hide
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
                Settings.Values["Hide"] = true;
            }
        }
    }
}
