# dotnetcore-datadog-tracing-middleware
aspnetcore middleware that sets custom tags in spans as part of the request lifecycle


# use
In Startup.cs, use the extension to add the middleware into the stack *before MVC*:
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseHsts();
    }

    app.UseDataDogTracing("myCoolService");
    app.UseMvc();
}
```
