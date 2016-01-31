using System;
using NUnit.Framework;

namespace Walter.AcceptanceTests
{
    [TestFixture]
    public class WebCrawlerShould
    {

        [Test]
        public void fire_page_completed_event_after_crawling_starts()
        {
            var crawler = new WebCrawler();
            var wasCalled = false;
            crawler.RaisePageCrawlCompletedAsync += (o, e) => wasCalled = true;

            crawler.Crawl(new Uri("https://www.xn--jobbrse-d1a.com/list/jobtitle/"));

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void not_fire_page_completed_event_after_crawling_starts_and_url_does_not_responds()
        {
            var crawler = new WebCrawler();
            var wasCalled = false;
            crawler.RaisePageCrawlCompletedAsync += (o, e) => wasCalled = true;

            crawler.Crawl(new Uri("http://localhost123:456/"));

            Assert.IsFalse(wasCalled);
        }

    }
}
