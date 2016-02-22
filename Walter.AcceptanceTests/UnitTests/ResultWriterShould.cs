using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using Walter.FileIO;
using Walter.Statistics;

namespace Walter.AcceptanceTests.UnitTests
{
    [TestFixture]
    public class ResultWriterShould
    {
        private const string ResultFilePath = @"c:\temp\WalterResult.csv";
        private readonly ICrawlingStats _crawlingStats = Substitute.For<ICrawlingStats>();
        private IResultWriter _resultWriter;
        private readonly int _allPages = new Random().Next(1, 100);
        private readonly int _xingPages = new Random().Next(1,10);

        [SetUp]
        public void Init()
        {
            _resultWriter = new ResultWriter(_crawlingStats);
        }


        [Test]
        public void save_results_to_csv_file()
        {
            _crawlingStats.CountOfCrawledPagesContainingSpecificKeyword.Returns(_allPages);
            _crawlingStats.CountOfCrawledPagesContainingSpecificDetails.Returns(_xingPages);

            _resultWriter.SaveFile(ResultFilePath, new GeneralCrawlingData(DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongTimeString()));

            Assert.IsTrue(File.Exists(ResultFilePath));
        }

        [TearDown]
        public void delete_result_file()
        {
            File.Delete(ResultFilePath);
        }

    }
}