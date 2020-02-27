using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chart.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Chart.Api;

namespace chart_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //database service
            services.AddDbContext<ApiContext>( opt => opt.UseNpgsql( Configuration.GetConnectionString( "ApiContext" )));

            services.AddMvc();
            services.AddTransient<DataSeed>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , DataSeed seed )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            seed.SeedData( 20 , 200 );

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseMvc( routes => routes.MapRoute(
            //     "default", "api/{controller}/{action}/{id}"
            // ));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
