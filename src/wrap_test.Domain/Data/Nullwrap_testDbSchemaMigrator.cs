using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace wrap_test.Data;

/* This is used if database provider does't define
 * Iwrap_testDbSchemaMigrator implementation.
 */
public class Nullwrap_testDbSchemaMigrator : Iwrap_testDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
