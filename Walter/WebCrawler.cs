using System;
using System.Net;
using Abot.Crawler;
using Abot.Poco;
using Walter.Statistics;

namespace Walter
{
    public class WebCrawler
    {
        private readonly ICrawlingStats _crawlingStats;
        private readonly PoliteWebCrawler _webCrawler;

        public WebCrawler(ICrawlingStats crawlingStats)
        {
            _crawlingStats = crawlingStats;
            _webCrawler = new PoliteWebCrawler();
            _webCrawler.PageCrawlCompletedAsync += ProcessPageCrawlCompleted;
        }

        private void ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            if (e.CrawledPage.HttpWebResponse != null && e.CrawledPage.HttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                _crawlingStats.ProcessCrawledPage(e.CrawledPage);
            }
        }

        public CrawlResult Crawl(Uri uri)
        {
            return _webCrawler.Crawl(uri);
        }

    }
}