using Volo.Abp.Identity;
using Volo.Abp.Reflection;

namespace Volo.Abp.IdentityServer
{
    public class IdentityServerPermissions
    {
        public const string GroupName = "AbpIdentityServer";

        public class Clients
        {
            public const string Default = IdentityServerPermissions.GroupName + ".Clients";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
        
        public static string[] GetAll() => ReflectionHelper.GetPublicConstantsRecursively(typeof (IdentityServerPermissions));
    }
}