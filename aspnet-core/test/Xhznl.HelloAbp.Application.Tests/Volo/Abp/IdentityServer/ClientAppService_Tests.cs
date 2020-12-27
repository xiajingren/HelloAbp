using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.IdentityServer;
using Xunit;

namespace Xhznl.HelloAbp.Volo.Abp.IdentityServer
{
    public class ClientAppService_Tests:HelloAbpApplicationTestBase
    {
        private readonly IClientAppService _clientApp;
        private readonly AbpIdentityServerTestData _testData;
        public ClientAppService_Tests()
        {
            _clientApp = ServiceProvider.GetRequiredService<IClientAppService>();
            _testData = ServiceProvider.GetRequiredService<AbpIdentityServerTestData>();
        }

        [Fact]
        public async Task ShouldBe_NotNull_GetAsync()
        {
            var client1 = await _clientApp.GetAsync(_testData.Client1Id);
            client1.ShouldNotBeNull();
            client1.Id.ShouldBe(_testData.Client1Id);
        }
        
        [Fact]
        public async Task ShouldBe_NotNull_GetListAsync()
        {
            var clients = await _clientApp.GetListAsync(new GetClientListInput());
            clients.TotalCount.ShouldBeGreaterThan(0);
        }
    }
}