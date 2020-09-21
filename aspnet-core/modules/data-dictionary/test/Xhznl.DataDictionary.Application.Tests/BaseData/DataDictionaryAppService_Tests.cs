using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement;
using Xhznl.DataDictionary.BaseData.DataDictionaryManagement.Dto;
using Xunit;

namespace Xhznl.DataDictionary.BaseData
{
    public class DataDictionaryAppService_Tests : DataDictionaryApplicationTestBase
    {
        private readonly IDictionaryAppService _dictionaryAppService;
        private readonly IDictionaryDetailAppService _dictionaryDetailAppService;
        private readonly IUnitOfWorkManager _uowMgr;
        private ICurrentTenant _fakeCurrentTenant;

        public DataDictionaryAppService_Tests()
        {
            _dictionaryAppService = GetRequiredService<IDictionaryAppService>();
            _dictionaryDetailAppService = GetRequiredService<IDictionaryDetailAppService>();
            _uowMgr = GetRequiredService<IUnitOfWorkManager>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            _fakeCurrentTenant = Substitute.For<ICurrentTenant>();
            services.AddSingleton(_fakeCurrentTenant);
        }

        [Fact]
        public async Task Get_List_Test()
        {
            var result = await _dictionaryAppService.GetListAsync(new GetDictionaryInputDto());
            result.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Get_GenderDetail_List_Test()
        {
            var result = await _dictionaryDetailAppService.GetListAsync(new GetDictionaryDetailInputDto
            {
                Pid = DataDictionaryBuilder.GenderId
            });
            result.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Create_DataDictionary_Test()
        {
            var input = new CreateDataDictionaryDto
            {
                Name = "外贸",
                Description = "I-进口,E-出口,C-国产"
            };
            var create = await _dictionaryAppService.CreateAsync(input);
            create.ShouldNotBeNull();
            create.Name.ShouldBe("外贸");
        }

        [Fact]
        public async Task Create_DataDictionary_SetTenantId_Test()
        {
            //TODO:Set tenant
            _fakeCurrentTenant.Id.Returns(DataDictionaryBuilder.TenantId1);
            var input = new CreateDataDictionaryDto
            {
                Name = DataDictionaryBuilder.TenantId1 + "-外贸",
                Description = "I-进口,E-出口,C-国产",
                Sort = 100
            };
            var create = await _dictionaryAppService.CreateAsync(input);
            create.ShouldNotBeNull();

            var repository = GetService<IRepository<DataDictionary, Guid>>();
            var result = await repository.GetAsync(create.Id);
            result.ShouldNotBeNull();
            result.TenantId.ShouldNotBeNull();
            result.TenantId.ShouldNotBe(Guid.Empty);
            result.TenantId.ShouldBe(DataDictionaryBuilder.TenantId1);
        }

        [Fact]
        public async Task Add_DrugType_Test()
        {
            (await _dictionaryDetailAppService.CreateAsync(new CreateDataDictionaryDetailDto
            {
                Pid = DataDictionaryBuilder.DrugTypeId,
                Label = "胶囊",
                Value = "JN",
                Sort = 10
            })).ShouldNotBeNull();

            var reponstory = GetService<IRepository<DataDictionaryDetail, Guid>>();
            (await reponstory.FindAsync(d => d.Value == "JN")).ShouldNotBeNull();
        }

        [Fact]
        public async Task Create_DataDictionaryDetail_Test()
        {
            var input = new CreateDataDictionaryDto
            {
                Name = "外贸",
                Description = "I-进口,E-出口,C-国产"
            };
            var master = await _dictionaryAppService.CreateAsync(input);
            master.ShouldNotBeNull();

            var inputC = new CreateDataDictionaryDetailDto
            {
                Pid = master.Id,
                Label = "国产",
                Value = "G",
                Sort = 1
            };
            (await _dictionaryDetailAppService.CreateAsync(inputC)).ShouldNotBeNull();

            var inputI = new CreateDataDictionaryDetailDto
            {
                Pid = master.Id,
                Label = "进口",
                Value = "I",
                Sort = 2
            };
            (await _dictionaryDetailAppService.CreateAsync(inputI)).ShouldNotBeNull();

            var inputE = new CreateDataDictionaryDetailDto
            {
                Pid = master.Id,
                Label = "出口",
                Value = "E",
                Sort = 3
            };
            (await _dictionaryDetailAppService.CreateAsync(inputE)).ShouldNotBeNull();

            var items = (await _dictionaryDetailAppService.GetListAsync(new GetDictionaryDetailInputDto
            {
                SkipCount = 0,
                Sorting = "sort desc",
                MaxResultCount = 20,
                Pid = master.Id
            })).Items;
            items.FirstOrDefault().ShouldNotBeNull();
            items.Count.ShouldBeGreaterThan(0);
            items.FirstOrDefault().Label.ShouldBe("出口");
        }

        [Fact]
        public async Task Delete_Single_DataDictionary()
        {
            using (var uw=_uowMgr.Begin())
            {
                await _dictionaryAppService.DeleteAsync(DataDictionaryBuilder.ExamTypeId);
                
                var reponstory = GetRequiredService<IRepository<DataDictionary,Guid>>();
                var dataDictionary= await reponstory.GetAsync(DataDictionaryBuilder.ExamTypeId);
                dataDictionary.ShouldBeNull();

                var result = await _dictionaryDetailAppService.GetListAsync(new GetDictionaryDetailInputDto()
                {
                    Pid = DataDictionaryBuilder.ExamTypeId
                });
                result.TotalCount.ShouldBe(0);
            }
        }
    }
}