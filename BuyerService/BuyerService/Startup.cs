using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BUYERDBENTITY.Entity;
using BUYERDBENTITY.Repositories;
using BuyerService.GlobalException;
using BuyerService.Manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;

namespace BuyerService
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
            services.AddDbContext<BuyerdataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connectionstring")));
            services.AddTransient<IBuyerManager, BuyerManager>();
            services.AddTransient<IBuyerRepository, BuyerRepository>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
               options.AllowAnyOrigin()
                      .AllowAnyMethod()
                     .AllowAnyHeader()
               );

            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BIG BASKET", Version = "v1" });
            });
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(CustomExceptionFilter));
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BIG BASKET");
            });
        }
    }
}
