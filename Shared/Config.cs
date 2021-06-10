using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace F12020.Backend.Shared
{
    class Config
    {
        public IPSettings IPSettings { get; set; }

        public static async Task<Config> LoadAsync()
        {
            string configpath = Path.Combine(Config.GetAssemblyPath(), "config.json");
            Console.WriteLine(configpath);

            FileStream fs = File.Open(configpath, FileMode.Open);

            StreamReader sr = new StreamReader(fs);
            string filecontents = await sr.ReadToEndAsync();
            fs.Dispose();

            try
            {
                return JsonConvert.DeserializeObject<Config>(filecontents);
            }
            catch
            {
                return null;
            }

        }

        public static Config Load()
        {
            string configpath = Path.Combine(Config.GetAssemblyPath(), "config.json");
            Console.WriteLine(configpath);

            FileStream fs = File.Open(configpath, FileMode.Open);

            StreamReader sr = new StreamReader(fs);
            string filecontents = sr.ReadToEnd();
            fs.Dispose();

            try
            {
                return JsonConvert.DeserializeObject<Config>(filecontents);
            }
            catch
            {
                return null;
            }

        }

        private static string GetAssemblyPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }

    class IPSettings
    {
        public string ListenIPv4 { get; set; }
        public string ListenIPv6 { get; set; }
        public int ListenPort { get; set; }

    }
}