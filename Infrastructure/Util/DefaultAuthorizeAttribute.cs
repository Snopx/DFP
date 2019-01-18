using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Util
{
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
    {
        public const string DefaultAuthenticationScheme = "DefaultAuthenticationScheme";
        public DefaultAuthorizeAttribute(string policy) : base(policy)
        {
        }
    }
}
