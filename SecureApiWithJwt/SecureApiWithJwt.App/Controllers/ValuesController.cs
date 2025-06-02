using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureApiWithJwt.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("I am root for everyone!");
        }

        [HttpGet("/admin")]
        [Authorize("AdminPolicy")]
        public IActionResult GetAdmin([FromRoute] string token)
        {
            //here you have decode token and check if required role exists
            return Ok("I am root for admin!");
        }

        [HttpGet("/claim")]
        [Authorize("CourseraPolicy")]
        public IActionResult GetClaim([FromRoute] string token)
        {
            //here you have to decode token and check if required claim exists
            return Ok("I am root for claim!");
        }
    }
}
