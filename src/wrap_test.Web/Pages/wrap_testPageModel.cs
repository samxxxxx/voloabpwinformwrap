using wrap_test.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace wrap_test.Web.Pages;

public abstract class wrap_testPageModel : AbpPageModel
{
    protected wrap_testPageModel()
    {
        LocalizationResourceType = typeof(wrap_testResource);
    }
}
