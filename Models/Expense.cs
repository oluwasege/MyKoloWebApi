using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyKoloWebApi.Models
{
    public class Expense
    {
        //id,amount,description,createdDate,lastModifiedDate
      
        [Key]
        public string Id { get; set; }

        public virtual User User { get; set; }
        
        public string UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifiedDate { get; set; }
        
    }
}
