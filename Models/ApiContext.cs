using Microsoft.EntityFrameworkCore;

namespace Chart.Api.Models
{
    public class ApiContext: DbContext
    {
        public ApiContext( DbContextOptions<ApiContext> options): base(options){ }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        
    }
}