using Volo.Abp.Modularity;

namespace wrap_test;

public abstract class wrap_testApplicationTestBase<TStartupModule> : wrap_testTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
