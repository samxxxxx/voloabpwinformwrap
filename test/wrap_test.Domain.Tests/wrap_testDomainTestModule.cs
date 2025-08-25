using Volo.Abp.Modularity;

namespace wrap_test;

[DependsOn(
    typeof(wrap_testDomainModule),
    typeof(wrap_testTestBaseModule)
)]
public class wrap_testDomainTestModule : AbpModule
{

}
