using System;
using System.Collections.Generic;
using System.Linq;
using Chart.Api.Models;

namespace Chart.Api
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;

        public DataSeed( ApiContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedData( int nCustomers , int nOrders )
        {
            if( !_ctx.Customers.Any() )
            {
                SeedCustomers(nCustomers);
               
            }
             _ctx.SaveChanges(); //commits the transaction in db

             if( !_ctx.Shippers.Any() )
            {
                SeedShippers();
                _ctx.SaveChanges(); 
            }
            Console.WriteLine( _ctx.Customers );

             if( !_ctx.Orders.Any() )
            {
                SeedOrders(nOrders);
                _ctx.SaveChanges(); 
            }
            
            
        }

        public void SeedCustomers(int nCustomers)
        {
            List<Customer> customers = BuildCustomerList(nCustomers);

            foreach( var customer in customers )
            {
                _ctx.Customers.Add( customer );
            }
        }

        public List<Customer> BuildCustomerList(int nCustomers)
        {
            var customer = new List<Customer>();

            for( var i=1 ; i<= nCustomers ; i++)
            {
                var customerName = Helper.MakeRandomName();
                var customerState = Helper.MakeRandomState();

                customer.Add(new Customer
                {
                   Id =i,
                   Name= customerName,
                   Email = Helper.MakeRandomEmail(customerName),
                   State = customerState
                });
            }

            return customer;
        }

        private void SeedOrders(int nOrders)
        {
            List<Order> orders = BuildOrdersList(nOrders);

            foreach( var order in orders )
            {
                _ctx.Orders.Add(order);
            }

                        
        }

        private List<Order> BuildOrdersList(int nOrders)
        {
            var orders = new List<Order>(nOrders);
            var rand = new Random();

            for ( var i=1 ; i<= nOrders ; i++ )
            {
                var placed = Helper.GetRandomPlaced();
                var completed = Helper.GetRandomCompleted( placed );

                var randomCustomerId = rand.Next(1, _ctx.Customers.Count());

                orders.Add(new Order{
                    Id =i,
                    Customer = _ctx.Customers.FirstOrDefault( c =>  c.Id == randomCustomerId),
                    Total = Helper.GetRandomTotal(),
                    Placed = placed,
                    Shipped = completed
                });
            }

            return orders;
        }

        private void SeedShippers()
        {
            List<Shipper> servers = BuildServerList();

            foreach( var server in servers )
            {
                _ctx.Shippers.Add(server);
            }
        }

        private List<Shipper> BuildServerList()
        {
            return new List<Shipper>()
            {
                new Shipper{
                    Id =1,
                    Name = "Mail Service",
                    IsOnline = true
                },
                new Shipper{
                    Id =2,
                    Name = "Shipping Service",
                    IsOnline = false
                },
                new Shipper{
                    Id =3,
                    Name = "Web Service",
                    IsOnline = true
                },
                new Shipper{
                    Id =4,
                    Name = "Prod-mail",
                    IsOnline = true
                },
                new Shipper{
                    Id =5,
                    Name = "Services",
                    IsOnline = false
                },
                new Shipper{
                    Id =6,
                    Name = "Service 6",
                    IsOnline = true
                },
                new Shipper{
                    Id =7,
                    Name = "Service 7",
                    IsOnline = false
                },
                new Shipper{
                    Id =8,
                    Name = "Service 8",
                    IsOnline = true
                }

            };

        }

    }
}