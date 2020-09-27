using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        public CrawlingChinaRegionJob(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public override async Task ExecuteAsync(ChinaRegionArgs args)
        {
            _httpClient.BaseAddress = args.BaseGovUri;
            await ReadSheng("index.html", args);
        }

        private async Task ReadSheng(string url, ChinaRegionArgs args)
        {
            var indexHtml = await GetPageString(url, args);
            var path = Path.Combine(AppContext.BaseDirectory, $"{args.Year}-html");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var arrs = indexHtml.Split("<A");
            foreach (var s in arrs)
            {
                StringBuilder sb = new StringBuilder();
                if (s.IndexOf("HREF") != -1 &&
                    s.IndexOf(".HTML") != -1)
                {
                    var a = s.Substring(7, s.IndexOf("'>") - 7);
                    var start = s.IndexOf("'>") + 2;
                    var end = s.IndexOf("<BR/>") - start;
                    var name = s.Substring(start,
                        end);
                    Console.WriteLine(name);
                    sb.AppendLine(
                        "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head><body><table border='1' bordercolor='#000000' style='border-collapse:collapse'><tr><td>代码</td><td>省</td><td>市</td><td>县</td><td>镇</td><td>城乡分类</td><td>村/街道</td></tr>"
                    );
                    sb.AppendLine($"<tr><td></td><td>{name}</td><td></td><td></td><td></td><td></td><td></td></tr>");
                    var filePath = Path.Combine(path, $"{name}.html");
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                        FileShare.None))
                    {
                        Console.WriteLine("爬取:" + name);
                        var shi = await ReadShi(a, args);
                        sb.Append(shi);
                        sb.AppendLine("</table></body></html>");
                        var bytes = Encoding.Default.GetBytes(sb.ToString());
                        await fs.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        private async Task<StringBuilder> ReadShi(string url, ChinaRegionArgs args)
        {
            StringBuilder sb = new StringBuilder();
            var content = await GetPageString(url, args);
            var citys = content.Split("CITYTR");
            for (int c = 1, len = citys.Length; c < len; c++)
            {
                var strs = citys[c].Split("<A HREF='");
                string cityUrl = string.Empty;
                for (int si = 1; si < 3; si++)
                {
                    var start = strs[si].IndexOf("'>") + 2;
                    var end = strs[si].IndexOf("</A>") - start;
                    var cityCode = strs[si].Substring(start,
                        end);
                    if (si == 1)
                    {
                        //取链接和编码
                        cityUrl = strs[si].Substring(0, strs[si].IndexOf("'>"));

                        sb.Append($"<tr><td>{cityCode}</td>");
                    }
                    else
                    {
                        sb.Append(
                            $"<td></td><td>${cityCode}</td><td></td><td></td><td></td><td></td></tr>");
                        Console.WriteLine($"爬取:{cityCode}");
                    }
                }

                var xian = await ReadXian(cityUrl.Substring(0, cityUrl.IndexOf("/") + 1), cityUrl, args);
                sb.Append(xian);
            }

            return sb;
        }

        private async Task<StringBuilder> ReadXian(string prix, string url, ChinaRegionArgs args)
        {
            StringBuilder sb = new StringBuilder();
            var content = await GetPageString(url, args);
            ;
            var citys = content.Split("COUNTYTR");
            for (int i = 1; i < citys.Length; i++)
            {
                string cityUrl = string.Empty;

                //发现石家庄有一个县居然没超链接，特殊处理
                if (citys[i].IndexOf("<A HREF='") == -1)
                {
                    sb.AppendLine(
                        $"<tr><td>{citys[i].Substring(6, 18 - 6)}</td><td></td><td></td><td>{citys[i].Substring(citys[i].IndexOf("</TD><TD>") + 9, citys[i].LastIndexOf("</TD>") - citys[i].IndexOf("</TD><TD>") - 9)}</td><td></td><td></td><td></td></tr>");
                }
                else
                {
                    String[] strs = citys[i].Split("<A HREF='");
                    for (int si = 1; si < 3; si++)
                    {
                        var start = strs[si].IndexOf("'>") + 2;
                        var end = strs[si].IndexOf("</A>") - start;
                        var cityCode = strs[si].Substring(start,
                            end);
                        if (si == 1)
                        {
                            //取链接和编码
                            cityUrl = strs[si].Substring(0, strs[si].IndexOf("'>"));
                            sb.Append($"<tr><td>{cityCode}</td>");
                        }
                        else
                        {
                            sb.Append($"<td></td><td></td><td>{cityCode}</td><td></td><td></td><td></td></tr>");
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(cityUrl))
                {
                    var zhen = await ReadZhen(prix, cityUrl, args);
                    sb.Append(zhen);
                }
            }

            return sb;
        }

        private async Task<StringBuilder> ReadZhen(string prix, string url, ChinaRegionArgs args)
        {
            StringBuilder sb = new StringBuilder();
            var content = await GetPageString(prix + url, args);
            ;
            var myPrix = (prix + url).Substring(0, (prix + url).LastIndexOf("/") + 1);
            var citys = content.Split("TOWNTR");
            for (int i = 1; i < citys.Length; i++)
            {
                String[] strs = citys[i].Split("<A HREF='");
                String cityUrl = null;
                for (int si = 1; si < 3; si++)
                {
                    var start = strs[si].IndexOf("'>") + 2;
                    var end = strs[si].IndexOf("</A>") - start;
                    var cityCode = strs[si].Substring(start,
                        end);
                    if (si == 1)
                    {
                        //取链接和编码
                        cityUrl = strs[si].Substring(0, strs[si].IndexOf("'>"));
                        sb.Append($"<tr><td>{cityCode}</td>");
                    }
                    else
                    {
                        sb.Append($"<td></td><td></td><td></td><td>{cityCode}</td><td></td><td></td></tr>");
                    }
                }

                var cun = await ReadCun(myPrix, cityUrl, args);
                sb.Append(cun);
            }

            return sb;
        }

        private async Task<StringBuilder> ReadCun(string prix, string url, ChinaRegionArgs args)
        {
            StringBuilder sb = new StringBuilder();
            var content = await GetPageString(prix + url, args);
            var citys = content.Split("VILLAGETR");
            for (int i = 1; i < citys.Length; i++)
            {
                var strs = citys[i].Split("<TD>");
                sb.Append(
                    $"<tr><td>{strs[1].Substring(0, strs[1].IndexOf("</TD>"))}</td><td></td><td></td><td></td><td></td><td>{strs[2].Substring(0, strs[2].IndexOf("</TD>"))}</td><td>{strs[3].Substring(0, strs[3].IndexOf("</TD>"))}</td></tr>");
            }

            return sb;
        }


        public int RetryTimes { get; set; } = 3;

        private async Task<string> GetPageString(string url, ChinaRegionArgs args)
        {
            var httpResponseMessage = await _httpClient.GetAsync(url);
            try
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                RoleBackTimes();
                using StreamReader reader = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync(),
                    Encoding.GetEncoding("GBK"));
                return (await reader.ReadToEndAsync()).ToUpper();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (RetryTimes-- <= 0)
                {
                    return (await GetPageString(url, args)).ToUpper();
                }
            }

            return string.Empty;
        }

        private void RoleBackTimes()
        {
            RetryTimes = 3;
        }
    }
}