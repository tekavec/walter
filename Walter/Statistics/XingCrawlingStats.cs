using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abot.Poco;

namespace Walter.Statistics
{
    public class XingCrawlingStats : ICrawlingStats
    {
        private readonly string[] _keywords;
        private readonly ICrawlingFilterDetail _crawlingFilterDetail;
        public int CountOfCrawledPagesContainingSpecificKeyword { get; private set; }
        public int CountOfCrawledPagesContainingSpecificDetails { get; private set; }
        private IDictionary<string, string> _uniquePages = new Dictionary<string, string>();

        public XingCrawlingStats(string[] keywords, ICrawlingFilterDetail crawlingFilterDetail)
        {
            _keywords = keywords;
            _crawlingFilterDetail = crawlingFilterDetail;
            CountOfCrawledPagesContainingSpecificKeyword = 0;
            CountOfCrawledPagesContainingSpecificDetails = 0;
        }

        public void ProcessCrawledPage(CrawledPage crawledPage)
        {
            var uri = crawledPage.Uri.AbsoluteUri;
            Console.WriteLine($"Url: {uri}, All: {CountOfCrawledPagesContainingSpecificKeyword}, Xing: {CountOfCrawledPagesContainingSpecificDetails}");
            var queryParts = crawledPage.Uri.Query.Replace("?", "").Split('&');
            foreach (var queryPart in queryParts)
            {
                if (queryPart.StartsWith("rid="))
                {
                    if (_uniquePages.ContainsKey(queryPart))
                    {
                        return;
                    }
                    _uniquePages.Add(queryPart, queryPart);
                }
            }
            var id = crawledPage.Uri.Fragment;
            if (!_keywords.Any(uri.Contains)) return;
            CountOfCrawledPagesContainingSpecificKeyword++;
            if (SearchForSpecificAttributeValue(crawledPage))
            {
                CountOfCrawledPagesContainingSpecificDetails++;
            }
        }

        private bool SearchForSpecificAttributeValue(CrawledPage crawledPage)
        {
            var dom = crawledPage.CsQueryDocument;
            var elementById = dom.Document.GetElementById(_crawlingFilterDetail.ElementId);
            if (elementById != null)
            {
                var attribute = elementById.GetAttribute(_crawlingFilterDetail.AttributeName);
                return !string.IsNullOrEmpty(attribute) && attribute.ToLower().Contains(_crawlingFilterDetail.AttributeContains.ToLower());
            }
            return false;
        }
    }
}