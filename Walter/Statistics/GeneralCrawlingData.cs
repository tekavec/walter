namespace Walter.Statistics
{
    public struct GeneralCrawlingData
    {
        public GeneralCrawlingData(string startTime, string endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public string StartTime { get; }

        public string EndTime { get; }


    }
}