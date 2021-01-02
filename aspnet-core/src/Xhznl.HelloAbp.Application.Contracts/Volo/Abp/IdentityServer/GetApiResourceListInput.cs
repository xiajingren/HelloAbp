namespace Volo.Abp.IdentityServer
{
    public class GetApiResourceListInput:Application.Dtos.PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}