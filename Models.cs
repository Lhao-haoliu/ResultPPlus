namespace ResultPPlus
{
    public class StatRow
    {
        public string Type { get; set; }
        public int TrueCount { get; set; }
        public int FalseCount { get; set; }
        public int Total => TrueCount + FalseCount;
        public string SuccessRate =>
            Total <= 0 ? "0.00%" : ((double)TrueCount / Total).ToString("P2");
    }
}
