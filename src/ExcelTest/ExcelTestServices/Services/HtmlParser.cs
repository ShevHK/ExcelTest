using ExcelTestServices.Interfaaces;
using HtmlAgilityPack;
using System.Text;

namespace ExcelTestServices.Services
{
    public class HtmlParser : IHtmlParser
    {
        private readonly HtmlWeb _web;
        public HtmlParser(HtmlWeb web)
        {
            _web = web;
        }

        public async Task<string> GetUrlByFileName(string url, string fileName)
        {
            var result = new StringBuilder();
            try
            {

                var htmlDoc = await _web.LoadFromWebAsync(url);

                var tags = htmlDoc.DocumentNode.SelectNodes($"//table//tbody//span//a");

                if (tags != null)
                    foreach (var tag in tags)
                        if (tag.InnerText.Contains(System.Web.HttpUtility.HtmlEncode(fileName)))
                        {
                            result.AppendLine(tag.Attributes["href"].Value);
                            Console.WriteLine("Link of file founded.");
                        }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem with web site. Error message - " + ex.Message);
            }
            return result.ToString();
        }
    }
}
