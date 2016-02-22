using Abot.Poco;

namespace Walter.Statistics
{
    public interface ICrawlingStats
    {
        int CountOfCrawledPagesContainingSpecificKeyword { get; }
        int CountOfCrawledPagesContainingSpecificDetails { get; }
        void ProcessCrawledPage(CrawledPage crawledPage);
    }
}