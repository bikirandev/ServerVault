using System;
using Bikiran.Validation;
using Spectre.Console;

namespace ServerVault.Models
{
    public class ServerInfo
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IP { get; set; } = string.Empty;

        public int Port { get; set; } = 22; // Default SSH port

        public string Username { get; set; } = string.Empty;

        public bool IsPasswordAuth { get; set; } = true;

        public string SshPath { get; set; } = string.Empty;

        public async Task UpdateName()
        {
            // Delay
            await Task.Delay(1);

            this.Name = AnsiConsole.Prompt(new TextPrompt<string>("Your server Name/Hostname?").DefaultValue("Live Server"));
        }

        public async Task UpdateIP()
        {
            // Delay
            await Task.Delay(1);

            var ip = AnsiConsole.Prompt(new TextPrompt<string>("Your server IP?"));

            // Validate IP
            var ipValidate = ValIP.IsValidIpFormat(ip, "IP");
            if (ipValidate.Error)
            {
                AnsiConsole.Markup($"[red]({ip}) {ipValidate.Message}[/]\n");
                await UpdateIP();
            }

            this.IP = ip;
        }

        public async Task UpdatePort()
        {
            // Delay
            await Task.Delay(1);

            var port = AnsiConsole.Prompt(new TextPrompt<int>("Your server SSH Port?").DefaultValue(22));

            // Port Range
            var minPort = 1;
            var maxPort = 49151;

            if (port < minPort || port > maxPort)
            {
                AnsiConsole.Markup($"[red]Port {port} is not valid. Valid port range is {minPort} to {maxPort}[/]\n");
                await UpdatePort();
            }

            this.Port = port;
        }

        public async Task UpdateUsername()
        {
            // Delay
            await Task.Delay(1);

            var serverUsername = AnsiConsole.Prompt(new TextPrompt<string>("Your server Username?"));

            // Validate Username
            var validate = ValUser.IsValidUserNameFormat(serverUsername, "Username", 2, 20);
            if (validate.Error)
            {
                AnsiConsole.Markup($"[red]{validate.Message}[/]\n");
                await UpdateUsername();
            }

            this.Username = serverUsername;
        }

        public async Task UpdateAuthType()
        {
            // Delay
            await Task.Delay(1);

            var isPasswordAuth = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Authenticate by [green](SSH Recommended)[/]?")
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices(["SSH", "Password"])
            );

            AnsiConsole.Markup($"Authenticate by: {isPasswordAuth}\n");

            this.IsPasswordAuth = isPasswordAuth == "Password";
        }

        public async Task UpdatePath()
        {
            // Delay
            await Task.Delay(1);

            var sshPath = AnsiConsole.Prompt(new TextPrompt<string>("SSH Public Key Path?"));

            // Validate
            //var validate = ValPath.IsValidPathPattern(sshPath, "SSH Public Key Path");
            //if (validate.Error)
            //{
            //    AnsiConsole.Markup($"[red]{validate.Message}[/]\n");
            //    await UpdatePath();
            //}

            this.SshPath = sshPath;
        }
    }
}
