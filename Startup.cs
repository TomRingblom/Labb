using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.Security;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Labb;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;

    public Startup(IWebHostEnvironment webHostingEnvironment)
    {
        _webHostingEnvironment = webHostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

        services
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCms()
            .AddAdminUserRegistration()
            .AddEmbeddedLocalization<Startup>();

        services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "azure-cookie";
            options.DefaultChallengeScheme = "azure";
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Events.OnSignedIn = async ctx =>
            {
                if (ctx.Principal?.Identity is ClaimsIdentity claimsIdentity)
                {
                    // Syncs user and roles so they are available to the CMS
                    var synchronizingUserService = ctx
                        .HttpContext
                        .RequestServices
                        .GetRequiredService<ISynchronizingUserService>();

                    await synchronizingUserService.SynchronizeAsync(claimsIdentity);
                }
            };
        })
        .AddOpenIdConnect("azure", options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.CallbackPath = "/signin-oidc";
            options.UsePkce = true;

            // If Azure AD is register for multi-tenant
            //options.Authority = "https://login.microsoftonline.com/" + "common" + "/v2.0";
            options.Authority = "https://login.microsoftonline.com/" + "e7c989d7-a43c-4fff-9418-a487ec311ffd" + "/v2.0";
            options.ClientId = "05d0ac35-a66d-4d1e-ba3d-a6bd9814a4bd";
            options.ClientSecret = "lV68Q~bQ4Ms8NTmVtYEwonZULXHA1iSHyjbJJcCm";

            options.Scope.Clear();
            options.Scope.Add(OpenIdConnectScope.OpenId);
            options.Scope.Add(OpenIdConnectScope.OfflineAccess);
            options.Scope.Add(OpenIdConnectScope.Email);
            options.MapInboundClaims = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = "preferred_username",
                ValidateIssuer = false
            };

            options.Events.OnRedirectToIdentityProvider = ctx =>
            {
                // Prevent redirect loop
                if (ctx.Response.StatusCode == 401)
                {
                    ctx.HandleResponse();
                }

                return Task.CompletedTask;
            };

            options.Events.OnAuthenticationFailed = context =>
            {
                context.HandleResponse();
                context.Response.BodyWriter.WriteAsync(Encoding.ASCII.GetBytes(context.Exception.Message));
                return Task.CompletedTask;
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
        });
    }
}
