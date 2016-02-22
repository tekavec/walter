using System;
using System.Net;
using Abot.Crawler;
using Abot.Poco;
using Walter.FileIO;
using Walter.Infrastructure;
using Walter.Statistics;

namespace Walter
{
    public class WebCrawler
    {
        private readonly ICrawlingStats _crawlingStats;
        private readonly IResultWriter _resultWriter;
        private readonly IClock _clock;
        private readonly PoliteWebCrawler _webCrawler;
        private readonly string _startCrawlingTime;

        public WebCrawler(ICrawlingStats crawlingStats, IResultWriter resultWriter, IClock clock)
        {
            _crawlingStats = crawlingStats;
            _resultWriter = resultWriter;
            _clock = clock;
            _webCrawler = new PoliteWebCrawler();
            _webCrawler.PageCrawlCompletedAsync += ProcessPageCrawlCompleted;
            _startCrawlingTime = _clock.FormattedCurrentTime();
            //_resultFilePath = System.Configuration.ConfigurationManager.AppSettings["ResultFileName"];
        }

        private void ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            if (e.CrawledPage.HttpWebResponse != null && e.CrawledPage.HttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                _crawlingStats.ProcessCrawledPage(e.CrawledPage);
            }
        }

        public CrawlResult Crawl(Uri uri, string resultFilePath)
        {
            var crawlResult = _webCrawler.Crawl(uri);
            var formattedCurrentTime = _clock.FormattedCurrentTime();
            _resultWriter.SaveFile(resultFilePath, new GeneralCrawlingData(_startCrawlingTime, formattedCurrentTime));
            return crawlResult;
        }
    }
}