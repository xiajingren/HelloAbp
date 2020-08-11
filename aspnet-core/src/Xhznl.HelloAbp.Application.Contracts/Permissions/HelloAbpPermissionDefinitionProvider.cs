using Xhznl.HelloAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Xhznl.HelloAbp.Permissions
{
    public class HelloAbpPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(HelloAbpPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(HelloAbpPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HelloAbpResource>(name);
        }
    }
}
