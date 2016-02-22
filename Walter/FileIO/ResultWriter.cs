using System.IO;
using System.Text;
using Walter.Statistics;

namespace Walter.FileIO
{
    public class ResultWriter : IResultWriter
    {
        private readonly ICrawlingStats _crawlingStats;

        public ResultWriter(ICrawlingStats crawlingStats)
        {
            _crawlingStats = crawlingStats;
        }

        public void SaveFile(string filePath, GeneralCrawlingData generalCrawlingData)
        {
            File.WriteAllText(filePath, CreateFileContent(generalCrawlingData));
        }

        private string CreateFileContent(GeneralCrawlingData generalCrawlingData)
        {
            var csv = new StringBuilder();
            csv.AppendLine($"All jobs,{_crawlingStats.CountOfCrawledPagesContainingSpecificKeyword}")
                .AppendLine($"Xing jobs,{_crawlingStats.CountOfCrawledPagesContainingSpecificDetails}")
                .AppendLine($"Start crawling at,{generalCrawlingData.StartTime}")
                .AppendLine($"End crawling at,{generalCrawlingData.EndTime}");
            return csv.ToString();
        }
    }
}