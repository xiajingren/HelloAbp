using Xhznl.DataDictionary.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Xhznl.DataDictionary
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(DataDictionaryEntityFrameworkCoreTestModule)
        )]
    public class DataDictionaryDomainTestModule : AbpModule
    {
        
    }
}
