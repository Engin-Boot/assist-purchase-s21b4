using Backend.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public class Startup
    {
        /*public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }*/

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IDeviceRepository>(deviceRepository => new DeviceRepository(@"C:\Users\MEITY\assist-purchase-s21b4\Client GUI\assist-purchase-backend\Backend\Devices.csv"));
            services.AddScoped<IDeviceFiltersRepository, DeviceFiltersRepository>();
            services.AddScoped<IMailAlerterRepository>(mailAlerterRepository => new MailAlerterRepository(@"C:\Users\MEITY\assist-purchase-s21b4\Client GUI\assist-purchase-backend\Backend\Customers.csv"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

           

            app.UseRouting();

           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
