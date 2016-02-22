using System;

namespace Walter.Infrastructure
{
    public class Clock : IClock
    {
        public string FormattedCurrentTime()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}