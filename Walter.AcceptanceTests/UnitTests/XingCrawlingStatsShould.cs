using System;
using System.IO;
using System.Reflection;
using Abot.Poco;
using NSubstitute;
using NUnit.Framework;
using Walter.Statistics;

namespace Walter.AcceptanceTests.UnitTests
{
    [TestFixture]
    public class XingCrawlingStatsShould
    {
        private ICrawlingFilterDetail _crawlingFilterDetail = Substitute.For<ICrawlingFilterDetail>();

        [SetUp] 
        public void Init()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
        }

        [Test]
        public void count_pages_containing_specific_keywords()
        {
            ICrawlingStats crawlingStats = new XingCrawlingStats(new[] { "jobdetail" }, _crawlingFilterDetail);
            var page = new CrawledPage(new Uri("http://a.com/jobdetail"));

            crawlingStats.ProcessCrawledPage(page);

            Assert.AreEqual(1, crawlingStats.CountOfCrawledPagesContainingSpecificKeyword);
        }

        [Test]
        public void count_pages_containing_specific_content_with_specific_filter_detail()
        {
            ICrawlingFilterDetail crawlingFilterDetail = new CrawlingFilterDetail("jobdetail-iframe", "src", "/jobdetail");
            ICrawlingStats crawlingStats = new XingCrawlingStats(new[] { "jobdetail" }, crawlingFilterDetail);
            CrawledPage page = new CrawledPage(new Uri("http://a.com/jobdetail"))
            {
                Content = new PageContent
                {
                    Text = GetFileContent("TestPages\\StellenangebotOeffnen.html")
                }
            };

            crawlingStats.ProcessCrawledPage(page);

            Assert.AreEqual(1, crawlingStats.CountOfCrawledPagesContainingSpecificDetails);
        }

        [Test]
        public void ignore_duplicated_pages()
        {
            ICrawlingStats crawlingStats = new XingCrawlingStats(new[] { "jobdetail" }, _crawlingFilterDetail);
            var page = new CrawledPage(new Uri("https://www.xn--jobbrse-d1a.com/jobdetail/?rid=101496772&qid=36120&fid=97&_uri=am9idGl0bGU9TWFya2V0aW5nJnJhZGl1cz0xMCZjb3VudHJ5PSZjYXRlZ29yeT0mYWdlbmN5PTAmY2FyZWVyPSZwYXJ0dGltZT0wJnNvcnQ9ZGF0ZSZwYWdlPTEmcnBwPTEwJmRhdGU9JnFkYXRlPTIwMTYtMDItMjImam9iaWQ9MSZ0b3RhbD0yNzI1Mw=="));

            crawlingStats.ProcessCrawledPage(page);
            crawlingStats.ProcessCrawledPage(page);

            Assert.AreEqual(1, crawlingStats.CountOfCrawledPagesContainingSpecificKeyword);
        }

        private string GetFileContent(string fileName)
        {
            if (!File.Exists(fileName))
                throw new ApplicationException("Cannot find file " + fileName);

            return File.ReadAllText(fileName);
        }

    }
}