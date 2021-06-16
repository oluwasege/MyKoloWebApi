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
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        List<Expense> Expenses = null;
        List<ViewAllExpensesDto> AllExpenses = new List<ViewAllExpensesDto>();
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
                Description = requestBody.Description
            };

            _context.Set<Expense>().Add(expense);
            _context.SaveChanges();
            return Ok(expense.Id);

        }

        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            
            Expenses = _context.Expenses.ToList();
            if (Expenses.Count == 0)
            {
                return NotFound();
            }
            foreach(var expense in Expenses)
            {
                ViewAllExpensesDto ViewAllExpenses = new ViewAllExpensesDto()
                {
                    Amount = expense.Amount,
                    Description = expense.Description
                };
                AllExpenses.Add(ViewAllExpenses);
            }
            return Ok(AllExpenses);

        }


        [HttpGet("Id")]
        public IActionResult GetExpenseWithId(GetExpenseWithId getBody)
        {
            Expenses = _context.Expenses.ToList();
            ViewAllExpensesDto ViewAllExpenses = new ViewAllExpensesDto();
            foreach(var expense in Expenses)
            {
                if(getBody.Id==expense.Id)
                {
                    ViewAllExpenses.Amount = expense.Amount;
                    ViewAllExpenses.Description = expense.Description;
                }
                
            }
            if(ViewAllExpenses==null)
            {
                return NotFound();
            }
            return Ok(ViewAllExpenses);
        }
    }
}
