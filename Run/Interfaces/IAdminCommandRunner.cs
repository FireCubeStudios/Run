using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run.Interfaces
{
    internal interface IAdminCommandRunner
    {
        Task<bool> RunAsync(string command, bool isAdmin); // Run command, Return success status
    }
}
