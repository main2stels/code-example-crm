using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullCRM.Config;
using FullCRM.Database;
using FullCRM.Database.Postgre.PostgreEssence;
using FullCRM.Database.Postgre.Model;
using FullCRM.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using FullCRM.Database.MongoDB.Database;
using FullCRM.Database.MongoDB.Repository;
using Microsoft.AspNetCore.Http;
using FullCRM.Middleware;

namespace FullCRM
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"\fullcrm\keys\"))
            .SetApplicationName("FullCrm");

            services.AddLogging();

            services.Configure<Section>(Configuration);

            services.AddScoped<InventoryDb>();
            services.AddSingleton<VersionHistoryDB>();
            services.AddSingleton<LogDB>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();


            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.RedirectUri = null;
                        return Task.CompletedTask;
                    };

                });

            services.AddTransient(typeof(PostgreEssence<>));

            services.AddTransient<LogRepository>();
            services.AddTransient<LoggerService>();
            services.AddTransient<VersionsLogRepository>();            
            services.AddTransient<VersionService>();

            services.AddTransient<InfoWebSocketService>();

            services.AddTransient<NextNumberEssenceService>();
            services.AddTransient<ContractService>();
            services.AddTransient<OrderService>();
            services.AddTransient<ProductService>();          
            services.AddTransient<ContractorService>();
            services.AddTransient<OrganizationService>();
            services.AddTransient<AccountBillingService>();
            services.AddTransient<AddressService>();
            services.AddTransient<InvoiceService>();
            services.AddTransient<ContactService>();

            services.AddTransient<UserService>();
            services.AddTransient<ActService>();

            

            services.AddTransient<DocumentService>();

            services.AddTransient<FrontendBillingService>();

            services.AddTransient<FrontendService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            LoggerService loggerService)
        {

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseWebSockets();
            app.UseMiddleware<InfoWebSocketMiddleware>();

            app.UseMvc();

            loggerFactory.AddProvider(new LoggerProvider(loggerService));
            var logger = loggerFactory.CreateLogger("Logger");
            
        }
    }
}
