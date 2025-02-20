using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : BaseController
    {
        private readonly DenDbContext _dbContext;
        public TestController(DenDbContext denDbContext)
        {
            _dbContext = denDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool status = _dbContext.Database.CanConnect();
            bool isCreated = await _dbContext.Database.EnsureCreatedAsync();


            return Ok("Test is working");
        }
    }
}
