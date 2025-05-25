using System;
using ServerVault.Models;

namespace ServerVault.Control
{
    public class ServerLibControl
    {
        public static async Task AddServer()
        {
            var data = new ServerInfo();

            // Update Name
            await data.UpdateName();

            // Update IP
            await data.UpdateIP();

            // Update Port
            await data.UpdatePort();

            // Update Username
            await data.UpdateUsername();

            // Update Auth Type
            await data.UpdateAuthType();

            // Update Path
            await data.UpdatePath();

            // Write
            await ServerData.AddServerData(data);

            // Console Log
            Console.WriteLine("The Server configuration file updated");
        }
    }
}
