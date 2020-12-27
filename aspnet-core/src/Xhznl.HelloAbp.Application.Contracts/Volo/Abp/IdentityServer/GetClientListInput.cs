using Volo.Abp.Application.Dtos;

namespace Volo.Abp.IdentityServer
{
    public class GetClientListInput:PagedAndSortedResultRequestDto
    {
        public override string Sorting { get; set; } = nameof(ClientDto.ClientId);
    }
}