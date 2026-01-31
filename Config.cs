using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ResultPPlus
{
    public class AppConfig
    {
        public IntegrationConfig Integration { get; set; }

        public static AppConfig LoadOrDefault(string path)
        {
            try
            {
                if (!File.Exists(path)) return Default();
                var cfg = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(path));
                return cfg ?? Default();
            }
            catch
            {
                return Default();
            }
        }

        public static AppConfig Default()
        {
            return new AppConfig
            {
                Integration = new IntegrationConfig
                {
                    SheetName = "data",
                    Mappings = new Dictionary<string, string>
                    {
                        { "ocrlayer", "L" },
                        { "OCRname", "V" },
                        { "公版图比对", "AE" },
                        { "量测", "AN" },
                        { "平滑度检测", "AS" }
                    }
                }
            };
        }
    }

    public class IntegrationConfig
    {
        public string SheetName { get; set; }
        public Dictionary<string, string> Mappings { get; set; }
    }
}
