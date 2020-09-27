using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Xhznl.HelloAbp.Jobs.ChinaRegion
{
    /// <summary>
    /// 爬取国家行政区域字典
    /// </summary>
    public class CrawlingChinaRegionJob : AsyncBackgroundJob<ChinaRegionArgs>, ITransientDependency
    {
        private readonly HttpClient _httpClient;
        private readonly SortedDictionary<string, string> _sheng = new SortedDictionary<string, string>();
        private readonly ChinaRegionOption _regionOption;

        public CrawlingChinaRegionJob(IHttpClientFactory factory,
            IOptionsSnapshot<ChinaRegionOption> regionOption)
        {
            _httpClient = factory.CreateClient();
            _regionOption = regionOption.Value;
        }

        public override async Task ExecuteAsync(ChinaRegionArgs args)
        {
            if (string.IsNullOrEmpty(args.Province))
            {
                return;
            }

            var path = Path.Combine(AppContext.BaseDirectory, $"{_regionOption.Year}--html");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _httpClient.BaseAddress = _regionOption.BaseGovUri;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
                "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head><body><table border='1' bordercolor='#000000' style='border-collapse:collapse'><tr><td>代码</td><td>省</td><td>市</td><td>县</td><td>镇</td><td>城乡分类</td><td>村/街道</td></tr>"
            );
            sb.AppendLine($"<tr><td></td><td>{args.Province}</td><td></td><td></td><td></td><td></td><td></td></tr>");
            var filePath = Path.Combine(path, $"{args.Province}.html");
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                FileShare.None))
            {
                Console.WriteLine("爬取:" + args.Province);
                var city = await ReadCity(args.Href);
                sb.Append(city);
                sb.AppendLine("</table></body></html>");
                var bytes = Encoding.Default.GetBytes(sb.ToString());
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// 省/直辖市/自治区
        /// </summary>
        /// <returns></returns>
        public async Task<SortedDictionary<string, string>> GetProvincesAsync()
        {
            var indexHtml = await GetPageString(_regionOption.BaseGovUri.AbsoluteUri + "index.html");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(indexHtml); // load
            var arrs = htmlDoc.DocumentNode.SelectNodes("//tr[@class='provincetr']//td//a");
            foreach (var s in arrs)
            {
                StringBuilder sb = new StringBuilder();
                var a = s.Attributes["href"].Value;
                var name = s.InnerText;
                //添加在内存中
                _sheng.TryAdd(name, a);
                Console.WriteLine(name);
            }

            return _sheng;
        }

        /// <summary>
        /// 市辖区/县(或者分区)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<StringBuilder> ReadCity(string url)
        {
            StringBuilder sb = new StringBuilder();
            var content = await GetPageString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var cityTr = htmlDoc.DocumentNode.SelectNodes("//tr[@class='citytr']");
            foreach (var tr in cityTr)
            {
                var citys = tr.ChildNodes;
                var code = citys[0].LastChild.InnerText;
                var name = citys[1].LastChild.InnerText;
                var herf = citys[1].LastChild.Attributes["href"].Value;
                sb.Append(
                    $"<tr><td>{code}</td><td></td><td>{name}</td><td></td><td></td><td></td><td></td></tr>");
                Console.WriteLine($"爬取:{name}");

                var county = await ReadCounty(herf);
                sb.Append(county);
            }

            return sb;
        }

        /// <summary>
        /// 具体的县
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<StringBuilder> ReadCounty(string url)
        {
            StringBuilder sb = new StringBuilder();
            var content = await GetPageString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            var countyTr = htmlDoc.DocumentNode.SelectNodes("//tr[@class='countytr']");
            foreach (var tr in countyTr)
            {
                var citys = tr.ChildNodes;
                var code = citys[0].LastChild.InnerText;
                var name = citys[1].LastChild.InnerText;
                var herf = citys[1].LastChild.Attributes["href"].Value;
                sb.Append(
                    $"<tr><td>{code}</td><td></td><td></td><td>{name}</td><td></td><td></td><td></td></tr>");
                Console.WriteLine($"爬取:{name}");

                if (!string.IsNullOrWhiteSpace(herf))
                {
                    //加载镇的时候url进行了分组,组装url
                    var zhen = await ReadTown(url.Substring(0, url.IndexOf('/') + 1) + herf);
                    sb.Append(zhen);
                }
            }

            return sb;
        }

        /// <summary>
        /// 乡镇,街道
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<StringBuilder> ReadTown(string url)
        {
            StringBuilder sb = new StringBuilder();
            var htmlDoc = new HtmlDocument();
            var content = await GetPageString(url);
            htmlDoc.LoadHtml(content);
            var townTr = htmlDoc.DocumentNode.SelectNodes("//tr[@class='towntr']");
            foreach (var tr in townTr)
            {
                var citys = tr.ChildNodes;
                var code = citys[0].LastChild.InnerText;
                var name = citys[1].LastChild.InnerText;
                var herf = citys[1].LastChild.Attributes["href"].Value;
                sb.Append(
                    $"<tr><td>{code}</td><td></td><td></td><td></td><td>{name}</td><td></td><td></td></tr>");
                Console.WriteLine($"爬取:{name}");

                if (!string.IsNullOrWhiteSpace(herf))
                {
                    var village = await ReadVillage(url.Substring(0, url.LastIndexOf('/') + 1) + herf);
                    sb.Append(village);
                }
            }

            return sb;
        }

        /// <summary>
        /// 村委会/社区
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<StringBuilder> ReadVillage(string url)
        {
            StringBuilder sb = new StringBuilder();
            var htmlDoc = new HtmlDocument();
            var content = await GetPageString(url);
            htmlDoc.LoadHtml(content);
            var townTr = htmlDoc.DocumentNode.SelectNodes("//tr[@class='villagetr']");
            foreach (var tr in townTr)
            {
                sb.Append(
                    $"<tr><td>{tr.ChildNodes[0].InnerText}</td><td></td><td></td><td></td><td></td><td>{GetVillageType(tr.ChildNodes[1].InnerText)}({tr.ChildNodes[1].InnerText})</td><td>{tr.ChildNodes[2].InnerText}</td></tr>");
            }

            return sb;
        }


        public int RetryTimes { get; set; } = 3;

        private async Task<string> GetPageString(string url)
        {
            var httpResponseMessage = await _httpClient.GetAsync(url);
            try
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                RoleBackTimes();
                using StreamReader reader = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync(),
                    Encoding.GetEncoding("GBK"));
                return await reader.ReadToEndAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (RetryTimes-- <= 0)
                {
                    return await GetPageString(url);
                }
            }

            return string.Empty;
        }


        private string GetVillageType(string code)
        {
            var str = "主城区";
            switch (code)
            {
                case "112":
                    str = "城乡结合区";
                    break;
                case "121":
                    str = "镇中心区";
                    break;;
                case "122":
                    str = "镇乡结合区";
                    break;
                case "123":
                    str = "特殊区域";
                    break;
                case "210":
                    str = "乡中心区";
                    break;
                case "220":
                    str = "村庄";
                    break;
            }

            return str;
        }

        private void RoleBackTimes()
        {
            RetryTimes = 3;
        }
    }
}