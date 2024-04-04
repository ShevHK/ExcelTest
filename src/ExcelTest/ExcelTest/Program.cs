using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ExcelTestServices.Interfaaces;
using ExcelTestServices.Services;
using HtmlAgilityPack;

const string baseUrl = "https://bakerhughesrigcount.gcs-web.com/";
int lastYearNeeded = DateTime.Now.Year - 2;


var client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "C# HttpClient");

HtmlWeb web = new HtmlWeb();
web.UserAgent = "C# HttpClient";


IHtmlParser parser = new HtmlParser(web);
IFileDownloader fileDownloader = new FileDownloader(client);
IFileFormatter formatter = new FileFormatter();

var link = await parser.GetUrlByFileName(baseUrl + "intl-rig-count?c=79687&p=irol-rigcountsintl", "Worldwide Rig Counts - Current & Historical Data");

var (isLoaded, filePath) = await fileDownloader.DownloadFileFromURL(baseUrl + link);
if (isLoaded)
    await formatter.FromXlsxToCsv(filePath, lastYearNeeded);