using Volo.Abp.Modularity;

namespace wrap_test;

/* Inherit from this class for your domain layer tests. */
public abstract class wrap_testDomainTestBase<TStartupModule> : wrap_testTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
