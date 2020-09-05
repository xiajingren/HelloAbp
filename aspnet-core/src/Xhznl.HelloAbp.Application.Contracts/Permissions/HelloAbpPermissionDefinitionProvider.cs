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

            var ouPermission = identityGroup.AddPermission(HelloIdentityPermissions.OrganitaionUnits.Default, L("Permission:OrganitaionUnitManagement"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Create, L("Permission:Create"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Update, L("Permission:Edit"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Delete, L("Permission:Delete"));

            var userPermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Users.Default);
            if (userPermission!=null)
            {
                userPermission.AddChild(HelloIdentityPermissions.Users.DistributionOrganizationUnit, L("Permission:DistributionOrganizationUnit"));
            }

            var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            if (rolePermission != null)
            {
                rolePermission.AddChild(HelloIdentityPermissions.Roles.AddOrganizationUnitRole, L("Permission:AddOrganizationUnitRole"));
            }
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
