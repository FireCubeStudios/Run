using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run.Helpers
{
    public class CommandHelper
    {

        public static async Task<bool> ExecuteCommand(string command)
        {
            if (command.Contains(":") && !command.StartsWith("shell:"))
            {
                UriRunner uriRunner = new UriRunner();
                bool success = await uriRunner.RunAsync(command);
                if (!success)
                {
                    CmdRunner cmdRunner = new CmdRunner();
                    success = await cmdRunner.HiddenRunAsync(command);
                }
                return success;
            }
            else if(command.ToLower().StartsWith("cmd") && command != "cmd")
            {
                CmdRunner cmdRunner = new CmdRunner();
                return await cmdRunner.RunAsync(command);
            }
            else if(command.StartsWith("%"))
            {
                PathRunner cmdRunner = new PathRunner();
                return await cmdRunner.RunAsync(command);
            }
            else
            {
                ProcessRunner procRunner = new ProcessRunner();
                bool success = await procRunner.RunAsync(command);
                if(!success)
                {
                    CmdRunner cmdRunner = new CmdRunner();
                    await cmdRunner.HiddenRunAsync(command);
                }
                return success;
            }
        }
    }
}
