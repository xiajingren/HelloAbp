using System;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Xhznl.HelloAbp.Jobs.ChinaRegion;

namespace Xhznl.HelloAbp
{
    [DependsOn(typeof(HelloAbpDomainModule),
        typeof(AbpEmailingModule))]
    public class HelloAbpBackgroundJobSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<ChinaRegionOption>(options =>
            {
                options.Year = 2019;
                options.BaseGovUri=new Uri("http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2019/");
            });
        }
    }
}