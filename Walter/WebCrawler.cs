using System;
using System.Net;
using Abot.Crawler;
using Abot.Poco;

namespace Walter
{
    public class WebCrawler
    {
        private readonly PoliteWebCrawler _webCrawler;
        public event EventHandler RaisePageCrawlCompletedAsync;

        public WebCrawler()
        {
            _webCrawler = new PoliteWebCrawler();
            _webCrawler.PageCrawlCompletedAsync += ProcessPageCrawlCompleted;
        }

        private void ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            if (e.CrawledPage.HttpWebResponse != null && e.CrawledPage.HttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                RaisePageCrawlCompletedAsync?.Invoke(sender, e);
                var uri = e.CrawledPage.Uri;
            }
        }

        public CrawlResult Crawl(Uri uri)
        {
            return _webCrawler.Crawl(uri);
        }

    }
}