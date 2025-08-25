using wrap_test.Samples;
using Xunit;

namespace wrap_test.EntityFrameworkCore.Applications;

[Collection(wrap_testTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<wrap_testEntityFrameworkCoreTestModule>
{

}
