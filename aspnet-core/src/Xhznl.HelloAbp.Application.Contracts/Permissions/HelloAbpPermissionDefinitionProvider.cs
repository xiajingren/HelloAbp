using Xhznl.HelloAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;

namespace Xhznl.HelloAbp.Permissions
{
    public class HelloAbpPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.GetGroup(IdentityPermissions.GroupName);

            var ouPermission = identityGroup.AddPermission(HelloIdentityPermissions.OrganitaionUnits.Default, IdentityL("Permission:OrganitaionUnitManagement"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Create, IdentityL("Permission:Create"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Update, IdentityL("Permission:Edit"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Delete, IdentityL("Permission:Delete"));

            var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            userPermission?.AddChild(HelloIdentityPermissions.Users.DistributionOrganizationUnit, IdentityL("Permission:DistributionOrganizationUnit"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HelloAbpResource>(name);
        }

        private static LocalizableString IdentityL(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }

    }
}
