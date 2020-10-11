using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.OpenApi.Models;
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
            services.ConfigureJWT(Configuration);
            #region Swagger
            services.AddSwaggerGen(o =>
            {
                var title = "DF's Blog API";
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = title,
                    Description = "API文档",
                    Contact = new OpenApiContact { Name = "DarrenFang", Email = "darrenfang94@gmail.com", Url = "https://github.com/Snopx" }
                });
                string basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Web.Host.xml");
                o.IncludeXmlComments(xmlPath, true);

                #region Token绑定到ConfigureServices
                var IssuerName = Configuration["Authentication:JwtBearer:Issuer"];
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                o.AddSecurityRequirement(new OpenApiSecurityScheme { });

                o.AddSecurityDefinition(IssuerName, new OpenApiSecurityScheme
                {
                    Description = "Input 'Bearer {token}'",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
            #endregion
            #region CORS
            services.AddCors(c =>
            {
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                //↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑


                //一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://localhost:8020", "http://blog.core.xxx.com")//支持多个域名端口
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();//标头添加到策略
                });

            });
            #endregion
            AutomapperHelper.RegisterMappings();
            services.AddAutoMapper();
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
            app.UseCors("LimitRequests");
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
