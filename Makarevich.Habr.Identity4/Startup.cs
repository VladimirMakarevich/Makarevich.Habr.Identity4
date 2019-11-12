using Makarevich.Habr.Identity4.Data;
using Makarevich.Habr.Identity4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Makarevich.Habr.Identity4
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    b =>
                    {
                        b.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer(o =>
            //{
            //    //o.UserInteraction.LoginUrl = "/account/login";
            //    //o.UserInteraction.LoginReturnUrlParameter = "returnUrl";
            //    //o.UserInteraction.LogoutUrl = "/account/logout";
            //})
            //    .AddInMemoryIdentityResources(Config.Ids)
            //    .AddInMemoryApiResources(Config.Apis)
            //    .AddInMemoryClients(Config.Clients)
            //    .AddTestUsers(TestUsers.Users)
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            //            builder.AddDeveloperSigningCredential();

            services.AddIdentityServer(o =>
            {
                o.UserInteraction.LoginUrl = "/account/login";
                o.UserInteraction.LoginReturnUrlParameter = "returnUrl";
                o.UserInteraction.LogoutUrl = "/account/logout";
            }).AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            services.AddAuthentication().AddIdentityServerJwt();

            services.AddControllers();
//            services.AddRazorPages();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
//                endpoints.MapRazorPages();
            });

            //app.UseCors();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}