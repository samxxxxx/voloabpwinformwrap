using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using wrap_test.Localization;

namespace wrap_test.Web;

[Dependency(ReplaceServices = true)]
public class wrap_testBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<wrap_testResource> _localizer;

    public wrap_testBrandingProvider(IStringLocalizer<wrap_testResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
