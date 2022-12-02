using GlidewellDentalService.Contexts;
using GlidewellDentalService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlidewellDentalService
{
    public class Startup
    {        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddDbContext<EmployeeDbContext>(o =>
            //{
            //    o.UseSqlServer(Configuration["ConnectionStrings:EmployeeDbConnectionString"]);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(opts =>
                {
                    opts.WithOrigins(new string[]
                    {
                        "http://localhost:8080"
                    });

                    opts.AllowAnyHeader();
                    opts.AllowAnyMethod();
                    opts.AllowCredentials();
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(op => {
                op.SwaggerEndpoint($"/swagger/v1/swagger.json", "Glidewell Dental API V1");
            });

            app.Use((context, next) =>
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                //context.Response.Headers["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS, DELETE, PUT";
                //context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Accept, Pragma, Cache-Control, Authorization, X-Requested-With";
                return next.Invoke();                
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
