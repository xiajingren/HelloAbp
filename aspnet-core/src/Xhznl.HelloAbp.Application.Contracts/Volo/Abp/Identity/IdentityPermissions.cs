using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Volo.Abp.Identity
{
    public static class HelloIdentityPermissions
    {
        public static class Users
        {
            // 分配人员给组织单元权限
            public const string DistributionOrganizationUnit = IdentityPermissions.GroupName + ".DistributionOrganizationUnit";
        }

        public static class OrganitaionUnits
        {
            public const string Default = IdentityPermissions.GroupName + ".OrganitaionUnits";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityPermissions));
        }
    }
}
