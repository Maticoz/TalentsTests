using Microsoft.AspNetCore.Mvc;

namespace TalentsApi.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet()]
        public async Task<ActionResult<string>> Get()
        {

            return Ok("Siema tu eniu");
        }
    }
}
