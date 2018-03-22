using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Command
    {
        public string CommandText { get; set; }
        public string Argument { get; set; }

        public override string ToString()
        {
            return $"Command: \"{CommandText}\" Argument: \"{Argument}\"";
        }
    }
}
