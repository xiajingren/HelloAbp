using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using Xhznl.HelloAbp.EntityFrameworkCore;
using Xhznl.HelloAbp.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Xhznl.DataDictionary;
using Xhznl.FileManagement;

namespace Xhznl.HelloAbp
{
    [DependsOn(
        typeof(HelloAbpHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(HelloAbpApplicationModule),
        typeof(HelloAbpEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class HelloAbpHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureCache(configuration);
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureFile(hostingEnvironment);
        }

        private void ConfigureFile(IWebHostEnvironment hostingEnvironment)
        {
            Configure<FileManagement.Files.FileOptions>(options =>
            {
                options.FileUploadLocalFolder = Path.Combine(hostingEnvironment.ContentRootPath, "upload");
            });
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "HelloAbp:"; });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloAbpDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Xhznl.HelloAbp.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloAbpDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Xhznl.HelloAbp.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloAbpApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Xhznl.HelloAbp.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloAbpApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Xhznl.HelloAbp.Application"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<FileManagementDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}file-management{0}src{0}Xhznl.FileManagement.Domain.Shared",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FileManagementDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}file-management{0}src{0}Xhznl.FileManagement.Domain",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FileManagementApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}file-management{0}src{0}Xhznl.FileManagement.Application.Contracts",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FileManagementApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}file-management{0}src{0}Xhznl.FileManagement.Application",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FileManagementHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}file-management{0}src{0}Xhznl.FileManagement.HttpApi",Path.DirectorySeparatorChar)));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<DataDictionaryDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}data-dictionary{0}src{0}Xhznl.DataDictionary.Domain.Shared",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DataDictionaryDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}data-dictionary{0}src{0}Xhznl.DataDictionary.Domain",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DataDictionaryApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}data-dictionary{0}src{0}Xhznl.DataDictionary.Application.Contracts",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DataDictionaryApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}data-dictionary{0}src{0}Xhznl.DataDictionary.Application",Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<DataDictionaryHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath,string.Format("..{0}..{0}modules{0}data-dictionary{0}src{0}Xhznl.DataDictionary.HttpApi",Path.DirectorySeparatorChar)));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(HelloAbpApplicationModule).Assembly);
            });
            
            //TODO:Automatic registration is reproduced later.Temporary use of manual registration
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(DataDictionaryApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "data-dictionary";
                    opt.RemoteServiceName = "AbpDataDictionary";
                    opt.UrlControllerNameNormalizer = (context) =>
                    {
                        if (context.ControllerName == "DictionaryDetail")
                        {
                            return "dictionary-detail";
                        }
                        return context.ControllerName;
                    };
                });
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "HelloAbp";
                });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "HelloAbp API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    options.OrderActionsBy(decs => decs.RelativePath);
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme()
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = JwtBearerDefaults.AuthenticationScheme,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                //options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                //options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                //options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                //options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                //options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "HelloAbp-Protection-Keys");
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAbpRequestLocalization();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "HelloAbp API"); });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}