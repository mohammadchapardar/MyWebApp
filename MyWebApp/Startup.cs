using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebApp.Dto;
using MyWebApp.Models;
using MyWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("AppDb"));
            });
            AddAuthentication(services);
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("AppDb"));
            });

            services.AddIdentity<AppUser, AppRole>(option=> 
            {
                option.SignIn.RequireConfirmedAccount = false;
            
            }).AddEntityFrameworkStores<AuthDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Auth/Signin";
                option.AccessDeniedPath = "/Auth/AccessDedied";
                option.LogoutPath = "/Auth/Logout";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                option.Cookie.Name = "AuthCookie";
                option.SlidingExpiration = true;
                option.Cookie.HttpOnly = true;
            });
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

    }
}
