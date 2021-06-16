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
    [Route("Expenses")]
    public class ExpensesController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpPost]
        public IActionResult AddExpense(AddExpenseDto requestBody)
        {
            //amount,description
            Expense expense = new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = requestBody.UserId,
                Amount = requestBody.Amount,
                Description=requestBody.Description      
            };

            _context.Set<Expense>().Add(expense);
            _context.SaveChanges();
            return Ok(expense.Id);
            
        }

            
    }
}
