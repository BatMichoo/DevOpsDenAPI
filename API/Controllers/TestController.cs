using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok("Test is working");
        }
    }
}
