using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InvoiceManagement.Repositories;
using InvoiceManagement.Services;
using FluentValidation.AspNetCore;
using InvoiceManagement.Validations;
using InvoiceManagement.Models;
using FluentValidation;

namespace InvoiceManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            // CORS Policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IValidator<Invoice>, InvoiceValidator>();
            services.AddScoped<IValidator<Product>, ProductValidator>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoiceManagement v1"));
            }

            app.UseRouting();

            // Enable CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
