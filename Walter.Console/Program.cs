using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.FileIO;
using Walter.Infrastructure;
using Walter.Statistics;

namespace Walter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ICrawlingFilterDetail crawlingFilterDetail = new CrawlingFilterDetail("jobdetail-iframe", "src", "/jobdetail");
            ICrawlingStats crawlingStats = new XingCrawlingStats(new[] { "jobdetail" }, crawlingFilterDetail); 
            IResultWriter resultWriter = new ResultWriter(crawlingStats);
            var walter = new WebCrawler(crawlingStats, resultWriter, new Clock());
            var result = walter.Crawl(new Uri("https://www.xn--jobbrse-d1a.com/list/jobtitle/"), @"c:\temp\WalterResult.csv");
        }
    }
}
