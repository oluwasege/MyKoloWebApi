using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKoloWebApi.Models
{
    public class Expense
    {
        //id,amount,description,createdDate,lastModifiedDate

        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; }
        
    }
}
