using Xhznl.HelloAbp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Localization;

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

            var rolePermission = identityGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            rolePermission?.AddChild(HelloIdentityPermissions.Roles.AddOrganizationUnitRole, IdentityL("Permission:AddOrganizationUnitRole"));

            //Claim
            var claimPermission = identityGroup.AddPermission(HelloIdentityPermissions.ClaimTypes.Default, IdentityL("Permission:ClaimManagement"));
            claimPermission.AddChild(HelloIdentityPermissions.ClaimTypes.Create, IdentityL("Permission:Create"));
            claimPermission.AddChild(HelloIdentityPermissions.ClaimTypes.Update, IdentityL("Permission:Edit"));
            claimPermission.AddChild(HelloIdentityPermissions.ClaimTypes.Delete, IdentityL("Permission:Delete"));

            //AuditLogging
            var auditLogGroup = context.AddGroup(AuditLogPermissions.GroupName);
            var aduditLogPermission = auditLogGroup.AddPermission(AuditLogPermissions.AuditLogs.Default, AuditLoggingL("Permission:AuditLogManagement"));
            aduditLogPermission.AddChild(AuditLogPermissions.AuditLogs.Delete, AuditLoggingL("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HelloAbpResource>(name);
        }

        private static LocalizableString IdentityL(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }

        private static LocalizableString AuditLoggingL(string name)
        {
            return LocalizableString.Create<AuditLoggingResource>(name);
        }

    }
}
