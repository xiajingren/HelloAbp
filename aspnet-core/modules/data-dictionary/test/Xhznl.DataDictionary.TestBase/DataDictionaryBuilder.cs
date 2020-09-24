using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Xhznl.DataDictionary
{
    public class DataDictionaryBuilder : ITransientDependency
    {
        public static Guid TenantId1 { get; } = new Guid("55687dce-595c-41b4-a024-2a5e991ac8f4");
        public static Guid TenantId2 { get; } = new Guid("f522d19f-5a86-4278-98fb-0577319c544a");
        public static Guid GenderId { get; } = new Guid("eb597b22-42ce-4bb9-81b5-edd40b556316");
        public static Guid DrugTypeId { get; } = new Guid("91358c27-ba24-41fc-843c-6bc9ce2f6016");
        public static Guid CountryId { get; } = new Guid("bbe67ab3-23fa-4bfb-8e3d-b4325a2792b9");
        public static Guid ExamTypeId { get; } = new Guid("3d817bda-90b1-42aa-9be1-bf16722b8450");

        private readonly IRepository<DataDictionary, Guid> _directory;
        private readonly IRepository<DataDictionaryDetail, Guid> _directoryDetail;

        public DataDictionaryBuilder(
            IRepository<DataDictionary, Guid> directory,
            IRepository<DataDictionaryDetail, Guid> directoryDetail)
        {
            _directory = directory;
            _directoryDetail = directoryDetail;
        }

        public async Task BuildAsync()
        {
            await AddDictionariesAsync();
            await AddDetailsAsync();
        }

        private async Task AddDictionariesAsync()
        {
            var list = new List<DataDictionary>
            {
                 new DataDictionary(
                    GenderId,
                    "性别",
                    "F-女，M-男，U-未知",
                    2),

                 new DataDictionary(
                    DrugTypeId,
                    "药物类别",
                    "K-颗粒，F-粉状，Y-液体",
                    1),

                 new DataDictionary(
                    CountryId,
                    "国籍",
                    "C-国内，O-国外",
                    0),

                 new DataDictionary(
                    ExamTypeId,
                    "体检类型",
                    "H-健康体检，C-从业体检，Z-职业病体检",
                    4)
            };
            foreach (var item in list)
            {
                await _directory.InsertAsync(item);
            }
        }

        private async Task AddDetailsAsync()
        {
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: GenderId,
                tenantId: null,
                label: "F",
                value: "女"));
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: GenderId,
                tenantId: null,
                label: "M",
                value: "男"));

            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: DrugTypeId,
                tenantId: TenantId1,
                label: "K",
                value: "颗粒"));
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: DrugTypeId,
                tenantId: TenantId1,
                label: "F",
                value: "粉状"));
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: DrugTypeId,
                tenantId: TenantId1,
                label: "Y",
                value: "液体"));

            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: CountryId,
                tenantId: TenantId1,
                label: "国内",
                value: "C"));
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: CountryId,
                tenantId: TenantId1,
                label: "国外",
                value: "O"));


            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
            id: Guid.NewGuid(),
            pid: ExamTypeId,
            tenantId: TenantId1,
            label: "健康体检",
            value: "H"));
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: ExamTypeId,
                tenantId: TenantId2,
                label: "从业体检",
                value: "C"));
            await _directoryDetail.InsertAsync(new DataDictionaryDetail(
                id: Guid.NewGuid(),
                pid: ExamTypeId,
                tenantId: TenantId2,
                label: "职业病体检",
                value: "Z"));
        }
    }
}
