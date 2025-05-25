using System;
using System.Diagnostics;

namespace ServerVault.Models
{
    public class ServerOperation
    {
        public static void ConnectToServer(ServerInfo config)
        {
            try
            {
                // Validate key format (should be OpenSSH format, not PPK)
                if (!config.SshPath.EndsWith(".pem") && !config.SshPath.EndsWith(".key"))
                {
                    Console.WriteLine("Warning: Key file should be in OpenSSH format (.pem or .key)");
                }

                // Build SSH command arguments
                string sshArgs = $"-i \"{config.SshPath}\" {config.Username}@{config.IP} -p {config.Port}";

                // Configure process start
                var processInfo = new ProcessStartInfo
                {
                    FileName = "ssh", // Assumes OpenSSH client is in PATH
                    Arguments = sshArgs,
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                Console.WriteLine($"Connecting to {config.Name} ({config.IP})...");

                // Start the process
                using (var process = Process.Start(processInfo))
                {
                    //process.WaitForExit();
                    //Console.WriteLine($"Disconnected from {config.Name}");

                    Console.WriteLine("Server open in new window");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Ensure:");
                Console.WriteLine("1. OpenSSH client is installed");
                Console.WriteLine("2. Private key is in OpenSSH format (.pem)");
                Console.WriteLine("3. Key permissions are properly set (chmod 600)");
            }
        }
    }
}
