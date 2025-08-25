using wrap_test.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace wrap_test.Permissions;

public class wrap_testPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(wrap_testPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(wrap_testPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<wrap_testResource>(name);
    }
}
