using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run.Interfaces
{
    public interface ICommandRunner
    {
        Task<bool> RunAsync(string command); // Run command, Return success status
    }
}
