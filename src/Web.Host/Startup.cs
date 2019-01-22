using System;
using System.IO;
using Infrastructure.Data;
using Infrastructure.ioC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())).SetCompatibilityVersion(CompatibilityVersion.Latest);
            AuthConfigurer.Configure(services, Configuration);
            services.AddDbContext<DFDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default"), builder => builder.MigrationsAssembly("Infrastructure")));

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "TestDoc",
                    Version = "v1",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "DarrenFang", Email = "258700581@qq.com" }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Web.Host.xml");
                o.IncludeXmlComments(xmlPath);
            });
            services.AddMvcCore().AddApiExplorer();
            return IocConfiguration.UseIoc(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ShowExtensions();
                c.EnableValidator();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "test V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
