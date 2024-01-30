using api.Data;
using api.DTO;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/register")]
        public IActionResult Register(RegisterDTO dTO)
        {
            var user = new User
            {
                Name = dTO.Name,
                Email = dTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dTO.Password)
            };
            return Created("sucess", _userRepository.Create(user));
        }
    }
}
