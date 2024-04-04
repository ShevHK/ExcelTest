using ExcelTestServices.Interfaaces;

namespace ExcelTestServices.Services
{
    public class FileDownloader : IFileDownloader
    {
        private const string directory = "../../../data/";
        private readonly HttpClient _httpClient;

        public FileDownloader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool isFileLoaded, string filePath)> DownloadFileFromURL(string url)
        {
            var isFileLoaded = false;
            string filePath = "";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        var fileName = response.Content.Headers.ContentDisposition.FileName.Trim('"');
                        filePath = directory + fileName;
                        using (FileStream fileStream = File.Create(filePath))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                        Console.WriteLine("File successfully saved.");
                        isFileLoaded = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            return (isFileLoaded, filePath);
        }
    }
}