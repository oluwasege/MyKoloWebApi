using Microsoft.AspNetCore.Mvc;
using MyKoloWebApi.Data;
using MyKoloWebApi.DTOs;
using MyKoloWebApi.Models;
using MyKoloWebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoloWebApi.Controllers
{
    public class AuthController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(AddUserDto requiredUser)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = requiredUser.UserName,
                Password = requiredUser.Password,
                Email = requiredUser.Email
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user.UserName);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult AuthenticateUser([FromBody]LoginDto loginCreds)
        {
            User foundUser = _context.Users.FirstOrDefault(c => c.Email.ToLower() == loginCreds.Email.ToLower());
            if(foundUser==null)
            {
                return Unauthorized("Invalid Email or Password");
            }
            else
            {
                if(foundUser.Password==loginCreds.Password)
                {
                    return Ok(JwtUtils.WriteToken("valid issuer", "valid audience", "my secret", DateTime.Now.AddMinutes(2)));
                }
                else
                {
                    return Unauthorized("Invalid Email or Password");
                }
            }
            
            return NoContent();
        }

    }
}
