using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Infrastructure.Util
{
    public class JwtAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private JwtBearSetting _jwtBearSetting;
        public JwtAuthorizeMiddleware(RequestDelegate next, JwtBearSetting jwtBearSetting)
        {
            _next = next;
            _jwtBearSetting = jwtBearSetting;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var result = httpContext.Request.Headers.TryGetValue("Authorization",out StringValues auth);
            if (result || string.IsNullOrWhiteSpace(auth))
            {
                throw new UnauthorizedAccessException("未授权");
            }
            result = JwtTokenHelper.Validate(auth.ToString().Substring("Bearer ".Length).Trim(), payLoad =>
            {
                var success = true;
                success = success && payLoad["aud"]?.ToString() == _jwtBearSetting.Audience;
                success = success && payLoad["iss"]?.ToString() == _jwtBearSetting.Issuer;
                if (success)
                {
                    //将用户信息存入app;
                }
                return success;
            });
            if(!result) throw new UnauthorizedAccessException("未授权");
            //TODO 权限验证
            await _next(httpContext);
        }
    }
}
