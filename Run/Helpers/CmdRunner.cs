using Run.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Run.Helpers
{
    public static class CmdRunner
    {
        public static async Task<bool> RunAsync(string command, bool isAdmin)
        {
            try
            {
                await Task.Run(() =>
                {
                    command.Replace("cmd", "");
                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command.Trim());
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    procStartInfo.RedirectStandardOutput = true;
                    procStartInfo.RedirectStandardError = true;
                    using (Process process = new Process())
                    {
                        process.StartInfo = procStartInfo;
                        process.Start();

                        // Read the output and error streams
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        process.WaitForExit();

                        return process.ExitCode == 0;
                    }
                });
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> HiddenRunAsync(string command, bool IsAdmin)
        {
            try
            {
                await Task.Run(() =>
                {
                    command.Replace("cmd", "");
                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command.Trim());
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    procStartInfo.RedirectStandardOutput = true;
                    procStartInfo.RedirectStandardError = true;
                    procStartInfo.Verb = IsAdmin ? "runas" : "";
                    using (Process process = new Process())
                    {
                        process.StartInfo = procStartInfo;
                        process.Start();

                        // Read the output and error streams
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        process.WaitForExit();

                        return process.ExitCode == 0;
                    }
                });
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
