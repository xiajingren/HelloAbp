using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
	public class GetIdentityClaimTypesInput : PagedAndSortedResultRequestDto
	{
		public string Filter { get; set; }

	}
}
