using Volo.Abp.Reflection;

namespace Xhznl.DataDictionary.Permissions
{
    public class DataDictionaryPermissions
    {
        public const string GroupName = "AbpDataDictionary";
        public const string GroupDetailName = GroupName + ".Detail";
        public static class DataDictionary
        {
            public const string Default = GroupName + ".Default";
            public const string Delete = GroupName + ".Delete";
            public const string Update = GroupName + ".Update";
            public const string Create = GroupName + ".Create";
        }

        public static class DataDictionaryDetail
        {
            public const string Default = GroupDetailName + ".Default";
            public const string Delete = GroupDetailName + ".Delete";
            public const string Update = GroupDetailName + ".Update";
            public const string Create = GroupDetailName + ".Create";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DataDictionaryPermissions));
        }
    }
}