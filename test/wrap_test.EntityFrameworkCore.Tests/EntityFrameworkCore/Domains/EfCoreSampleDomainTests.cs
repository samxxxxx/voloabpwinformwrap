using wrap_test.Samples;
using Xunit;

namespace wrap_test.EntityFrameworkCore.Domains;

[Collection(wrap_testTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<wrap_testEntityFrameworkCoreTestModule>
{

}
