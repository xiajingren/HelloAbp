using Xhznl.HelloAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.Identity;

namespace Xhznl.HelloAbp.Permissions
{
    public class HelloAbpPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.GetGroup(HelloIdentityPermissions.GroupName);

            var ouPermission = identityGroup.AddPermission(HelloIdentityPermissions.OrganitaionUnits.Default, L("Permission:OrganitaionUnitManagement"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Create, L("Permission:Create"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Update, L("Permission:Edit"));
            ouPermission.AddChild(HelloIdentityPermissions.OrganitaionUnits.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HelloAbpResource>(name);
        }
    }
}
