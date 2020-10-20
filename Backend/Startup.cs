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
            services.AddScoped<IDeviceRepository>(deviceRepository => new DeviceRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Backend\Devices.csv"));
            services.AddScoped<IDeviceFiltersRepository, DeviceFiltersRepository>();
            services.AddScoped<IMailAlerterRepository>(mailAlerterRepository => new MailAlerterRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Backend\Customers.csv"));
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
