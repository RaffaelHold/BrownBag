using Common;
using Common.Services;
using Common.Statics;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var powerPointBus = new ServiceBus(ServiceBusQueues.PowerPoint, HandlePowerPointMessage);
            var gitBus = new ServiceBus(ServiceBusQueues.GitQueue, HandleGitMessage);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after receiving all the messages.");
            Console.WriteLine("======================================================");

            Console.ReadKey();

            await powerPointBus.CloseAsync();
            await gitBus.CloseAsync();
        }

        static void HandlePowerPointMessage(string message)
        {
            var p = Process.GetProcessesByName("POWERPNT").FirstOrDefault();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                System.Windows.Forms.SendKeys.SendWait(message);
            }
        }

        static void HandleGitMessage(string message)
        {
            var command = JsonConvert.DeserializeObject<Command>(message);

            var executor = new GitExecuter();
            executor.Execute(command.CommandText, command.Argument);
        }
    }
}

