using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chart.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Column("CustomerId" , TypeName= "integer")]
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public DateTime Placed { get; set; } 
        public DateTime? Shipped { get; set; }
    }
}