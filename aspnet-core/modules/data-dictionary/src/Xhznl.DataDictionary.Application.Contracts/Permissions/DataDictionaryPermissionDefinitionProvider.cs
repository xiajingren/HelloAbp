using Xhznl.DataDictionary.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Xhznl.DataDictionary.Permissions
{
    public class DataDictionaryPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var dataDirctionaryGroup = context.AddGroup(DataDictionaryPermissions.GroupName, L("Permission:DataDictionary"));

            var dictionary = dataDirctionaryGroup.AddPermission(DataDictionaryPermissions.DataDictionary.Default, L("DataDictionary"));
            dictionary.AddChild(DataDictionaryPermissions.DataDictionary.Update, L("Edit"));
            dictionary.AddChild(DataDictionaryPermissions.DataDictionary.Delete, L("Delete"));
            dictionary.AddChild(DataDictionaryPermissions.DataDictionary.Create, L("Create"));

            var detail=dictionary.AddChild(DataDictionaryPermissions.DataDictionaryDetail.Default, L("DataDictionaryDetail"));
            detail.AddChild(DataDictionaryPermissions.DataDictionaryDetail.Update, L("Edit"));
            detail.AddChild(DataDictionaryPermissions.DataDictionaryDetail.Delete, L("Delete"));
            detail.AddChild(DataDictionaryPermissions.DataDictionaryDetail.Create, L("Create"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DataDictionaryResource>(name);
        }
    }
}