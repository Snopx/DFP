using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Host.Controllers
{
    /// <summary>
    /// 测试项目
    /// </summary>
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

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="aa">AAA</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Post(string aa)
        {
            return aa;
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
