using System.Linq;
using Chart.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chart.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController: Controller
    {
        private readonly ApiContext _ctx;

        public OrderController( ApiContext ctx )
        {
            _ctx = ctx;
        }

        //GET api/order/pageNumber/pageSize
        [HttpGet( "{pageIndex:int}/{pageSize:int}" )]
        public IActionResult Get( int pageIndex ,int pageSize )
        {
            var data = _ctx.Orders.Include( o=> o.Customer )
                                  .OrderByDescending( c=> c.Placed );

            var page = new PaginatedResponse<Order>(data , pageIndex ,pageSize);

            var totalCount = data.Count();
            var totalPage = System.Math.Ceiling( (double)totalCount/pageSize );

            var res = new
            {
                Page = page,
                TotalPages = totalPage
            };
            return Ok( res );
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _ctx.Orders.Include( o=> o.Customer ).ToList();  //including -joining the two tables by the customer property

            var groupedResult = orders.GroupBy( o => o.Customer.State )
                                      .ToList()
                                      .Select( grp=> new{
                                        State = grp.Key , //key is group key: state
                                        Total = grp.Sum( x=> x.Total ) //sum of totals for each aggregated group
                                      })
                                      .OrderByDescending( res => res.Total )
                                      .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer( int n )
        {
            var orders = _ctx.Orders.Include( o=> o.Customer ).ToList();  //including -joining the two tables by the customer property

            var groupedResult = orders.GroupBy( o => o.Customer.Id )
                                      .ToList()
                                      .Select( grp=> new{
                                        Name = _ctx.Customers.Find(grp.Key).Name , 
                                        Total = grp.Sum( x=> x.Total ) 
                                      })
                                      .OrderByDescending( res => res.Total )
                                      .Take(n)
                                      .ToList();

            return Ok(groupedResult);
        }
    }
}