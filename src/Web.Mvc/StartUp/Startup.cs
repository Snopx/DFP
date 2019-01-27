using Application.AutoMapper;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.ioC;
using Infrastructure.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Web.Mvc.StartUp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtTokenHelper.SetConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region Cookie保护政策 ban
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            #endregion

            //自动验证防伪标签 (全局)  亦可用 ValidateAntiForgeryToken 在控制器或控制器下的方法使用。 如果不需要使用可以使用 IgnoreAntiforgeryToken 特性标注
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AuthConfigurer.ConfigureCookieBase(services, Configuration);
            services.AddAutoMapper();

            services.AddDbContext<DFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            return IocConfiguration.UseIoc(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
            }
            //app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAuthentication();
            AutomapperHelper.RegisterMappings();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
