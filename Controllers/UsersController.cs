using Microsoft.AspNetCore.Mvc;
using MyKoloWebApi.Data;
using MyKoloWebApi.DTOs;
using MyKoloWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoloWebApi.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
       [HttpPost]
       public IActionResult AddUser(AddUserDto requiredUser)
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
            return Ok(user.Id);
        }

        

    }
}
