using Microsoft.Extensions.DependencyInjection;
using Run.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinUIEx;
using WinUIEx.Messaging;

namespace Run.Services
{
    public class KeyboardService
    {
        private static KeyboardHelper KeyboardHook;

        private static SettingsService Settings = App.Current.Services.GetService<SettingsService>();

        private static int KeysValid = 0;

        public static void Initialize()
        {
            if (Settings.Hide)
            {
                KeyboardHook = new KeyboardHelper();
                KeyboardHook.KeyboardPressed += OnKeyPressed;
            }
        }

        private static void OnKeyPressed(object sender, KeyboardHelperEventArgs e)
        {
            if (!Settings.Hide)
                return;

            if (e.KeyboardState == KeyboardHelper.KeyboardState.KeyDown)
            {
                if(e.KeyboardData.VirtualCode == 0x5B || e.KeyboardData.VirtualCode == 0x10 || e.KeyboardData.VirtualCode == 0x52)
                {
                    KeysValid++;    
                }
                if(KeysValid == 3)
                {
                    ((WindowEx)App.Current.m_window).Show();
                }
            }
            else if(e.KeyboardState == KeyboardHelper.KeyboardState.KeyUp)
            {
                if (e.KeyboardData.VirtualCode == 0x5B || e.KeyboardData.VirtualCode == 0x10 || e.KeyboardData.VirtualCode == 0x52)
                {
                    KeysValid--;
                }
            }
        }

    }
}
