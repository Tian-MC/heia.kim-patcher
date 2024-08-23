using System;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using HoLLy.ManagedInjector;

namespace OsuInjector
{
    internal class Program
    {
        public static void Main(string[] _)
        {
            try
            {
                var hookDllPath = Path.GetFullPath(typeof(Program).Assembly.Location + @"\..\osu!.hook.dll");

                if (!File.Exists(hookDllPath))
                    throw new Exception("Unable to find osu!.hook.dll in the current directory! Are you trying to cheat?");

                using (var proc = new InjectableProcess(GetOsuPid()))
                {
                    proc.Inject(hookDllPath, "OsuHook.MainHook", "Initialize");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);

                Console.WriteLine("\nPress any key to continue...");
                Console.Write("\a"); // Bell sound
                Console.ReadKey();
            }
        }

        /// <summary>
        ///     Find a <c>osu!.exe</c> process that has a devserver in the cli arguments.
        ///     (Not connected to osu!bancho)
        /// </summary>
        /// <returns>The process id of the first matching process.</returns>
        /// <exception cref="Exception">If found invalid osu! process or no process at all.</exception>
        private static uint GetOsuPid()
        {
            using (var mgmt = new ManagementClass("Win32_Process"))
            using (var processes = mgmt.GetInstances())
            {
                foreach (var process in processes)
                {
                    var exe = (string)process["Name"];
                    var pid = (uint)process["ProcessId"];
                    var cli = (string)process["CommandLine"];

                    if (exe != "osu!.exe") continue;

                    // Check if the process is connected to a non osuwtf.pw server
                    var match = Regex.Match(cli, @"-devserver\s+(\S+)");
                    if (match.Success)
                    {
                        var server = match.Groups[1].Value;

                        if (server != "osuwtf.pw")
                        {
                            // osu! crash
                            ForceCrash(pid, "This patcher is only compatible with osu!wtf.");
                        }

                        return pid;
                    }

                    throw new Exception("Will not inject into osu! connected to Bancho! Is bannable!");
                }
            }

            throw new Exception("Cannot find a running osu! process! Does osu! open?");
        }

        /// <summary>
        ///     Forces the osu! process to crash and displays a custom error message.
        /// </summary>
        /// <param name="pid">The process ID of the osu! process.</param>
        /// <param name="errorMessage">The error message to display before crashing.</param>
        private static void ForceCrash(uint pid, string errorMessage)
        {
            try
            {
                // Attach to the osu! process
                var process = System.Diagnostics.Process.GetProcessById((int)pid);

                // Show error message
                Console.WriteLine(errorMessage);

                // Terminate the process
                process.Kill();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to crash osu! process: {ex.Message}");
            }
        }
    }
}