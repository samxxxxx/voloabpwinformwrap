using Volo.Abp.Modularity;

namespace wrap_test;

[DependsOn(
    typeof(wrap_testApplicationModule),
    typeof(wrap_testDomainTestModule)
)]
public class wrap_testApplicationTestModule : AbpModule
{

}
