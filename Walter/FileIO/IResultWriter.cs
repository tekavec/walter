using Walter.Statistics;

namespace Walter.FileIO
{
    public interface IResultWriter
    {
        void SaveFile(string resultFilePath, GeneralCrawlingData generalCrawlingData);
    }
}