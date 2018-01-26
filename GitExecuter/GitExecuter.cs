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
            switch (command)
            {
                case "Add":
                    Process.Start("git", "add -A");
                    break;
                case "Commit":
                    Process.Start("git", $"commit -m \"{param}\"");
                    break;
                case "Pull":
                    Process.Start("git", $"pull");
                    break;
                case "Push":
                    Process.Start("git", $"push {param}");
                    break;
            }
        }
    }
}
