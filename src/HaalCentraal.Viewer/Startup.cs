using HaalCentraal.Viewer.Helpers;
using HaalCentraal.Viewer.HttpHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace HaalCentraal.Viewer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenIdConnect(Configuration);
            services.AddAttributeBasedAccessControl();

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddTransient<BearerTokenHandler>();

            services.AddHttpClient("idp", client =>
            {
                client.BaseAddress = new Uri(Configuration["OpenIdConnect:authority"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            services.AddHttpClient("bag", client =>
            {
                client.BaseAddress = new Uri(new Uri(Configuration["HaalCentraalApiGatewayBaseUrl"]), "bag/");
            }).AddHttpMessageHandler<BearerTokenHandler>();
            services.AddHttpClient("brk", client =>
            {
                client.BaseAddress = new Uri(new Uri(Configuration["HaalCentraalApiGatewayBaseUrl"]), "brk/");
            }).AddHttpMessageHandler<BearerTokenHandler>();
            services.AddHttpClient("brp", client =>
            {
                client.BaseAddress = new Uri(new Uri(Configuration["HaalCentraalApiGatewayBaseUrl"]), "brp/");
            }).AddHttpMessageHandler<BearerTokenHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var forwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwardedHeaderOptions.KnownNetworks.Clear();
            forwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardedHeaderOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
