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

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
