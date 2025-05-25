using System;
using NuGet.Protocol;
using ServerVault.Control;
using ServerVault.Models;
using Spectre.Console;

namespace ServerVault
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var servers = await ServerData.ReadServerData();
            var serverTitles = servers.Select(s => $"{s.Name} ({s.IP})").ToList();
            serverTitles.Add("Add");

            // Ask for the user's favorite fruit
            var fruit = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What's your [green]favorite fruit[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(serverTitles));

            if(fruit == "Add")
            {
                await ServerLibControl.AddServer();
                await Main(args);
                return;
            }

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