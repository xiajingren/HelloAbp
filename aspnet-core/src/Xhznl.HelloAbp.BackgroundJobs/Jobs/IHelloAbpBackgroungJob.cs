using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Xhznl.HelloAbp.Jobs
{
    public interface IHelloAbpBackgroungJob : ITransientDependency
    {
        Task ExecuteAsync();
    }
}
