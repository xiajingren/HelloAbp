using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Identity
{
	public class UpdateClaimTypeDto : ExtensibleObject
	{
		public string Name { get; set; }

		public bool Required { get; set; }

		public string Regex { get; set; }

		public string RegexDescription { get; set; }

		public string Description { get; set; }

		public IdentityClaimValueType ValueType { get; set; }

		public UpdateClaimTypeDto()
			: base(false)
		{
		}
	}
}
