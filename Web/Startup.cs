using System;
using System.Security.Claims;
using AspNetCoreRateLimit;
using Data;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.Identity;
using VueCliMiddleware;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace AusOuvidos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AUS Ouvidos API", Version = "v1" });
            });

            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddHttpClient("ApiClient", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("APIUrl"));
                c.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", Configuration.GetValue<string>("APIKey"));
            });

            services.AddHttpClient("FlowClient", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("FlowUrl"));
                c.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", Configuration.GetValue<string>("APIKey"));
            });

            services.AddHttpClient("RecaptchaClient", c =>
            {
                c.BaseAddress = new Uri("https://www.google.com/recaptcha/api/");
            });

            
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.AddMediatR(typeof(Services.Application).Assembly);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            });

            services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                options.Authority += "/v2.0";
                options.TokenValidationParameters.ValidIssuer = options.Authority;
                options.TokenValidationParameters.ValidAudiences = new[]
                {
                    options.Audience,
                    $"api://{options.Audience}"
                };

                options.Events = new JwtBearerEvents();
                options.Events.OnTokenValidated += async (ctx) =>
                {
                    var oid = ctx.Principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");
                    var name = ctx.Principal.FindFirstValue("name");
                    var email = ctx.Principal.FindFirstValue("preferred_username");

                    var mediator = ctx.HttpContext.RequestServices.GetRequiredService<IMediator>();
                    await mediator.Send(new EnsureUserCommand
                    {
                        UserIdentityId = new Guid(oid),
                        UserEmail = email,
                        UserName = name
                    });
                };
            });

            services.AddDbContext<AusOuvidosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            // (options =>
            //     options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            // Add AddRazorPages if the app uses Razor Pages.
            services.AddRazorPages();

            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUS Ouvidos API");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }


            app.UseIpRateLimiting();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                if (env.IsDevelopment())
                {
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions { SourcePath = "ClientApp" },
                        npmScript: "serve",
                        regex: "Compiled successfully");
                }

                // Add MapRazorPages if the app uses Razor Pages. Since Endpoint Routing includes support for many frameworks, adding Razor Pages is now opt -in.
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}
