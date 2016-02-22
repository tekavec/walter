using System;
using NSubstitute;
using NUnit.Framework;
using Walter.FileIO;
using Walter.Infrastructure;
using Walter.Statistics;

namespace Walter.AcceptanceTests.EndToEndTests
{
    [TestFixture]
    public class WebCrawlerShould
    {
        private const string ResultFilePath = "WalterResult.csv";
        private readonly ICrawlingStats _crawlingStats = Substitute.For<ICrawlingStats>();
        private readonly IResultWriter _resultWriter = Substitute.For<IResultWriter>();
        private readonly IClock _clock = Substitute.For<IClock>();
        private WebCrawler _crawler;
        private const string ArbitraryFormattedCurrentTime = "now";

        [SetUp]
        public void Init()
        {
            _clock.FormattedCurrentTime().Returns(ArbitraryFormattedCurrentTime);
            _clock.FormattedCurrentTime().Returns(ArbitraryFormattedCurrentTime);
            _crawler = new WebCrawler(_crawlingStats, _resultWriter, _clock);
        }

        [Test]
        public void fire_page_completed_event_after_crawling_starts()
        {

            _crawler.Crawl(new Uri("https://www.xn--jobbrse-d1a.com/list/jobtitle/"), ResultFilePath);

            _crawlingStats.ReceivedWithAnyArgs().ProcessCrawledPage(null);
        }

        [Test]
        public void not_fire_page_completed_event_after_crawling_starts_and_url_does_not_responds()
        {
            _crawler.Crawl(new Uri("http://localhost123:456/"), ResultFilePath);

            _crawlingStats.DidNotReceiveWithAnyArgs().ProcessCrawledPage(null);
        }

        [Test]
        public void save_results_to_csv_file()
        {
            _crawler.Crawl(new Uri("https://www.xn--jobbrse-d1a.com/list/jobtitle/"), ResultFilePath);

            _resultWriter.Received().SaveFile(ResultFilePath, new GeneralCrawlingData(ArbitraryFormattedCurrentTime, ArbitraryFormattedCurrentTime));
        }

    }
}