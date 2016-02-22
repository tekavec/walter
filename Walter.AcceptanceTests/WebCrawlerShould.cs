using System;
using NSubstitute;
using NUnit.Framework;

namespace Walter.AcceptanceTests
{
    [TestFixture]
    public class WebCrawlerShould
    {

        [Test]
        public void fire_page_completed_event_after_crawling_starts()
        {
            ICrawlingStats crawlingStats = Substitute.For<ICrawlingStats>();
            var crawler = new WebCrawler(crawlingStats);

            crawler.Crawl(new Uri("https://www.xn--jobbrse-d1a.com/list/jobtitle/"));

            crawlingStats.ReceivedWithAnyArgs().ProcessCrawledPage(null);
        }

        [Test]
        public void not_fire_page_completed_event_after_crawling_starts_and_url_does_not_responds()
        {
            ICrawlingStats crawlingStats = Substitute.For<ICrawlingStats>();
            var crawler = new WebCrawler(crawlingStats);

            crawler.Crawl(new Uri("http://localhost123:456/"));

            crawlingStats.DidNotReceiveWithAnyArgs().ProcessCrawledPage(null);
        }

    }
}