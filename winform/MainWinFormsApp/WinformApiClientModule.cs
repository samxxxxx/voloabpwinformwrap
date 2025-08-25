using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;
using wrap_test;
using wrap_test.DynamicProxying;

namespace MainWinFormsApp
{
    [DependsOn(

        typeof(AbpHttpClientIdentityModelModule),
        typeof(wrap_testApplicationContractsModule),
        typeof(wrap_testHttpApiClientModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpHttpClientModule)
    )]
    public class WinformApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WinformApiClientModule>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            // Initialization logic can be added here if needed
        }
    }
}
