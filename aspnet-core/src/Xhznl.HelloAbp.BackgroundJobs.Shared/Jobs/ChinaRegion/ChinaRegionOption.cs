using System;

namespace Xhznl.HelloAbp.Jobs.ChinaRegion
{
    public class ChinaRegionOption
    {
        public int Year { get; set; } = 2019;

        public Uri BaseGovUri { get; set; } = new Uri("http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2019/");
    }
}