using BussinessLayer;
using BussinessLayer.JWT;
using BussinessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using WebApplication;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            services.AddDbContext<EFCoreContext>(options
                => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddAuthentication(appSettings);
            services.AddAuthorization();
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<FileLoggerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
