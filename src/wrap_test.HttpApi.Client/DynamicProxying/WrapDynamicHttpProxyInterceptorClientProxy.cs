using System;
using System.Threading.Tasks;
using Volo.Abp.Content;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Client.DynamicProxying;

namespace wrap_test.DynamicProxying
{
    public class WrapDynamicHttpProxyInterceptorClientProxy<TService> : DynamicHttpProxyInterceptorClientProxy<TService>
    {
        public override Task<T> CallRequestAsync<T>(ClientProxyRequestContext requestContext)
        {
            return this.RequestAsync<T>(requestContext);
        }

        protected override async Task<T> RequestAsync<T>(ClientProxyRequestContext requestContext)
        {
            var responseContent = await RequestAsync(requestContext);

            if (typeof(T) == typeof(IRemoteStreamContent) ||
                typeof(T) == typeof(RemoteStreamContent))
            {
                /* returning a class that holds a reference to response
                 * content just to be sure that GC does not dispose of
                 * it before we finish doing our work with the stream */
                return (T)(object)new RemoteStreamContent(
                    await responseContent.ReadAsStreamAsync(),
                    responseContent.Headers?.ContentDisposition?.FileNameStar ??
                    RemoveQuotes(responseContent.Headers?.ContentDisposition?.FileName).ToString(),
                    responseContent.Headers?.ContentType?.ToString(),
                    responseContent.Headers?.ContentLength);
            }

            var stringContent = await responseContent.ReadAsStringAsync();

            if (stringContent.IsNullOrWhiteSpace())
            {
                return default!;
            }

            var result = JsonSerializer.Deserialize<Oxetek.AspNetCore.Mvc.Models.FetchResponse<T>>(stringContent);
            return result.Data;
        }
    }
}
