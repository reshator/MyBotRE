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
        public static List<string> GetDistrosList() => ParseDistrosList().Result;
        public static async Task<Distribution> GetDistribution(string distroName)
        {
            return ParseDistroInfo(distroName).Result;
        }

        private static async Task<Distribution> ParseDistroInfo(string distroName)
        {
            var dist = new Distribution();
            string html = ReadHTML($"{Link}/table.php?distribution={distroName.ToLower()}").Result;
            var HTMLDocument = await htmlParser.ParseDocumentAsync(html);
            #region parsingInfo

#nullable disable
            dist.Name = HTMLDocument.QuerySelector("h1").Text();
            dist.LastUpdate = HTMLDocument.QuerySelector("td.TablesTitle > div:nth-child(2) > h2").Text();
            dist.BasedOn = string.Join(", ", HTMLDocument.QuerySelectorAll("td.TablesTitle > div:nth-child(2) > ul > li:nth-child(2) > a").Select(item => item.Text()));
            dist.DesktopEnvironments = string.Join(",", HTMLDocument.QuerySelectorAll("td.TablesTitle > div:nth-child(2) > ul > li:nth-child(5) > a").Select(item => item.Text()));
            dist.Categories = string.Join(", ", HTMLDocument.QuerySelectorAll("td.TablesTitle > div:nth-child(2) > ul > li:nth-child(6) > a").Select(item => item.Text()));
            dist.Architecture = string.Join(", ", HTMLDocument.QuerySelectorAll("td.TablesTitle > div:nth-child(2) > ul > li:nth-child(4) > a").Select(item => item.Text()));
            dist.Status = HTMLDocument.QuerySelector("td.TablesTitle > div:nth-child(2) > ul > li:nth-child(7) > font").Text();
            dist.Description = HTMLDocument.QuerySelector("td.TablesTitle > div:nth-child(2)").Text().Split("\n")[7];
            var a = HTMLDocument.QuerySelector("td.TablesTitle > div:nth-child(2)").Text().Split("\n\r\"");
            //var path = HTMLDocument.QuerySelector("td.TablesTitle > img").GetAttribute("src");
            //HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Link}/{path}");
            //using (FileStream stream = new FileStream($@"img/{distroName}.png", FileMode.OpenOrCreate))
            //{
            //    await httpResponse.Content.CopyToAsync(stream);
            //}
            //dist.imagePath = $@"img/{distroName}.png";
#nullable restore
            #endregion

            return dist;
        }
        private static async Task<List<string>> ParseDistrosList()
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
