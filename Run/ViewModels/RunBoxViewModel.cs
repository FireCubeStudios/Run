using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUIEx;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Diagnostics;
using System.ComponentModel;
using Run.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Run.Services;

namespace Run.ViewModels
{
    public partial class RunBoxViewModel : ObservableObject
    {
        public EventHandler<bool> CommandExecuted;

        [ObservableProperty]
        private string commandText;

        public HistoryService Recents = App.Current.Services.GetService<HistoryService>();
        public SettingsService Settings = App.Current.Services.GetService<SettingsService>();

        [RelayCommand]
        private async Task Run()
        {
            if (String.IsNullOrEmpty(commandText))
            {
                CommandExecuted.Invoke(this, false);
                return;
            }
            bool Success = await CommandHelper.ExecuteCommand(commandText.Trim(), false);
            CommandExecuted.Invoke(this, Success);
            if (Success)
            {
                Recents.AddItem(commandText);
                CommandText = "";
            }
        }

        [RelayCommand]
        private async Task RunAdmin()
        {
            if (String.IsNullOrEmpty(commandText))
            {
                CommandExecuted.Invoke(this, false);
                return;
            }
            bool Success = await CommandHelper.ExecuteCommand(commandText.Trim(), true);
            CommandExecuted.Invoke(this, Success);
            if (Success)
            {
                Recents.AddItem(commandText);
                CommandText = "";
            }
        }

        [RelayCommand]
        private async Task BrowseAsync(WindowEx RunWindow)
        {
            var picker = RunWindow.CreateOpenFilePicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Add("*");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
                CommandText = file.Path;
        }
    }
}
