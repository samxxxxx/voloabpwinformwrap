using System.Threading.Tasks;

namespace wrap_test.Data;

public interface Iwrap_testDbSchemaMigrator
{
    Task MigrateAsync();
}
