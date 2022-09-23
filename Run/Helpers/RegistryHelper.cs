using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run.Helpers
{
    public static class RegistryHelper
    {
        public static void DisableDefault()
        {
            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            key.SetValue("NoRun", 0, RegistryValueKind.DWord);
            key.Close();
        }

        public static void EnableDefault()
        {
            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            key.SetValue("NoRun", 1, RegistryValueKind.DWord);
            key.Close();
        }
    }
}
