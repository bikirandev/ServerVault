using System;

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
    }
}
