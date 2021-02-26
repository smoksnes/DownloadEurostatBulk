using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace DownloadEverything
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting!");
            string uri =
                @"https://ec.europa.eu/eurostat/estat-navtree-portlet-prod/BulkDownloadListing?sort=1&dir=comext%2FCOMEXT_DATA%2FPRODUCTS";
            string pathForDownload = @"C:\Temp\Hack";
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(uri);
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    // Using HtmlDecode to remove &amp from url.
                    var href = HttpUtility.HtmlDecode(link.Attributes["href"].Value);

                    // Detect if it's a file.
                    var name = HttpUtility.UrlDecode(href).Split("/").Last();
                    if (!name.Contains(".") || !link.InnerHtml.Equals("Download"))
                    {
                        Console.WriteLine($"Skipping link since it doesn't appear to be a file: {href}");
                        continue;
                    }

                    // Proceed with download.
                    Console.WriteLine($"Will download {href}.");
                    var filename = Path.Combine(pathForDownload, name);
                    var result =await DownloadFile(href);
                    File.WriteAllBytes(filename, result); // Requires System.IO

                    //client.DownloadFile(href, filename);

                    // Or you can get the file content without saving it
                    //string htmlCode = client.DownloadString("http://yoursite.com/page.html");
                }
            }
        }

        public static async Task<byte[]> DownloadFile(string url)
        {
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                }
            }
            return null;
        }
    }
}
