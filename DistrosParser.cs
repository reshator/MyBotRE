using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBotRE
{
    public static class DistrosParser
    {
        private const string Link = "https://distrowatch.com/";
        private static HttpClient httpClient = new HttpClient();
        private static HtmlParser htmlParser = new HtmlParser();

        public static async void GetDistrosList()
        {
            await ParseHTML();
        }

        private static async Task ParseHTML()
        {
            string html = ReadHTML("https://distrowatch.com/dwres.php?resource=bittorrent").Result;
            var HTMLDocument = await htmlParser.ParseDocumentAsync(html);
            var distroSelector = HTMLDocument.QuerySelectorAll("select[name=distribution]").Select(item => item.Text()).ToList();
            distroSelector.ForEach(item => Console.WriteLine(item));
        }
        private static async Task<string> ReadHTML(string url)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
