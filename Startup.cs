using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace App1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // configure identity server with in-memory stores, keys, clients and scopes
            services
            // .AddIdentityServer()
            // .AddIdentityServer(options => options.IssuerUri = "")
            .AddIdentityServer(options => options.IssuerUri = "http://10.0.2.2:5010")
            // .AddIdentityServer(options => options.IssuerUri = null)
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryClients(Config.GetClients())
            .AddTestUsers(Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // app.Use((context, next) =>
            // {
            //     if (context.Request.Host.Host == "10.0.2.2")
            //     {
            //         if (context.Request.Host.Port.HasValue)
            //         {
            //             context.Request.Host = new Microsoft.AspNetCore.Http.HostString("localhost", context.Request.Host.Port.Value);
            //         }
            //         else
            //         {
            //             context.Request.Host = new Microsoft.AspNetCore.Http.HostString("localhost");
            //         }
            //     }
            //     // context.Request.Scheme = "https";
            //     return next();
            // });



            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
