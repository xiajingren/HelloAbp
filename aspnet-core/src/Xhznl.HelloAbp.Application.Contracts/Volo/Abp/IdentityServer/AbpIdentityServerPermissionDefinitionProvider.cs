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
            var identityServerGroup = context.AddGroup(IdentityServerPermissions.GroupName,L("GroupIdentityServer"));
            var identityServerPermissions = identityServerGroup.AddPermission(IdentityServerPermissions.Clients.Default,
                L("Permission:ClientManagement"));
            identityServerPermissions.AddChild(IdentityServerPermissions.Clients.Create, L("Permission:Add"));
            identityServerPermissions.AddChild(IdentityServerPermissions.Clients.Update, L("Permission:Update"));
            identityServerPermissions.AddChild(IdentityServerPermissions.Clients.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpIdentityServerResource>(name);
        }
    }
}