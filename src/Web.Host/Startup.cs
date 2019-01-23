using System;
using System.Collections.Generic;
using System.IO;
using Infrastructure.Data;
using Infrastructure.ioC;
using Infrastructure.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Web.Host
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// construction of Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtTokenHelper.SetConfiguration(configuration);
        }
        /// <summary>
        /// Get the Configuration (DI)
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// add service to collection
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //TODO need to modify Role角色需要用枚举定义
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireRole("User").Build());//普通用户
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());//管理员
                options.AddPolicy("AdminOrUser", policy => policy.RequireRole("Admin", "User").Build());//均可
            });
            AuthConfigurer.Configure(services, Configuration);
            #region Swagger
            services.AddSwaggerGen(o =>
            {
                var title = "DF's Blog API";
                o.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = title,
                    Description = "框架说明文档",
                    Contact = new Contact { Name = "DarrenFang", Email = "darrenfang94@gmail.com", Url = "https://github.com/Snopx" }
                });
                string basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Web.Host.xml");
                o.IncludeXmlComments(xmlPath,true);

                #region Token绑定到ConfigureServices
                var IssuerName = Configuration["Authentication:JwtBearer:Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                o.AddSecurityRequirement(security);

                o.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
            });
            #endregion
            services.AddDbContext<DFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), builder => builder.MigrationsAssembly("Infrastructure")));
            return IocConfiguration.UseIoc(services);
        }


        /// <summary>
        /// configure the http pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api V1");
                c.RoutePrefix = "swagger";
            });
            #endregion
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
