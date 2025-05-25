using System;
using System.Reflection;
using Newtonsoft.Json;
using NuGet.Protocol;
using ServerVault.Models;
using Spectre.Console;

namespace ServerVault
{
    public class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BikiranConsoleAPP.Data.servers.json";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream ?? throw new FileNotFoundException("Data/servers.json not found in resources"));
            var json = reader.ReadToEnd();

            var servers = JsonConvert.DeserializeObject<List<ServerInfo>>(json) ?? [];
            var serverTitles = servers.Select(s => $"{s.Name} ({s.IP})").ToList();

            // Ask for the user's favorite fruit
            var fruit = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What's your [green]favorite fruit[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(serverTitles));

            // Find Index
            var index = serverTitles.IndexOf(fruit);

            // Get the corresponding server info
            var serverInfo = servers[index];

            // Echo the fruit back to the terminal
            AnsiConsole.WriteLine($"I agree. {serverInfo.ToJson()} is tasty!");

            // Connect to server
            ServerOperation.ConnectToServer(serverInfo);
        }
    }
}