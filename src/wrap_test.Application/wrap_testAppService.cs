using wrap_test.Localization;
using Volo.Abp.Application.Services;

namespace wrap_test;

/* Inherit your application services from this class.
 */
public abstract class wrap_testAppService : ApplicationService
{
    protected wrap_testAppService()
    {
        LocalizationResource = typeof(wrap_testResource);
    }
}
