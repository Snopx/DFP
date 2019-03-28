using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// teadsf s 
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public JsonResult Login()
        {
            return new JsonResult(JwtTokenHelper.CreateTokenByHandler("df", "Admin", 5));
        }
    }
}
