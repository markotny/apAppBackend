﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthServer.Data;
using AuthServer.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace AuthServer
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
            IdentityModelEventSource.ShowPII = true;    //for debugging

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // Register the Identity services.
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and entities.
                    options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>();
                })

                .AddServer(options =>
                {
                    // Register the ASP.NET Core MVC binder used by OpenIddict.
                    // Note: if you don't call this method, you won't be able to
                    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                    options.UseMvc();

                    // Enable the token endpoint (required to use the password flow).
                    options.EnableTokenEndpoint("/connect/token");

                    //either disable scope validation or register scopes https://github.com/openiddict/openiddict-core/issues/621
                    options.DisableScopeValidation();
                    //options.RegisterScopes(
                    //    OpenIddictConstants.Scopes.Roles,
                    //    OpenIdConnectConstants.Scopes.OpenId,
                    //    OpenIdConnectConstants.Scopes.Email,
                    //    OpenIdConnectConstants.Scopes.Profile,
                    //    OpenIdConnectConstants.Scopes.OfflineAccess);
                    // Allow client applications to use the grant_type=password flow.
                    options.AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    // During development, you can disable the HTTPS requirement.
                    options.DisableHttpsRequirement();

                    // Accept token requests that don't specify a client_id.
                    options.AcceptAnonymousClients();

                    options.UseJsonWebTokens();

                    //https://openiddict.github.io/openiddict-documentation/configuration/token-setup-and-validation.html#jwts
                    //JWTs must be signed by a self-signing certificate or a symmetric key
                    //Here a certificate is used. I used IIS to create a self-signed certificate
                    //and saved it in /FolderName folder. See below for .csproj configuration
                    //options.AddSigningCertificate(
                    //    assembly: typeof(Startup).GetTypeInfo().Assembly,
                    //    resource: "AppName.FolderName.certname.pfx",
                    //    password: "anypassword");

                    // not for production, use x509 certificate and .AddSigningCertificate()
                    options.AddSigningKey(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FOR TESTING ONLY")));
                });

            services.AddCors();

            //this must come after registering ASP.NET Core Identity
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //options.Authority = "http://localhost:443/";
                    //options.Audience = "ResourceServer";
                    options.RequireHttpsMetadata = false;
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        NameClaimType = OpenIdConnectConstants.Claims.Subject,
                        RoleClaimType = OpenIdConnectConstants.Claims.Role,
                        
                        //TODO: Change the JWT issuer server to use Certificate and add proper validation
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FOR TESTING ONLY")),
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(c =>
                c.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
