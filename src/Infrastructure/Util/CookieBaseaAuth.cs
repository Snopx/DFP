using Domain.Model;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Infrastructure.Util
{
    public class CookieBaseaAuth
    {
        public static ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim("FullName", user.Name),
                    new Claim(ClaimTypes.Role, string.Join(',',user.Roles.Select(m =>m.ID.ToString()).ToArray())),
                };
            user.AuthenticationType = DefaultAuthorizeAttribute.DefaultAuthenticationScheme;
            var claimsIdentity = new ClaimsIdentity(user);
            claimsIdentity.AddClaims(claims);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
