﻿namespace Walter
{
    public interface ICrawlingFilterDetail
    {
        string ElementId { get; }
        string AttributeName { get; }
        string AttributeContains { get; }
    }
}