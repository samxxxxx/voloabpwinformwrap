using wrap_test.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace wrap_test.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(wrap_testEntityFrameworkCoreModule),
    typeof(wrap_testApplicationContractsModule)
)]
public class wrap_testDbMigratorModule : AbpModule
{
}
