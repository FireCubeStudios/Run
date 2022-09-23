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
        [ObservableProperty]
        private string commandText;

        public HistoryService Recents = App.Current.Services.GetService<HistoryService>();
        private SettingsService Settings = App.Current.Services.GetService<SettingsService>();

        private WindowEx RunWindow;

        //  public RunBoxViewModel(WindowEx Window) => RunWindow = Window;

        public RunBoxViewModel(WindowEx Window)
        {
            RunWindow = Window;
        }

        [RelayCommand]
        private async void Run()
        {
            if(String.IsNullOrEmpty(commandText))
                return;
            bool Success = await CommandHelper.ExecuteCommand(commandText.Trim());
            if (Success)
            {
                Recents.AddItem(commandText);
                CommandText = "";
                if(Settings.Hide)
                    RunWindow.Hide();
            }
            else
            {

            }
        }

        [RelayCommand]
        private void Hide() => RunWindow.Hide();

        [RelayCommand]
        private async Task BrowseAsync()
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
