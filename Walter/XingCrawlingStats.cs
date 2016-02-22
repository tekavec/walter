using System.Linq;
using Abot.Poco;

namespace Walter
{
    public class XingCrawlingStats : ICrawlingStats
    {
        private readonly string[] _keywords;
        private readonly ICrawlingFilterDetail _crawlingFilterDetail;
        public int CountOfCrawledPagesContainingSpecificKeyword { get; private set; }
        public int CountOfCrawledPagesContainingSpecificDetails { get; private set; }

        public XingCrawlingStats(string[] keywords, ICrawlingFilterDetail crawlingFilterDetail)
        {
            _keywords = keywords;
            _crawlingFilterDetail = crawlingFilterDetail;
            CountOfCrawledPagesContainingSpecificKeyword = 0;
        }

        public void ProcessCrawledPage(CrawledPage crawledPage)
        {
            var uri = crawledPage.Uri.AbsoluteUri;
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