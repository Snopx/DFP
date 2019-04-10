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
        public static ClaimsPrincipal GetClaimsPrincipal(UserModel user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.Name),
                    new Claim(ClaimTypes.Role, string.Join(',',user.Roles.Select(m =>m.ID.ToString()).ToArray())),
                };
            user.AuthenticationType = DefaultAuthorizeAttribute.DefaultAuthenticationScheme;
            var claimsIdentity = new ClaimsIdentity(user);
            claimsIdentity.AddClaims(claims);
            return new ClaimsPrincipal(claimsIdentity);
        }

        public static Microsoft.AspNetCore.Authentication.AuthenticationProperties AuthenticationProperties
        {
            get
            {
                return new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    //AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    RedirectUri = "/User/Login"
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };
            }
        }
    }
}
