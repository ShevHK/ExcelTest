namespace ExcelTestServices.Interfaaces
{
    public interface IFileFormatter
    {
        Task<bool> FromXlsxToCsv(string file, int lastYearNeeded);
    }
}
