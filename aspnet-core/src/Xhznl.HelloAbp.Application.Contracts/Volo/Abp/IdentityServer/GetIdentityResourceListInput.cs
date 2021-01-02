namespace Volo.Abp.IdentityServer
{
    public class GetIdentityResourceListInput : Application.Dtos.PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}