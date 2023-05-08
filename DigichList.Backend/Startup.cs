using DigichList.Backend.Helpers;
using DigichList.Backend.Middlewares;
using DigichList.Backend.Options;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Repositories;
using DigichList.Infrastructure.Seeders;
using DigichList.TelegramNotifications.BotNotifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace DigichList.Backend
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
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();
            services.AddLogging(configure => configure.AddConsole());
            services.AddScoped<IDefectImageRepository, DefectImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDefectRepository, DefectRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBotNotificationSender, BotNotificationSender>();
            services.AddScoped<JwtService>();
            services.AddDbContext<DigichListContext>();

            var authOptionsCifiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authOptionsCifiguration);

            services.AddAutoMapper(typeof(Startup));
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
            .WithOrigins(
                "https://localhost:3000",
                "http://localhost:3000",
                "https://localhost:44379",
                "http://127.0.0.1:5500",
                "https://digichlistbackend.herokuapp.com",
                "https://digich-list-frontend.herokuapp.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            );

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
