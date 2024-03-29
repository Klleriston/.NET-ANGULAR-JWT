﻿using api.Data;
using api.DTO;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtServices _jwtService;

        public AuthController(IUserRepository userRepository, JwtServices jwtServices)
        {
            _userRepository = userRepository;
            _jwtService = jwtServices;
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
            return Created("Sucesso !", _userRepository.Create(user));
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = _userRepository.GetUserByEmail(loginDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(
                loginDTO.Password, user.Password
                ))
                return BadRequest(new
                {
                    message = "Credenciais Invalidas!"
                });

            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "Sucesso !"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);
                int userID = int.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userID);

                return Ok(user);
            } catch (Exception ex)
            {
               return Unauthorized(new
               {
                   message = "Sem autorização"
               });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "Você saiu"
            }); 
        }
    }
}
