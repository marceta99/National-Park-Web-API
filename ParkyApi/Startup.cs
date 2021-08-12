using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkyApi.Data;
using ParkyApi.Models.Repository.IRepository;
using AutoMapper;
using ParkyApi.Mapper;

namespace ParkyApi
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

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<INationalParkRepository, NatonalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>(); 

            services.AddAutoMapper(typeof(ParkyMappings));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ParkyOpenAPINationalPark",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Parky API National Park",
                        Version = "1",
                        Description="Api about National Parks"
                    }
                    );
             

            });

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options=> {
                options.SwaggerEndpoint("/swagger/ParkyOpenAPINationalPark/swagger.json", "Parky API NP");
               // options.SwaggerEndpoint("/swagger/ParkyOpenAPITrails/swagger.json", "Parky API Trails");
                options.RoutePrefix = ""; 
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
