using System;
using System.IO;
using System.Reflection;
using Abot.Poco;
using NSubstitute;
using NUnit.Framework;

namespace Walter.AcceptanceTests
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

        private string GetFileContent(string fileName)
        {
            if (!File.Exists(fileName))
                throw new ApplicationException("Cannot find file " + fileName);

            return File.ReadAllText(fileName);
        }

    }
}