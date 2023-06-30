using Run.Interfaces;
using Run.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;

namespace Run.Helpers
{
    public class CommandHelper
    {

        public static async Task<bool> ExecuteCommand(string command, bool isAdmin)
        {
            if (command.Contains(":") && !command.StartsWith("shell:"))
            {
                UriRunner uriRunner = new UriRunner();
                bool success = await uriRunner.RunAsync(command);
                if (!success)
                    await Backup(command, isAdmin);
                return success;
            }
            else if(command.ToLower().StartsWith("cmd") && command != "cmd")
            {
                return await CmdRunner.RunAsync(command, isAdmin);
            }
            else if(command.StartsWith("%"))
            {
                PathRunner cmdRunner = new PathRunner();
                return await cmdRunner.RunAsync(command, isAdmin);
            }
            else
            {
                ProcessRunner procRunner = new ProcessRunner(); // Try launching process
                bool success = await procRunner.RunAsync(command);
                if (!success)
                {
                    success = await Backup(command, isAdmin); // Try admin cmd command
                    if (!success && SettingsService.HasKey) // Try GPT if key exists
                    {
                        GPTRunner gptRunner = new GPTRunner();
                        success = await gptRunner.RunAsync(command, isAdmin);
                    }
                }
                return success;
            }
        }

        private static async Task<bool> Backup(string command, bool isAdmin) => await CmdRunner.HiddenRunAsync(command, isAdmin);
    }
}
