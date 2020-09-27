using System;

namespace Xhznl.HelloAbp.Jobs.ChinaRegion
{
    public class ChinaRegionArgs
    {
        public Uri BaseGovUri => new Uri($"http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/{Year}/");

        public int Year { get; set; } = 2019;
    }
}