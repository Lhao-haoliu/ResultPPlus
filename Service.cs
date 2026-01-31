using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using ClosedXML.Excel;

namespace ResultPPlus
{
    public static class StatServices
    {
        // =============================
        // 公共：TRUE/FALSE 解析（空忽略）
        // =============================
        private static bool TryParseBool(object v, out bool b)
        {
            b = false;
            if (v == null) return false;

            if (v is bool bb)
            {
                b = bb;
                return true;
            }

            var s = v.ToString();
            if (string.IsNullOrWhiteSpace(s)) return false;
            s = s.Trim();

            if (string.Equals(s, "TRUE", StringComparison.OrdinalIgnoreCase)) { b = true; return true; }
            if (string.Equals(s, "FALSE", StringComparison.OrdinalIgnoreCase)) { b = false; return true; }

            return false;
        }
        private static bool TryParseOkNgOrBool(object v, out bool isOk)
        {
            isOk = false;
            if (v == null) return false;

            // 先兼容 bool / TRUE/FALSE
            if (TryParseBool(v, out bool b))
            {
                isOk = b;
                return true;
            }

            var s = v.ToString();
            if (string.IsNullOrWhiteSpace(s)) return false;
            s = s.Trim();

            if (string.Equals(s, "OK", StringComparison.OrdinalIgnoreCase))
            {
                isOk = true;
                return true;
            }
            if (string.Equals(s, "NG", StringComparison.OrdinalIgnoreCase))
            {
                isOk = false;
                return true;
            }

            return false; // 其他内容忽略
        }

        private static int ColLetterToNumber(string letters)
        {
            if (string.IsNullOrWhiteSpace(letters)) return -1;
            letters = letters.Trim().ToUpperInvariant();

            int sum = 0;
            foreach (char c in letters)
            {
                if (c < 'A' || c > 'Z') return -1;
                sum = sum * 26 + (c - 'A' + 1);
            }
            return sum;
        }

        // =============================
        // 黄光类：KLA/AIM/Nikon/Canon 全表 + Summary 指定列
        // 自动容错：遇到 malformed URI 会复制临时文件修复 .rels 再打开
        // =============================
        public static List<StatRow> CalcLitho(string xlsxPath)
        {
            var rows = new List<StatRow>();
            string tempFixed = null;

            try
            {
                XLWorkbook wb = null;

                try
                {
                    wb = new XLWorkbook(xlsxPath);
                }
                catch (Exception ex) when (IsMalformedUri(ex))
                {
                    // 不动原文件：复制临时副本并修复rels
                    tempFixed = CreateFixedXlsxCopy(xlsxPath);
                    wb = new XLWorkbook(tempFixed);
                }

                using (wb)
                {
                    foreach (var name in new[] { "KLA", "AIM", "Nikon", "Canon" })
                        rows.Add(CountWholeSheet(wb, name));

                    var summary = CountSummaryColumns(wb, "Summary");
                    rows.Add(summary["Barcode"]);
                    rows.Add(summary["Version Code"]);
                    rows.Add(summary["LIPS"]);
                }

                rows.Add(MakeTotal(rows, "总计"));
                return rows;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(tempFixed))
                {
                    try { File.Delete(tempFixed); } catch { /* ignore */ }
                }
            }
        }

        private static StatRow CountWholeSheet(XLWorkbook wb, string sheetName)
        {
            var stat = new StatRow { Type = sheetName, TrueCount = 0, FalseCount = 0 };

            var ws = wb.Worksheets.FirstOrDefault(s => string.Equals(s.Name, sheetName, StringComparison.OrdinalIgnoreCase));
            if (ws == null) return stat;

            var range = ws.RangeUsed();
            if (range == null) return stat;

            foreach (var cell in range.Cells())
            {
                if (cell.IsEmpty()) continue;
                if (TryParseBool(cell.Value, out bool b))
                {
                    if (b) stat.TrueCount++;
                    else stat.FalseCount++;
                }
            }

            return stat;
        }

        private static Dictionary<string, StatRow> CountSummaryColumns(XLWorkbook wb, string sheetName)
        {
            var dict = new Dictionary<string, StatRow>(StringComparer.OrdinalIgnoreCase)
            {
                { "Barcode", new StatRow { Type = "Barcode" } },
                { "Version Code", new StatRow { Type = "Version Code" } },
                { "LIPS", new StatRow { Type = "LIPS" } }
            };

            var ws = wb.Worksheets.FirstOrDefault(s => string.Equals(s.Name, sheetName, StringComparison.OrdinalIgnoreCase));
            if (ws == null) return dict;

            string Normalize(string s)
            {
                if (s == null) return "";
                return new string(s.Trim().ToLowerInvariant().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
            }

            // 目标列：忽略大小写 + 去空格
            var want = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { Normalize("Barcode"), "Barcode" },
                { Normalize("version code"), "Version Code" },
                { Normalize("LIPS"), "LIPS" }
            };

            // 第一行表头 -> 列号
            var headerCells = ws.Row(1).CellsUsed().ToList();
            var colMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var c in headerCells)
            {
                var key = Normalize(c.GetString());
                if (want.ContainsKey(key))
                    colMap[key] = c.Address.ColumnNumber;
            }

            int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

            // 从第2行开始统计
            for (int r = 2; r <= lastRow; r++)
            {
                foreach (var kv in want)
                {
                    if (!colMap.TryGetValue(kv.Key, out int col)) continue;

                    var cell = ws.Cell(r, col);
                    if (cell.IsEmpty()) continue;

                    if (TryParseOkNgOrBool(cell.Value, out bool isOk))
                    {
                        if (isOk) dict[kv.Value].TrueCount++;
                        else dict[kv.Value].FalseCount++;
                    }

                }
            }

            return dict;
        }

        // =============================
        // 整合类：data sheet + config 映射列
        // =============================
        public static List<StatRow> CalcIntegration(string xlsxPath, AppConfig cfg)
        {
            cfg = cfg ?? AppConfig.Default();
            if (cfg.Integration == null) cfg.Integration = AppConfig.Default().Integration;
            if (cfg.Integration.Mappings == null) cfg.Integration.Mappings = AppConfig.Default().Integration.Mappings;

            var rows = new List<StatRow>();

            using (var wb = new XLWorkbook(xlsxPath))
            {
                string sheetName = string.IsNullOrWhiteSpace(cfg.Integration.SheetName) ? "data" : cfg.Integration.SheetName;
                var ws = wb.Worksheets.FirstOrDefault(s => string.Equals(s.Name, sheetName, StringComparison.OrdinalIgnoreCase));

                // sheet 不存在：全 0
                if (ws == null)
                {
                    foreach (var kv in cfg.Integration.Mappings)
                        rows.Add(new StatRow { Type = kv.Key, TrueCount = 0, FalseCount = 0 });

                    rows.Add(MakeTotal(rows, "总计"));
                    return rows;
                }

                int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

                foreach (var kv in cfg.Integration.Mappings)
                {
                    var stat = new StatRow { Type = kv.Key, TrueCount = 0, FalseCount = 0 };

                    int colNum = ColLetterToNumber(kv.Value);
                    if (colNum <= 0)
                    {
                        rows.Add(stat);
                        continue;
                    }

                    for (int r = 1; r <= lastRow; r++)
                    {
                        var cell = ws.Cell(r, colNum);
                        if (cell.IsEmpty()) continue;

                        if (TryParseBool(cell.Value, out bool b))
                        {
                            if (b) stat.TrueCount++;
                            else stat.FalseCount++;
                        }
                    }

                    rows.Add(stat);
                }
            }

            rows.Add(MakeTotal(rows, "总计"));
            return rows;
        }

        private static StatRow MakeTotal(IEnumerable<StatRow> items, string name)
        {
            int t = 0, f = 0;
            foreach (var it in items)
            {
                t += it.TrueCount;
                f += it.FalseCount;
            }
            return new StatRow { Type = name, TrueCount = t, FalseCount = f };
        }

        // =============================
        // 关键：malformed URI 容错修复（不改原文件）
        // =============================
        private static bool IsMalformedUri(Exception ex)
        {
            var msg = (ex.Message ?? "").ToLowerInvariant();
            return msg.Contains("malformed uri")
                   || msg.Contains("relationshiperrorrewriter")
                   || msg.Contains("invalid uri")
                   || msg.Contains("uri format");
        }

        /// <summary>
        /// 复制一份临时 xlsx，并修复所有 .rels 里的坏 Target，保证 OpenXML 能打开。
        /// 原文件不动。修复后的临时文件路径返回。
        /// </summary>
        private static string CreateFixedXlsxCopy(string originalPath)
        {
            var temp = Path.Combine(Path.GetTempPath(), "fixed_" + Guid.NewGuid().ToString("N") + ".xlsx");
            File.Copy(originalPath, temp, true);

            using (var zip = ZipFile.Open(temp, ZipArchiveMode.Update))
            {
                var relEntries = zip.Entries
                    .Where(e => e.FullName.EndsWith(".rels", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var entry in relEntries)
                {
                    string xml;
                    using (var sr = new StreamReader(entry.Open()))
                        xml = sr.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(xml)) continue;

                    XDocument doc;
                    try { doc = XDocument.Parse(xml); }
                    catch { continue; }

                    XNamespace ns = "http://schemas.openxmlformats.org/package/2006/relationships";
                    var rels = doc.Descendants(ns + "Relationship").ToList();
                    if (rels.Count == 0) continue;

                    bool changed = false;

                    foreach (var rel in rels)
                    {
                        var attr = rel.Attribute("Target");
                        if (attr == null) continue;

                        var target = attr.Value ?? "";
                        if (string.IsNullOrWhiteSpace(target)) continue;

                        // 相对/绝对 URI 都允许
                        if (Uri.TryCreate(target, UriKind.RelativeOrAbsolute, out _))
                            continue;

                        // 1) 先做 Escape（最常见可修复）
                        var fixed1 = Uri.EscapeUriString(target);

                        // 2) 如果还不行，替换空格（最常见脏数据）
                        if (!Uri.TryCreate(fixed1, UriKind.RelativeOrAbsolute, out _))
                            fixed1 = target.Replace(" ", "%20");

                        // 3) 仍不行：用安全占位，保证包能打开
                        if (!Uri.TryCreate(fixed1, UriKind.RelativeOrAbsolute, out _))
                            fixed1 = "about:blank";

                        attr.Value = fixed1;
                        changed = true;
                    }

                    if (!changed) continue;

                    using (var ws = entry.Open())
                    {
                        ws.SetLength(0);
                        using (var sw = new StreamWriter(ws))
                            sw.Write(doc.ToString(System.Xml.Linq.SaveOptions.DisableFormatting));

                    }
                }
            }

            return temp;
        }
    }
}
