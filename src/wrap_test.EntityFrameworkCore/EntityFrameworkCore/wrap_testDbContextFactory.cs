using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace wrap_test.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class wrap_testDbContextFactory : IDesignTimeDbContextFactory<wrap_testDbContext>
{
    public wrap_testDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        wrap_testEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<wrap_testDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new wrap_testDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../wrap_test.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
