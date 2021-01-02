using Volo.Abp.Application.Dtos;

namespace Volo.Abp.IdentityServer
{
    public class GetClientListInput:PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}