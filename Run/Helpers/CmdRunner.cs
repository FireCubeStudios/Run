using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Run.Helpers
{
    public class CmdRunner : ICommandRunner
    {
        public async Task<bool> RunAsync(string command)
        {
            try
            {
                await Task.Run(() =>
                {
                    command.Replace("cmd", "");
                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command.Trim());
                    procStartInfo.UseShellExecute = true;
                    Process proc = new Process();
                    proc.StartInfo = procStartInfo;
                    proc.Start();
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> HiddenRunAsync(string command)
        {
            try
            {
                await Task.Run(() =>
                {
                    command.Replace("cmd", "");
                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command.Trim());
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    Process proc = new Process();
                    proc.StartInfo = procStartInfo;
                    proc.Start();
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
