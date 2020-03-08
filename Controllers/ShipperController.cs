using System.Linq;
using Chart.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chart.Api.Controllers
{
    [Route("api/[controller]")]
    public class ShipperController : Controller
    {
        private readonly ApiContext _ctx;

        public ShipperController( ApiContext ctx )
        {

            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _ctx.Shippers.OrderBy( s => s.Id ).ToList();

            return Ok(res);
        }

        [HttpGet ( "id" , Name="GetShipper" )]
        public IActionResult Get( int id)
        {
            var res = _ctx.Shippers.Find( id );

            if( res == null )
            {
                return BadRequest();
            }

            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult Message( int id , [FromForm] ShipperMessage msg )
        {
            var shipper = _ctx.Shippers.Find(id);

            if( shipper==null )
            {
                return NotFound();
            }
           

           shipper.IsOnline = ( msg.Payload == "activate" )? true:false;

            _ctx.SaveChanges();

            return Ok( shipper );            

        }
    }
}