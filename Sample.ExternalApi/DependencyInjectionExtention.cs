using FluentValidation;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Sample.Api.Contract;
using Sample.Business.Businesses;
using Sample.Common;
using Sample.Common.Profiles;
using Sample.Common.Validators;
using Sample.ExternalServices.Base;
using Sample.ExternalServices.Services;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Resources;
using System.Text.Json.Serialization;

namespace Sample.ExternalApi;

internal static class DependencyInjectionExtension
{
        internal static IServiceCollection InjectController(this IServiceCollection services) =>
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

        internal static IServiceCollection InjectSqlConnection(this IServiceCollection services, IConfiguration configuration) =>
                        services.AddTransient(_ => new SqlConnection(configuration.GetConnectionString("EntitiesContext")));

        internal static IServiceCollection InjectBusinesses(this IServiceCollection services) =>
                services.AddScoped<PersonBusiness>();

        internal static IServiceCollection InjectExternalServices(this IServiceCollection services,
             IConfiguration configuration, IWebHostEnvironment environment)
        {
                return services.AddSingleton<BaseGatewayService>(_ => new PersonDataGatewayService(
                    configuration["GatewayConfiguration:Sample:PersonInquiry:AccessKey"]!,
                    configuration["GatewayConfiguration:Sample:PersonInquiry:BaseAddress"]!));
        }

        internal static IServiceCollection InjectContentCompression(this IServiceCollection services) =>
        services.Configure<GzipCompressionProviderOptions>
                        (options => options.Level = CompressionLevel.Optimal)
                .AddResponseCompression(options =>
                {
                        options.EnableForHttps = true;
                        options.MimeTypes = new[]
                        {
					// General
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

        internal static IServiceCollection InjectAutoMapper(this IServiceCollection services) =>
                services.AddAutoMapper(typeof(PersonProfile).Assembly);

        internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
                services.AddValidatorsFromAssemblyContaining<PersonValidator>();
}