using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Threading;

namespace Xhznl.DataDictionary
{
    public class DataDictionaryDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<DataDictionary, Guid> _directory;
        private readonly IRepository<DataDictionaryDetail, Guid> _directoryDetail;

        public DataDictionaryDataSeedContributor(
            IRepository<DataDictionary, Guid> directory,
            IRepository<DataDictionaryDetail, Guid> directoryDetail,
            IGuidGenerator guidGenerator)
        {
            _directory = directory;
            _directoryDetail = directoryDetail;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await Task.CompletedTask;
        }
    }
}