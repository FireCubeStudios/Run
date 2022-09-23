using CommunityToolkit.WinUI.Helpers;
using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Run.Services
{
    public class HistoryService
    {
        ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;
        public HashSet<string> History = new();

        public HistoryService()
        {
            if(SystemInformation.Instance.IsFirstRun)
            {
                AddItem("winver");
            }
            else
            {
                foreach (string command in (string[])Settings.Values["History"])
                {
                    History.Add(command);
                }
            }
        }

        public async void AddItem(string command)
        {
            History.Add(command);
            if (History.Count > 30)
                History.Remove(History.Last());
            await SaveItemsAsync();
        }

        public async Task SaveItemsAsync()
        {
            await Task.Run(() => {
                Settings.Values["History"] = History.ToArray();
            });
        }
    }
}
