using Volo.Abp.Localization;
using Volo.Abp.Settings;
using Xhznl.FileManagement.Localization;

namespace Xhznl.FileManagement.Settings
{
    public class FileManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from FileManagementSettings class.
             */

            context.Add(new SettingDefinition(
                FileManagementSettings.AllowedMaxFileSize,
                "1024",
                L("DisplayName:FileManagement.AllowedMaxFileSize"),
                L("Description:FileManagement.AllowedMaxFileSize")
                )
                    .WithProperty("Group1", "File")
                    .WithProperty("Group2", "Upload")
                    .WithProperty("Type", "number"),

                new SettingDefinition(
                    FileManagementSettings.AllowedUploadFormats,
                    ".jpg,.jpeg,.png,.gif,.txt",
                    L("DisplayName:FileManagement.AllowedUploadFormats"),
                    L("Description:FileManagement.AllowedUploadFormats")
                )
                    .WithProperty("Group1", "File")
                    .WithProperty("Group2", "Upload")
                    .WithProperty("Type", "text")
                );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FileManagementResource>(name);
        }
    }
}