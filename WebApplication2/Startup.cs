using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BusinessLayer.Models;
using BusinessLayer.Profiles;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)////
        {
            var smtpOptions = Configuration.GetSection("SmtpOptions");
            services.Configure<SmtpOptions>(smtpOptions);

            var hashSettings = Configuration.GetSection("HashSettings");
            services.Configure<HashSettings>(hashSettings);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            services.RegisterServices();

            services.AddControllers();

            var assemblies = new[]
            {
              Assembly.GetAssembly(typeof(WeatherForecastProfile))
            };

            services.AddAutoMapper(assemblies);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("Redis");
                options.InstanceName = "RedisDemo";
            });

            //services.AddDbContext<EFCoreContext>(options => options.UseSqlite("Filename=:memory:"));
            var desc = services.BuildServiceProvider();
            var dbConnection = (IConnectionForDb)desc.GetService(typeof(IConnectionForDb));

            services.AddDbContext<EFCoreContext>(options
                => options.UseSqlServer(dbConnection.DefaultConnection));

            services.AddAuthentication(appSettings);
            services.AddAuthorization();
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseMiddleware<FileLoggerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
