using Sample;
using Sample.DataAccess.Contexts;
using Sample.Web;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddEnvironmentVariables();

builder.Services
                .InjectApi()
                .InjectSwagger(builder.Environment)
                .InjectUnitOfWork()
                .InjectSieve()
                .InjectContext(builder.Configuration, builder.Environment)
                .InjectBusinesses()
                .InjectFluentValidation()
                .InjectAutoMapper()
                .InjectContentCompression()
                .InjectExternalServices(builder.Configuration, builder.Environment)
                .AddEndpointsApiExplorer();

        var app = builder.Build();

        await using var scope = app.Services.CreateAsyncScope();

        await using var context = scope.ServiceProvider.GetRequiredService<SampleContext>();

        await context.Database.EnsureCreatedAsync();

        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
                app.UseDeveloperExceptionPage()
                        .UseSwagger()
                        .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                "Swagger Demo API v1"));
        else
                app.UseExceptionHandler("/Error")
                        .UseHsts();

        app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
                                endpoints.MapHealthChecks("HealthCheck");
                        endpoints.MapControllers();
                        endpoints.MapRazorPages().RequireAuthorization();
                });

        await app.RunAsync();

public partial class Program { }