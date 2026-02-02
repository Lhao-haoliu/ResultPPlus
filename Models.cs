using System.Collections.Generic;

namespace ResultPPlus
{
    public class StatRow
    {
        public string Type { get; set; }
        public int TrueCount { get; set; }
        public int FalseCount { get; set; }
        public int Total => TrueCount + FalseCount;
        public double SuccessRateValue => Total <= 0 ? 0 : (double)TrueCount / Total * 100;
        public string SuccessRate =>
            Total <= 0 ? "0.00%" : ((double)TrueCount / Total).ToString("P2");
    }

    public class ErrorSummaryRow
    {
        public string Item { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class IntegrationDetailResult
    {
        public List<StatRow> Rows { get; set; }
        public List<ErrorSummaryRow> ErrorSummary { get; set; }
    }
}
