using Volo.Abp.Settings;

namespace wrap_test.Settings;

public class wrap_testSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(wrap_testSettings.MySetting1));
    }
}
