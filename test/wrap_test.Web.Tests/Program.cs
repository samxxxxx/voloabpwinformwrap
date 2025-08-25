using Microsoft.AspNetCore.Builder;
using wrap_test;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("wrap_test.Web.csproj"); 
await builder.RunAbpModuleAsync<wrap_testWebTestModule>(applicationName: "wrap_test.Web");

public partial class Program
{
}
