namespace Walter.Statistics
{
    public class CrawlingFilterDetail : ICrawlingFilterDetail
    {
        public CrawlingFilterDetail(string elementId, string attributeName, string attributeContains)
        {
            ElementId = elementId;
            AttributeName = attributeName;
            AttributeContains = attributeContains;
        }

        public string ElementId { get; }

        public string AttributeName { get; }

        public string AttributeContains { get; }
    }
}