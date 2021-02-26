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

            string uri = null;
            string folder = null;

            if (args.Length == 0)
            {
                uri =
                    @"https://ec.europa.eu/eurostat/estat-navtree-portlet-prod/BulkDownloadListing?sort=1&dir=comext%2FCOMEXT_DATA%2FPRODUCTS";
                folder = @"C:\Temp\Hack2021";
            }
            else if (args.Length == 1)
            {
                uri = args[0];
                folder = @"C:\Temp\Hack2021";
            }
            else if (args.Length == 2)
            {
                uri = args[0];
                folder = args[1];
            }
            else
            {
                Console.WriteLine("Unsupported arguments. Will stop now. Press any key to continue.");
                Console.ReadKey();
                return;
            }


            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(uri);

            var nodes = doc.DocumentNode.SelectNodes("//a[@href]").Where(Filter).ToArray();
            Console.WriteLine($"Found {nodes.Length} files to download.");

            int count = 0;
            foreach (HtmlNode link in nodes)
            {
                count++;
                var href = HttpUtility.HtmlDecode(link.Attributes["href"].Value);
                var name = GetName(link);
                Console.WriteLine($"Will download file {name} {href}.");
                var filename = Path.Combine(folder, name);
                var result = await DownloadFile(href);
                File.WriteAllBytes(filename, result);
                Console.WriteLine($"Finished downloading file {count}/{nodes.Length}.");
            }
            Console.WriteLine($"Finished downloading {nodes.Length} files. Press any key to continue.");
            Console.ReadKey();
        }

        private static bool Filter(HtmlNode link)
        {
            var name = GetName(link);
            return name.Contains(".") && link.InnerHtml.Equals("Download");
        }

        private static string GetName(HtmlNode link)
        {
            var href = HttpUtility.HtmlDecode(link.Attributes["href"].Value);
            var name = HttpUtility.UrlDecode(href).Split("/").Last();
            return name;
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
