using System;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace ServerVault.Models
{
    public class ServerData
    {
        public static string _fileName = "servers.json";

        public static string GetPath()
        {
            // Get the directory where the executable is located
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine with "test.json" to get the full path
           return Path.Combine(baseDirectory, _fileName);

        }

        public static async Task<List<ServerInfo>> ReadServerData()
        {
            string jsonFilePath = GetPath();

            if (!File.Exists(jsonFilePath))
            {
                return [];
            }

            string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
            var data = JsonConvert.DeserializeObject<List<ServerInfo>>(jsonContent);
            if (data == null)
            {
                return [];
            }

            return data;
        }

        public static async Task WriteServerData(List<ServerInfo> dataList)
        {
            string jsonFilePath = GetPath();

            // Force write data
            await File.WriteAllTextAsync(jsonFilePath, dataList.ToJson());
        }

        public static async Task<List<ServerInfo>> AddServerData(ServerInfo data)
        {
            string jsonFilePath = GetPath();

            // Read Existing Data List
            var dataList = await ReadServerData();

            // Add
            dataList.Add(data);

            // Write
            await WriteServerData(dataList);

            return dataList;
        }
    }
}
