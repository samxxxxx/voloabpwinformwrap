using wrap_test.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace wrap_test.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class wrap_testController : AbpControllerBase
{
    protected wrap_testController()
    {
        LocalizationResource = typeof(wrap_testResource);
    }
}
