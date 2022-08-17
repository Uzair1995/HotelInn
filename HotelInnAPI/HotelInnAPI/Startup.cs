using HotelInn.Persistence.Repositories;
using HotelInn.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace HotelInnAPI
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
            //Configuring the logger
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            //Register all classes in the IOC
            RegisterClasses(services);

            //Adding controllers from presentation library
            services.AddControllers().AddApplicationPart(typeof(HotelInn.Presentation.AssemblyReference).Assembly);

            //Adding swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelInnAPI", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Added Serilog in the middleware for logging.
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelInnAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>
        /// Private method for registring all the services and repositories.
        /// </summary>
        /// <param name="services"></param>
        private void RegisterClasses(IServiceCollection services)
        {
            services.AddTransient(typeof(Lazy<>), typeof(LazyInstance<>));

            //Register services and repositories modules in DI container
            services.RegisterServices();
            services.RegisterRepositories(Configuration);
        }
    }
}
