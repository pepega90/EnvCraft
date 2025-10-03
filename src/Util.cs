using EnvCraft.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvCraft.src
{
    /// <summary>
    /// Util, berisikan static method untuk mempermudah contohnya saat load env file yang kita pilih atau pun save
    /// </summary>
    public static class Util
    {
        public static void LoadEnvFile(string path, List<Item> listItem)
        {
            listItem.Clear();
            if (!File.Exists(path))
                return;

            foreach (var line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split("=", 2);
                if (parts.Length == 2)
                {
                    listItem.Add(new Item
                    {
                        Key = parts[0].Trim(),
                        Value = parts[1].Trim()
                    });
                }
            }
        }

        public static void SaveEnvFile(string path, List<Item> listItem)
        {
            using (var file = new StreamWriter(path))
            {
                foreach (var item in listItem)
                {
                    file.WriteLine($"{item.Key}={item.Value}");
                }
            }
        }
    }
}
