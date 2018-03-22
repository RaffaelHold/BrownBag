using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class GitExecuter
    {
        public void Execute(string command, string param)
        {
            switch (command)
            {
                case "add":
                    Process.Start("git", "add -A");
                    break;
                case "commit":
                    Process.Start("git", $"commit -m \"{param}\"");
                    break;
                case "pull":
                    Process.Start("git", $"pull");
                    break;
                case "push":
                    Process.Start("git", $"push {param}");
                    break;
            }
        }
    }
}
