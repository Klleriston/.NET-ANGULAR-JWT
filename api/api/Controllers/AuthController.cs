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

        [HttpPost("/login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = _userRepository.GetUserByEmail(loginDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password)) return BadRequest(new { message = "Invalid credencials" });

            return Ok(user);

        }
    }
}
