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
    public class PathRunner : IAdminCommandRunner
    {
        public async Task<bool> RunAsync(string command, bool isAdmin)
        {
        
            try
            {
                return await CmdRunner.HiddenRunAsync(@$"start %windir%\explorer.exe {command}", isAdmin);
            }
            catch
            {
                return false;
            }
        }
    }
}
