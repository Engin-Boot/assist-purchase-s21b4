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
            services.AddScoped<IDeviceRepository>(deviceRepository => new DeviceRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\Backend\Devices.csv"));
            services.AddScoped<IDeviceFiltersRepository>(deviceFilterRepository => new DeviceFiltersRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\Backend\Devices.csv"));
            services.AddScoped<ICustomerFilterPreferencesRepository>(mailAlerterRepository => new CustomerFilterPreferencesRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\Backend\FilterPreferences.csv"));
            services.AddScoped<IMailAlerterRepository>(mailAlerterRepository => new MailAlerterRepository(@"D:\a\assist-purchase-s21b4\assist-purchase-s21b4\Segment1_API\Backend\Customers.csv"));
            //services.AddScoped<IDeviceRepository>(deviceRepository => new DeviceRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy\Segment1_API\Backend\Devices.csv"));
            //services.AddScoped<IDeviceFiltersRepository>(deviceFilterRepository => new DeviceFiltersRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy\Segment1_API\Backend\Devices.csv"));
            //services.AddScoped<ICustomerFilterPreferencesRepository>(mailAlerterRepository => new CustomerFilterPreferencesRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy\Segment1_API\Backend\FilterPreferences.csv"));
            //services.AddScoped<IMailAlerterRepository>(mailAlerterRepository => new MailAlerterRepository(@"F:\philips pre-joining training\case study 2 part 2 AssistPurchase\git exact copy\Segment1_API\Backend\Customers.csv"));
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
