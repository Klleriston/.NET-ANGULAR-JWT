using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return Ok("Sucess");
        }
    }
}
