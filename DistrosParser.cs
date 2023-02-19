using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace MyBotRE
{
    public static class DistrosParser
    {
        private const string Link = "https://distrowatch.com/";
        private static HttpClient httpClient = new HttpClient();
        private static HtmlParser htmlParser = new HtmlParser();

        public static List<string> GetDistrosList() => ParseHTML().Result;

        private static async Task<List<string>> ParseHTML()
        {
            string html = ReadHTML(Link).Result;
            var HTMLDocument = await htmlParser.ParseDocumentAsync(html);
            return HTMLDocument
                .QuerySelectorAll("select[name=distribution] > option")
                .Select(item => item.Text())
                .ToList();
            //distroSelector.ForEach(item => Console.WriteLine(item));
        }
        private static async Task<string> ReadHTML(string url)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
