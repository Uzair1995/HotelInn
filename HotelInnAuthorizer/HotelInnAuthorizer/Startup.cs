using HotelInnAuthorizer.Repositories;
using HotelInnAuthorizer.Repositories.Models;
using HotelInnAuthorizer.Services;
using HotelInnAuthorizer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInnAuthorizer
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
            //Adding services
            services.AddMvc();
            services.AddControllers();
            services.AddRepositories(Configuration);
            services.AddServices(Configuration);

            //Adding DB Context
            services.AddDbContext<AuthorizerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            //Adding Microsoft identity for user management
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<AuthorizerDbContext>().AddDefaultTokenProviders();

            //Adding jwt authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudiences = Configuration["Jwt:Audience"].Split(',').ToList(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            //Adding swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelInnAuthorizer", Version = "v1" });
            });

            //Adding a default admin in the system
            ServiceProvider provider = services.BuildServiceProvider();
            IAccountService accountService = provider.GetRequiredService<IAccountService>();
            Task.Run(async () => await accountService.RegisterNewAccount(new Services.Models.RegisterAccount
            {
                Name = "admin",
                Role = "Admin",
                Password = "admin",
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelInnAuthorizer v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
