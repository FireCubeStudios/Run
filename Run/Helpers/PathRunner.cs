using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Run.Helpers
{
    public class PathRunner : ICommandRunner
    {
        public async Task<bool> RunAsync(string command)
        {
        
            try
            {
                CmdRunner cmdRunner = new CmdRunner();
                return await cmdRunner.HiddenRunAsync(@$"start %windir%\explorer.exe {command}");
            }
            catch
            {
                return false;
            }
        }
    }
}
