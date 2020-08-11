using Volo.Abp.Settings;

namespace Xhznl.HelloAbp.Settings
{
    public class HelloAbpSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(HelloAbpSettings.MySetting1));
        }
    }
}
