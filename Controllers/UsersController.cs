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

       //[HttpPost]
       //[Route("add")]
       //public IActionResult AddUser(AddUserDto requiredUser)
       // {
            
       // }

        

    }
}
