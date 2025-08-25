using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.DynamicProxying;
using Volo.Abp.Http.Modeling;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Tracing;

namespace wrap_test.DynamicProxying
{
    public class WrapApiDescriptionFinder : ApiDescriptionFinder, ITransientDependency
    {
        public WrapApiDescriptionFinder(IApiDescriptionCache cache, IOptions<AbpCorrelationIdOptions> abpCorrelationIdOptions, ICorrelationIdProvider correlationIdProvider, ICurrentTenant currentTenant) : base(cache, abpCorrelationIdOptions, correlationIdProvider, currentTenant)
        {
        }

        protected override async Task<ApplicationApiDescriptionModel> GetApiDescriptionFromServerAsync(HttpClient client, string baseUrl)
        {
            var requestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            baseUrl.EnsureEndsWith('/') + "api/abp/api-definition"
        );

            AddHeaders(requestMessage);

            var response = await client.SendAsync(
                requestMessage,
                CancellationTokenProvider.Token
            );

            if (!response.IsSuccessStatusCode)
            {
                throw new AbpException("Remote service returns error! StatusCode = " + response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();


            var result = JsonSerializer.Deserialize<Oxetek.AspNetCore.Mvc.Models.FetchResponse<ApplicationApiDescriptionModel>>(content, DeserializeOptions)!;

            return result.Data;
        }
    }
}
