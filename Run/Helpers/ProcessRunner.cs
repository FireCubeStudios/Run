using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run.Helpers
{
    public class ProcessRunner : ICommandRunner
    {
        public async Task<bool> RunAsync(string command)
        {
            try
            {
                await Task.Run(() =>
                {
                     ProcessStartInfo procStartInfo = new ProcessStartInfo(command);
                     procStartInfo.UseShellExecute = true;
                     Process proc = new Process();
                     proc.StartInfo = procStartInfo;
                     proc.Start();
                });
                return true;
            }
            catch
            {
                try
                {
                    // Obsolete 
                    await Task.Run(() =>
                    {
                        Process.Start(command);
                    });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
