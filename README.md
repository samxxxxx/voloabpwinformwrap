# Volo ABP HttpClient Api 自定义包装测试

## 问题点

在 `wrap_test.Web` 项目的 `wrap_testWebModule`模块，注册使用自定义结果包装 `context.Services.RegisterWrapResult(true);`服务，在 Winform 下调用 `wrap_test.Application.Contracts` `IBookAppService`服务接口，产生如下异常：

```
2025-08-25 13:53:36 [Error] 系统异常
System.NullReferenceException: Object reference not set to an instance of an object.
   at Volo.Abp.Http.Client.DynamicProxying.ApiDescriptionFinder.FindActionAsync(HttpClient client, String baseUrl, Type serviceType, MethodInfo method)
   at Volo.Abp.Http.Client.DynamicProxying.DynamicHttpProxyInterceptor`1.GetActionApiDescriptionModel(IAbpMethodInvocation invocation)
   at Volo.Abp.Http.Client.DynamicProxying.DynamicHttpProxyInterceptor`1.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at WebsiteView.<>c__DisplayClass11_0.<<BindData>b__0>d.MoveNext() in WebsiteView.cs:line 70
--- End of stack trace frog ExecuteAsync(Func`1 asyncAction, String caption, String description) in WaitDialogForm.cs:line 166
   at BindData() in WebsiteView.cs:line 61
   at WebsiteView_Load(Object sender, EventArgs e) in WebsiteView.cs:line 82
   at System.Threading.Tasks.Task.<>c.<ThrowAsync>b__128_0(Object state)
   at System.Windows.Forms.Control.InvokeMarshaledCallbackDo(ThreadMethodEntry tme)
   at System.Windows.Forms.Control.InvokeMarshaledCallbackHelper(Object obj)
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Windows.Forms.Control.InvokeMarshaledCallback(ThreadMethodEntry tme)
   at System.Windows.Forms.Control.InvokeMarshaledCallbacks()

```

*swagger中调试结果是正常的*

## 问题重现

1. 将 `wrap_test.HttpApi.Client` 项目中 `wrap_testHttpApiClientModule` 模块 `ConfigureServices` 方法中的代码

```c#
context.Services.AddWrapHttpClientProxies(
    typeof(wrap_testApplicationContractsModule).Assembly,
    RemoteServiceName
);
```
替换成

```c#
context.Services.AddHttpClientProxies(
    typeof(wrap_testApplicationContractsModule).Assembly,
    RemoteServiceName
);
```

2. 排除掉重写的 `wrap_test.DynamicProxying.WrapApiDescriptionFinder` 文件

3. 调用 Form1 中的 Call1 2 3 都会产生 `System.NullReferenceException:“Object reference not set to an instance of an object.”` 异常


## 分析

自定义包装结构：

```json
{
  "data": T,
  "success": true
}
```

1. 使用自定义包装后，会将 `api/abp/api-definition` 返回的结果进行包装，`ApiDescriptionFinder.GetApiDescriptionFromServerAsync` -该方法在对请求的结果反序列化时，找不到任何的 action (*因为结构已经改变*) 而导致`NullReferenceException`.

源码：

```c#
protected virtual async Task<ApplicationApiDescriptionModel> GetApiDescriptionFromServerAsync(
    HttpClient client,
    string baseUrl)
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

    var result = JsonSerializer.Deserialize<ApplicationApiDescriptionModel>(content, DeserializeOptions)!;

    return result;
}
```

2. 在 `DynamicHttpProxyInterceptorClientProxy.RequestAsync<T>(ClientProxyRequestContext requestContext)` 对结果反序列化时，同样也会存在问题.

源码：

```c#
protected virtual async Task<T> RequestAsync<T>(ClientProxyRequestContext requestContext)
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
    if (typeof(T) == typeof(string))
    {
        return (T)(object)stringContent;
    }

    if (stringContent.IsNullOrWhiteSpace())
    {
        return default!;
    }

    return JsonSerializer.Deserialize<T>(stringContent);
}
```


***希望 @abpframework 能兼容一下 client api 的自定义包装.***

*之前也有过类似的问题 [https://github.com/abpframework/abp/issues/15220](https://github.com/abpframework/abp/issues/15220)*

