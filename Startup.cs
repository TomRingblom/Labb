using EPiServer.Scheduler;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Geta.NotFoundHandler.Infrastructure.Configuration;
using Geta.NotFoundHandler.Infrastructure.Initialization;
using Geta.NotFoundHandler.Optimizely.Infrastructure.Configuration;
using Geta.NotFoundHandler.Optimizely.Infrastructure.Initialization;
using Labb.Infrastructure.Display;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Labb;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;
    private readonly IConfiguration _configuration;

	public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
	{
		_webHostingEnvironment = webHostingEnvironment;
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

		services.AddMvc(o =>
		{
			o.Conventions.Add(new FeatureConvention());
		})
		.AddRazorOptions(ro =>
		{
			ro.ViewLocationFormats.Add("/Features/{0}.cshtml"); //"{0}" represents the path "Components/{View Component Name}/{View Name}"
			ro.ViewLocationFormats.Add("/Features/{0}/Default.cshtml"); //"{0}" represents the View Component Name"
			ro.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
		});

		services.AddCms();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
		})
	    .AddCookie()
	    .AddOpenIdConnect(options =>
	    {
		    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.ClientId = "80b9830b-4285-4427-9c4b-6f4e0fcfa5a1";
			options.Authority = "https://login.microsoftonline.com/" + "e7c989d7-a43c-4fff-9418-a487ec311ffd" + "/v2.0";
		    options.CallbackPath = "/signin-oidc";
		    options.Scope.Add("email");

		    options.TokenValidationParameters = new TokenValidationParameters
		    {
			    ValidateIssuer = false,
			    RoleClaimType = ClaimTypes.Role,
			    NameClaimType = "preferred_username"
			};

		    options.Events.OnAuthenticationFailed = context =>
		    {
			    context.HandleResponse();
			    context.Response.BodyWriter.WriteAsync(Encoding.ASCII.GetBytes(context.Exception.Message));
			    return Task.FromResult(0);
		    };

		    options.Events.OnTokenValidated = (ctx) =>
		    {
			    var redirectUri = new Uri(ctx.Properties.RedirectUri, UriKind.RelativeOrAbsolute);
			    if (redirectUri.IsAbsoluteUri)
			    {
				    ctx.Properties.RedirectUri = redirectUri.PathAndQuery;
			    }
			    //    
			    //Sync user and the roles to EPiServer in the background
			    ServiceLocator.Current.GetInstance<ISynchronizingUserService>().SynchronizeAsync(ctx.Principal.Identity as ClaimsIdentity);
			    return Task.FromResult(0);
		    };
	    });

        services.AddNotFoundHandler(o =>
        {
            o.UseSqlServer(_configuration.GetConnectionString("EPiserverDB"));
			//o.BufferSize = 30;
			//o.ThreshHold = 5;
			o.HandlerMode = FileNotFoundMode.On;
			//o.IgnoredResourceExtensions = new[] { "jpg", "gif", "png", "css", "js", "ico", "swf", "woff" };
			//o.Logging = LoggerMode.On;
			//o.LogWithHostname = false;
			//o.AddProvider<NullNotFoundHandlerProvider>();
		});

        services.AddOptimizelyNotFoundHandler(o =>
        {
            o.AutomaticRedirectsEnabled = true;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseNotFoundHandler();
        app.UseStatusCodePagesWithReExecute("/error/{0}");
		app.UseOptimizelyNotFoundHandler();

		if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

		app.Map("/util/login", builder => builder.Run(context =>
		{
			context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
			{
				RedirectUri = "/"
			});
			return Task.CompletedTask;
		}));

		app.Map("/util/logout", builder => builder.Run(context =>
		{
			context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
			return Task.CompletedTask;
		}));

		app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
            endpoints.MapRazorPages();
        });
    }
}
