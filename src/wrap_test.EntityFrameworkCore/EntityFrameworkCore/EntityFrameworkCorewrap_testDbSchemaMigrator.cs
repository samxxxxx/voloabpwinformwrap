using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wrap_test.Data;
using Volo.Abp.DependencyInjection;

namespace wrap_test.EntityFrameworkCore;

public class EntityFrameworkCorewrap_testDbSchemaMigrator
    : Iwrap_testDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorewrap_testDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the wrap_testDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<wrap_testDbContext>()
            .Database
            .MigrateAsync();
    }
}
