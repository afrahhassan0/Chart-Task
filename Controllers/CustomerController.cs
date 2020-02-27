using System.Linq;
using Chart.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chart.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController: Controller
    {
        private readonly ApiContext _ctx;

        public CustomerController( ApiContext ctx )
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Customers.OrderBy( customer => customer.Id );

            return Ok(data);          
        }

        [HttpGet ("{id}" , Name ="GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _ctx.Customers.Find(id);

            if( customer == null )
            {
                return BadRequest();
            }

            return Ok(customer);

        }
    }
    
}