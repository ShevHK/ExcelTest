namespace ExcelTestServices.Interfaaces
{
    public interface IFileDownloader
    {
        Task<(bool isFileLoaded,string filePath)> DownloadFileFromURL(string url);
    }
}
