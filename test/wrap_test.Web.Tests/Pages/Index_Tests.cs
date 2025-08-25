using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace wrap_test.Pages;

[Collection(wrap_testTestConsts.CollectionDefinitionName)]
public class Index_Tests : wrap_testWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
