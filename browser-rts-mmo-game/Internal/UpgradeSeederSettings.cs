namespace BrowserGame.Internal
{
    internal class UpgradeSeederSettings
    {
        public string BuildingName { get; set; }
        public int Clay { get; set; } = 50;
        public int Iron { get; set; } = 50;
        public int Wood { get; set; } = 50;
        public int Crop { get; set; } = 25;

        /// <summary>
        /// In seconds.
        /// </summary>
        public int BuildTime { get; set; } = 600;

        public int FinalLevel { get; set; } = 10;
        public decimal Value { get; set; } = 5.0M;
        public decimal ValueScaling { get; set; } = 1.6M;
        public decimal ValueScalingDelta { get; set; } = 0.95M;
        public decimal ValueFixedChange { get; set; } = 0.02M;
        public bool UseFixedValue { get; set; } = false;
        public decimal TimeScaling { get; set; } = 2.0M;
        public decimal TimeScalingDelta { get; set; } = 0.95M;
        public decimal CostScaling { get; set; } = 1.6M;
        public decimal CostScalingDelta { get; set; } = 1.05M;
    }
}
