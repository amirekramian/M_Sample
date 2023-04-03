using Sample.Api.Contract;
using System.Resources;
using System.Text.Json.Serialization;
using Sample.Common;
using Microsoft.OpenApi.Models;
using Sample.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using Sample.DataAccess.Contracts;
using Sample.DataAccess;
using Sample.ExternalServices.Base;
using Sample.ExternalServices.Services;
using Sample.Common.Validators;
using FluentValidation;
using Sample.Common.Profiles;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Sample.Business;

namespace Sample.Web;

internal static class DependencyInjectionExtention
{
        internal static IServiceCollection InjectApi(this IServiceCollection services) =>
                        services
                                .AddControllers()
                                .AddJsonOptions(options =>
                                {
                                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                                })
                                .AddApplicationPart(typeof(IBaseController<>).Assembly)
                                .Services
                                .AddHealthChecks()
                                .Services
                                .AddSingleton(x => new ResourceManager(typeof(Messages)));

        internal static IServiceCollection InjectSwagger(this IServiceCollection services,
                IWebHostEnvironment environment) =>
                environment.IsDevelopment() || environment.IsEnvironment("Testing")
                        ? services.AddSwaggerGen(c =>
                        {
                                c.SwaggerDoc("v1",
                                        new OpenApiInfo { Title = "Sample.Api", Version = "v1" });
                        })
                        : services;

        internal static IServiceCollection InjectContext(this IServiceCollection services,
                IConfiguration configuration, IWebHostEnvironment environment) =>
                environment.IsDevelopment() || environment.IsEnvironment("Testing")
                        ? services.AddDbContextPool<SampleContext>(options =>
                                options.UseInMemoryDatabase("Sample"))
                        : services.AddDbContext<SampleContext>(options => options.UseSqlServer(
                                configuration.GetConnectionString("SampleContext"),
                                serverDbContextOptionsBuilder =>
                                {
                                        var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                                        serverDbContextOptionsBuilder.CommandTimeout(minutes);
                                        serverDbContextOptionsBuilder.EnableRetryOnFailure();
                                }));

        internal static IServiceCollection InjectSieve(this IServiceCollection services) =>
                services.AddScoped<ISieveProcessor, SieveProcessor>();

        internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
                services.AddScoped<IUnitOfWork, UnitOfWork>();

        internal static IServiceCollection InjectExternalServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
                return services.AddSingleton<BaseGatewayService>(_ => new PersonDataGatewayService(
                    configuration["GatewayConfiguration:Sample:PersonInquiry:AccessKey"]!,
                    configuration["GatewayConfiguration:Sample:PersonInquiry:BaseAddress"]!));               
        }

        internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
                services
                        .AddValidatorsFromAssemblyContaining<PersonValidator>()
                        .AddValidatorsFromAssemblyContaining<PhoneValidator>();

        internal static IServiceCollection InjectAutoMapper(this IServiceCollection services) =>
                services.AddAutoMapper(typeof(PersonProfile).Assembly);

        internal static IServiceCollection InjectContentCompression(this IServiceCollection services) =>
                        services.Configure<GzipCompressionProviderOptions>
                                        (options => options.Level = CompressionLevel.Optimal)
                                .AddResponseCompression(options =>
                                {
                                        options.EnableForHttps = true;
                                        options.MimeTypes = new[]
                                        {
					"text/plain",
                                        "text/html",
                                        "text/csv",
                                        "font/woff2",
                                        "application/javascript",
                                        "image/x-icon",
                                        "image/png"
                                        };
                                        options.Providers.Add<BrotliCompressionProvider>();
                                        options.Providers.Add<GzipCompressionProvider>();
                                });

        internal static IServiceCollection InjectBusinesses(this IServiceCollection services) =>
                services.Scan(scan =>
                                scan.FromAssembliesOf(typeof(IBaseBusiness<>))
                                        .AddClasses(classes =>
                                                classes.AssignableTo(typeof(IBaseBusiness<>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime()
                                        .AddClasses(classes =>
                                                classes.Where(predicate =>
                                                        predicate.Name.EndsWith("Business") && !predicate.IsAssignableTo(typeof(IBaseBusiness<>))))
                                        .AsSelf()
                                        .WithScopedLifetime());
}
