using Case.BLL.Repository;
using Case.BLL.Services;
using Case.DAL.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CaseProject
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
            services.AddControllersWithViews();

            services.AddDbContext<ProjectContext>(options => options.UseSqlServer("Server=LAPTOP-V4L4J7P2;Database=caseDB;Trusted_Connection=True;"));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ProjectContext>();


            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters =
                   "abcçdefgðhiýjklmnoöpqrstuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSTUÜVWXYZ0123456789";
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireLoggedIn", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });



            services.AddRazorPages();

            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IFieldService, FieldService>();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=LoginAndRegister}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
