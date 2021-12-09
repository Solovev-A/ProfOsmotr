using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Services;
using System.Threading.Tasks;

namespace ProfOsmotr.Web
{
    public class Startup
    {
        private const string GLOBAL_AUTH_POLICY_NAME = "AuthenticatedAndNotBanned";
        private readonly bool isAuthorizationEnabled;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            isAuthorizationEnabled = bool.Parse(Configuration["AuthorizationEnabled"] ?? bool.TrueString);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/User/Login");
                    options.AccessDeniedPath = new PathString("/Home/AccessDenied");
                    options.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = (context) => SendStatusCodeForApiRequest(context, 401),
                        OnRedirectToAccessDenied = (context) => SendStatusCodeForApiRequest(context, 403)
                    };
                });

            if (isAuthorizationEnabled)
            {
                services.AddTransient<IAuthorizationHandler, NotBannedHandler>();
                services.AddAuthorization(auth => auth.AddPolicy(GLOBAL_AUTH_POLICY_NAME,
                                            policy => policy.Requirements.Add(new NotBannedRequirement())));
            }
            else
            {
                services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();
            }

            services.AddControllersWithViews(options =>
            {
                if (isAuthorizationEnabled)
                {
                    options.Filters.Add(new AuthorizeFilter(GLOBAL_AUTH_POLICY_NAME));
                }
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new PatchRequestContractResolver();
                });

            services.AddMemoryCache();
            services.AddProfOsmotr(connection, isAuthorizationEnabled);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Profosmotr Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public, max-age=31536000");
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Profosmotr V1");
            });

            app.UseEndpoints(endpoints =>
            {
                var action = isAuthorizationEnabled ? "About" : "Index";
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: $"{{controller=Home}}/{{action={action}}}/{{id?}}");

                endpoints.MapSwagger();
            });
        }

        private Task SendStatusCodeForApiRequest(RedirectContext<CookieAuthenticationOptions> context, int statusCode)
        {
            if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == 200)
                context.Response.StatusCode = statusCode;
            else
                context.Response.Redirect(context.RedirectUri);

            return Task.CompletedTask;
        }
    }
}