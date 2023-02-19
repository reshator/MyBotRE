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

        public static void GetDistrosList()
        {
            ParseHTML();
        }

        private static async Task ParseHTML()
        {
            var HTMLDocument = await htmlParser.ParseDocumentAsync(ReadHTML(Link).Result);
            var distroSelector = HTMLDocument.QuerySelectorAll("select > option").Select(item => item.Text()).ToList();
            //await Task.Run(() => Console.WriteLine());
            distroSelector.ForEach(item => Console.WriteLine(item));

        }
        private static async Task<string> ReadHTML(string url)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
