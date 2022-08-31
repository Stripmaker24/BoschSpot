using BoschSpot.Data;
using BoschSpot.Data.Repositories;
using BoschSpot.Data.Repositories.Interfaces;
using BoschSpot.Utilities.Accounts;
using BoschSpot.Utilities.Geolocation;
using BoschSpot.Utilities.Spots;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot
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
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/Account/Login";
                });
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            services.AddScoped<IContenderRepo, ContenderRepo>();
            services.AddScoped<IGroupRepo, GroupRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ISpotRepo, SpotRepo>();
            services.AddScoped<IRarityRepo, RarityRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IGeoService, GeoService>();
            services.AddScoped<ISpotService, SpotService>();
            services.AddScoped<IAccountService, AccountService>();
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
