using Abot.Poco;

namespace Walter
{
    public interface ICrawlingStats
    {
        int CountOfCrawledPagesContainingSpecificKeyword { get; }
        int CountOfCrawledPagesContainingSpecificDetails { get; }
        void ProcessCrawledPage(CrawledPage crawledPage);
    }
}