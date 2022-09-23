using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Run.Helpers
{
    public class UriRunner : ICommandRunner
    {
        public async Task<bool> RunAsync(string command)
        {
            try
            {
                return await Launcher.LaunchUriAsync(new Uri(command));
            }
            catch
            {
                return false;
            }
        }
    }
}
