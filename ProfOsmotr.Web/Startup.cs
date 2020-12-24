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
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Services;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProfOsmotr.Web
{
    public class Startup
    {
        private const string GLOBAL_AUTH_POLICY_NAME = "AuthenticatedAndNotBanned";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            services.AddTransient<IAuthorizationHandler, NotBannedHandler>();
            services.AddAuthorization(auth => auth.AddPolicy(GLOBAL_AUTH_POLICY_NAME,
                                        policy => policy.Requirements.Add(new NotBannedRequirement())));

            services.AddControllersWithViews(options => options.Filters.Add(new AuthorizeFilter(GLOBAL_AUTH_POLICY_NAME)))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddMemoryCache();
            services.AddProfOsmotr(connection);
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=About}/{id?}");
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