using Xunit;

namespace wrap_test.EntityFrameworkCore;

[CollectionDefinition(wrap_testTestConsts.CollectionDefinitionName)]
public class wrap_testEntityFrameworkCoreCollection : ICollectionFixture<wrap_testEntityFrameworkCoreFixture>
{

}
