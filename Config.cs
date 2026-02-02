using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ResultPPlus
{
    public class AppConfig
    {
        public IntegrationConfig Integration { get; set; }
        public ErrorWriteBackConfig ErrorWriteBack { get; set; }

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
                },
                ErrorWriteBack = new ErrorWriteBackConfig
                {
                    SheetName = "data",
                    LayerColumn = "A",
                    TypeColumn = "B",
                    AppendSeparator = " | ",
                    ClearDataValidation = true,
                    Functions = new List<ErrorWriteBackFunction>
                    {
                        new ErrorWriteBackFunction { Name = "OCR-Layer", Range = "G-M", Result = "L" },
                        new ErrorWriteBackFunction { Name = "OCR-Name比对", Range = "N-X", Result = "W" },
                        new ErrorWriteBackFunction { Name = "公版图比对", Range = "Y-AG", Result = "AF" },
                        new ErrorWriteBackFunction { Name = "量测", Range = "AH-AP", Result = "AO" },
                        new ErrorWriteBackFunction { Name = "平滑度检测", Range = "AQ-AU", Result = "AT" }
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

    public class ErrorWriteBackConfig
    {
        public string SheetName { get; set; }
        public string LayerColumn { get; set; }
        public string TypeColumn { get; set; }
        public string AppendSeparator { get; set; }
        public bool ClearDataValidation { get; set; }
        public List<ErrorWriteBackFunction> Functions { get; set; }
    }

    public class ErrorWriteBackFunction
    {
        public string Name { get; set; }
        public string Range { get; set; }
        public string Result { get; set; }
        public string MessageTarget { get; set; }
        public List<string> Aliases { get; set; }
    }
}
