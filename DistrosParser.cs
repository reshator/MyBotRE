using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using MyBotRE.Models;

namespace MyBotRE
{
    public static class DistrosParser
    {
        private const string Link = "https://distrowatch.com/";
        private static HttpClient httpClient = new HttpClient();
        private static HtmlParser htmlParser = new HtmlParser();
        public static List<string> GetDistrosList() => ParseHTMLForDistrosList().Result;
        public static async Task<Distribution> GetDistribution(string distroName)
        {
            return ParseHTMLForDistroInfo(distroName).Result;
        }
        private static async Task<Distribution> ParseHTMLForDistroInfo(string distroName)
        {
            var dist = new Distribution();
            string html = ReadHTML($"{Link}/table.php?distribution={distroName.ToLower()}").Result;
            var HTMLDocument = await htmlParser.ParseDocumentAsync(html);
            dist.Name = HTMLDocument.QuerySelector("h1")!.Text();
            dist.LastUpdate = HTMLDocument.QuerySelector("body > table:nth-child(6) > tbody > tr > td:nth-child(1) > table:nth-child(1) > tbody > tr:nth-child(2) > td > div")!.Text();
            return dist;
        }
        private static async Task<List<string>> ParseHTMLForDistrosList()
        {
            string html = ReadHTML(Link).Result;
            var HTMLDocument = await htmlParser.ParseDocumentAsync(html);
            return HTMLDocument
                .QuerySelectorAll("select[name=distribution] > option")
                .Select(item => item.Text())
                .ToList();
        }
        private static async Task<string> ReadHTML(string url)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }


    }
}
