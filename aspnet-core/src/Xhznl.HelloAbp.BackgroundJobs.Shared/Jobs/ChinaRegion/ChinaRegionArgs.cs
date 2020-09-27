using System;

namespace Xhznl.HelloAbp.Jobs.ChinaRegion
{
    public class ChinaRegionArgs
    {
        /// <summary>
        /// 省份名称(第一级)
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 对应的地址
        /// </summary>
        public string Href { get; set; }
    }
}