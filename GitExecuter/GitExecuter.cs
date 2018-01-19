using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GitExecuter
{
    public class GitExecutor
    {
        public void Execute(string command, string param)
        {
            Process.Start("git", "commit -m \"Initial commit\"");
        }
    }
}
