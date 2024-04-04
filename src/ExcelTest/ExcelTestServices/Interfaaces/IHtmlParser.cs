namespace ExcelTestServices.Interfaaces
{
    public interface IHtmlParser
    {
        Task<string> GetUrlByFileName(string url, string fileName);
    }
}
