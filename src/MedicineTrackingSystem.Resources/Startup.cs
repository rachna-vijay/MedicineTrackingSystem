using Microsoft.Extensions.DependencyInjection;
using MedicineTrackingSystem.Resources.Contracts;
using MedicineTrackingSystem.Resources.Repositories;

namespace MedicineTrackingSystem.Resources
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMedicineRepository, MedicineRepository>();
        }
    }
}
