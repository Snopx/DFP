using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Util
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor HttpContextAccessor { get; set; }

        public static HttpContext CurrentHttpContext => HttpContextAccessor.HttpContext;

        public static string LoginUserName
        {
            get
            {
                return CurrentHttpContext.User.Claims.Where(x => x.Type.Equals("FullName", StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
            }
        }

        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }


        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            HttpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            return app;
        }
    }
}
