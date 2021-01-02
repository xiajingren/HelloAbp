using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.IdentityServer
{
    public class AbpIdentityServerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityServerGroup = context.AddGroup(IdentityServerPermissions.GroupName, L("GroupIdentityServer"));
            var clientPermissions = identityServerGroup.AddPermission(IdentityServerPermissions.Clients.Default,
                L("Permission:ClientManagement"));
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Create, L("Permission:Add"));
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Update, L("Permission:Update"));
            clientPermissions.AddChild(IdentityServerPermissions.Clients.Delete, L("Permission:Delete"));

            //IdentityResource
            var identityResourcePermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.IdentityResources.Default,
                L("Permission:ClientManagement"));
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.Create,
                L("Permission:Add"));
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.Update,
                L("Permission:Update"));
            identityResourcePermissions.AddChild(IdentityServerPermissions.IdentityResources.Delete,
                L("Permission:Delete"));

            //ApiResource
            var apiResourcePermissions = identityServerGroup.AddPermission(
                IdentityServerPermissions.ApiResources.Default,
                L("Permission:ClientManagement"));
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.Create, L("Permission:Add"));
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.Update, L("Permission:Update"));
            apiResourcePermissions.AddChild(IdentityServerPermissions.ApiResources.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpIdentityServerResource>(name);
        }
    }
}