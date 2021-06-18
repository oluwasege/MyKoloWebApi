using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    [Route("Expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public ExpensesController(ApplicationDbContext context)
        {
            //This is just to give access to the database
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

        //[HttpGet]
        //public IActionResult GetAllUserExpenses()
        //{
        //    List<ViewExpenseDto> Expenses = new List<ViewExpenseDto>();
        //    List<Expense> Data = _context.Expenses.ToList();
        //    if(Data.Count==0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        foreach (var expense in Data)
        //        {
        //            Expenses.Add(new ViewExpenseDto()
        //            {
        //                Amount = expense.Amount,
        //                Description=expense.Description
        //            });
                    
        //        }
        //    }
        //    return Ok(Expenses);

        //}


        [HttpGet]
        public IActionResult GetAllExpensesByUserId([FromQuery]string userId)
        {
            
            List<ViewExpenseDto> foundExpenses = new List<ViewExpenseDto>();
            List<Expense> Expenses = _context.Expenses.Where(expense => expense.UserId == userId).ToList();
            if(Expenses.Count==0)
            {
                return NotFound();
            }
            else
            {
               
                foreach (var expense in Expenses)
                {
                    foundExpenses.Add(new ViewExpenseDto()
                    {
                        Id = expense.Id,
                        Amount = expense.Amount,
                        Description = expense.Description
                    });
                }
            }


            return Ok(foundExpenses);
        }

        [HttpPut]
        public IActionResult UpdateExpensesByUser([FromQuery]string userId,[FromBody]List<UpdateExpenseDto> expensesToUpdate)
        {

            User attemptingUser = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (attemptingUser!=null)
            {
                List<Expense> oldExpenses = _context.Expenses.Where(y=>y.UserId==attemptingUser.Id).ToList();
                foreach (var expense in expensesToUpdate)
                {
                    Expense dbExpense = oldExpenses.FirstOrDefault(c => c.Id == expense.Id);
                    dbExpense.Description = expense.Description;
                    dbExpense.Amount = expense.Amount;
                    
                }
                _context.SaveChanges();
                return NoContent();
            }
           
            else
            {
                return Unauthorized();
            }          
        }

        [HttpDelete]
        public IActionResult DeleteUserExpenses([FromQuery]string userId ,[FromBody]List<string>expensesIdToDelete)
        {
            List<Expense> userExpenses = new List<Expense>();
            userExpenses = _context.Expenses.Where(c => c.UserId == userId && expensesIdToDelete.Contains(c.Id)).ToList();
            if(userExpenses!=null)
            {
                _context.Expenses.RemoveRange(userExpenses);
                _context.SaveChanges();                
            }
            return NoContent();
        }
        //[HttpGet("Id")]
        //public IActionResult GetExpenseWithExpenseId(GetExpenseWithId getBody)
        //{
        //    List<Expense> Expenses = null;
        //    List<ViewExpenseDto> AllExpenses = new List<ViewExpenseDto>();
        //    Expenses = _context.Expenses.ToList();
        //    ViewExpenseDto ViewAllExpenses = new ViewExpenseDto();
        //    foreach(var expense in Expenses)
        //    {
        //        if(getBody.Id==expense.Id)
        //        {
        //            ViewAllExpenses.Amount = expense.Amount;
        //            ViewAllExpenses.Description = expense.Description;
        //        }
                
        //    }
        //    if(ViewAllExpenses==null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(ViewAllExpenses);
        //}
    }
}
